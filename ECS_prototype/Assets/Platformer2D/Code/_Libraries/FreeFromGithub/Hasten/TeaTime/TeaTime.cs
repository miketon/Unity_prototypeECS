﻿// TeaTime v0.6.5.3 alpha

// By Andrés Villalobos [andresalvivar@gmail.com twitter.com/matnesis]
// Special thanks to Antonio Zamora [twitter.com/tzamora] (loop idea and initial push).
// Created 2014/12/26 12:21 am

// TeaTime is a fast & simple queue for timed callbacks, fashioned as a
// MonoBehaviour extension set, focused on solving common coroutines patterns in
// Unity games.

// Just put 'TeaTime.cs' somewhere in your project and call it inside any
// MonoBehaviour using 'this.tt' (and trust the autocomplete).


//    this.tt("QueueExample").ttAdd(2, () =>
//    {
//        Debug.Log("2 seconds since QueueExample started " + Time.time);
//    })
//    .ttLoop(3, delegate(ttHandler loop)
//    {
//        // ttLoop runs frame by frame for all his duration (3s) and his handler have a
//        // custom delta (loop.deltaTime) that represents the precise loop duration.
//        Camera.main.backgroundColor
//            = Color.Lerp(Camera.main.backgroundColor, Color.black, loop.deltaTime);
//    })
//    .ttAdd(() =>
//    {
//        Debug.Log("5 seconds since QueueExample started " + Time.time);
//    })


// Check out the examples!
// [http://github.com/alvivar/TeaTime/tree/master/Examples] to learn with
// practical patterns! (*More to come*)


// Copyright (c) 2014/12/26 andresalvivar@gmail.com

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// TeaTime task (queue data).
/// </summary>
public class ttTask
{
    public MonoBehaviour instance;
    public string queueName;
    public float time = 0f;
    public YieldInstruction yieldInstruction = null;
    public Action callback = null;
    public Action<ttHandler> callbackWithHandler = null;
    public bool isLoop = false;


    public ttTask(MonoBehaviour instance, string queueName,
                  float time, YieldInstruction yield,
                  Action callback, Action<ttHandler> callbackWithHandler,
                  bool isLoop)
    {
        this.instance = instance;
        this.queueName = queueName;
        this.time = time;
        this.yieldInstruction = yield;
        this.callback = callback;
        this.callbackWithHandler = callbackWithHandler;
        this.isLoop = isLoop;
    }
}


/// <summary>
/// TeaTime callback handler.
/// </summary>
public class ttHandler
{
    public bool isActive = true;
    public float t = 0f;
    public float deltaTime = 0f;
    public float timeSinceStart = 0f;
    public List<YieldInstruction> yieldsToWait;
    public List<IEnumerator> ienumsToWait;


    /// <summary>
    /// Breaks the current loop.
    /// </summary>
    public void Break()
    {
        this.isActive = false;
    }


    /// <summary>
    /// Appends a time interval to wait after the current callback.
    /// </summary>
    public void WaitFor(float interval)
    {
        this.WaitFor(new WaitForSeconds(interval));
    }


    /// <summary>
    /// Appends a YieldInstruction to wait after the current callback.
    /// </summary>
    public void WaitFor(YieldInstruction yieldToWait)
    {
        if (this.yieldsToWait == null)
            this.yieldsToWait = new List<YieldInstruction>();

        this.yieldsToWait.Add(yieldToWait);
    }


    /// <summary>
    /// Appends an IEnumerator to execute and wait after the current callback.
    /// </summary>
    public void WaitFor(IEnumerator ienumToWait)
    {
        if (this.ienumsToWait == null)
            this.ienumsToWait = new List<IEnumerator>();

        this.ienumsToWait.Add(ienumToWait);
    }
}


/// <summary>
/// TeaTime is a fast & simple queue for timed callbacks, fashioned as a
/// MonoBehaviour extension set, focused on solving common coroutines patterns in
/// Unity games.
/// </summary>
public static class TeaTime
{
    /// <summary>
    /// Debug mode prints queue interactions on console.
    /// </summary>
    public static bool debugMode = false;

    /// <summary>
    /// Default queue name.
    /// </summary>
    private const string DEFAULT_QUEUE_NAME = "TEATIME_DEFAULT_QUEUE_NAME";

    /// <summary>
    /// Main queue for all the timed callbacks.
    /// </summary>
    private static Dictionary<MonoBehaviour, Dictionary<string, List<ttTask>>> mainQueue = null;

    /// <summary>
    /// Contains a complete copy of every queue and their respective callbacks.
    /// </summary>
    private static Dictionary<MonoBehaviour, Dictionary<string, List<ttTask>>> blueprints = null;

    /// <summary>
    /// Current queue name.
    /// </summary>
    private static Dictionary<MonoBehaviour, string> currentQueueName = null;

    /// <summary>
    /// Queues currently running.
    /// </summary>
    private static Dictionary<MonoBehaviour, List<string>> runningQueues = null;

    /// <summary>
    /// Queues locked by ttWait().
    /// </summary>
    private static Dictionary<MonoBehaviour, List<string>> lockedQueues = null;

    /// <summary>
    /// Queues paused by ttPause().
    /// </summary>
    private static Dictionary<MonoBehaviour, List<string>> pausedQueues = null;

    /// <summary>
    /// Infinite queues by ttRepeat(-n).
    /// </summary>
    private static Dictionary<MonoBehaviour, List<string>> infiniteQueues = null;

    /// <summary>
    /// Coroutines running in the instance, by queue name.
    /// </summary>
    private static Dictionary<MonoBehaviour, Dictionary<string, List<IEnumerator>>> runningCoroutines = null;


    /// <summary>
    /// Prepares the main queue for the instance (and the blueprints mirror).
    /// </summary>
    private static void PrepareMainQueue(MonoBehaviour instance, string queueName = null)
    {
        // Main queue
        if (mainQueue == null)
            mainQueue = new Dictionary<MonoBehaviour, Dictionary<string, List<ttTask>>>();

        if (!mainQueue.ContainsKey(instance))
            mainQueue.Add(instance, new Dictionary<string, List<ttTask>>());


        // Blueprints
        if (blueprints == null)
            blueprints = new Dictionary<MonoBehaviour, Dictionary<string, List<ttTask>>>();

        if (!blueprints.ContainsKey(instance))
            blueprints.Add(instance, new Dictionary<string, List<ttTask>>());


        // Task list for both
        if (queueName != null)
        {
            if (!mainQueue[instance].ContainsKey(queueName))
                mainQueue[instance].Add(queueName, new List<ttTask>());

            if (!blueprints[instance].ContainsKey(queueName))
                blueprints[instance].Add(queueName, new List<ttTask>());
        }
    }


    /// <summary>
    /// Prepares the dictionary for the current queue name in the instance.
    /// </summary>
    private static void PrepareCurrentQueueName(MonoBehaviour instance)
    {
        if (currentQueueName == null)
            currentQueueName = new Dictionary<MonoBehaviour, string>();

        if (!currentQueueName.ContainsKey(instance))
            currentQueueName[instance] = DEFAULT_QUEUE_NAME;
    }


    /// <summary>
    /// Prepares the dictionary for the queues running in the instance.
    /// </summary>
    private static void PrepareRunningQueues(MonoBehaviour instance)
    {
        if (runningQueues == null)
            runningQueues = new Dictionary<MonoBehaviour, List<string>>();

        if (!runningQueues.ContainsKey(instance))
            runningQueues.Add(instance, new List<string>());
    }


    /// <summary>
    /// Prepares the dictionary for the queues locked in the instance.
    /// </summary>
    private static void PrepareLockedQueues(MonoBehaviour instance)
    {
        if (lockedQueues == null)
            lockedQueues = new Dictionary<MonoBehaviour, List<string>>();

        if (!lockedQueues.ContainsKey(instance))
            lockedQueues.Add(instance, new List<string>());
    }


    /// <summary>
    /// Prepares the dictionary for the queues paused in the instance.
    /// </summary>
    private static void PreparePausedQueues(MonoBehaviour instance)
    {
        if (pausedQueues == null)
            pausedQueues = new Dictionary<MonoBehaviour, List<string>>();

        if (!pausedQueues.ContainsKey(instance))
            pausedQueues.Add(instance, new List<string>());

    }


    /// <summary>
    /// Prepares the dictionary for the infinite queues in the instance.
    /// </summary>
    private static void PrepareInfiniteQueues(MonoBehaviour instance)
    {
        if (infiniteQueues == null)
            infiniteQueues = new Dictionary<MonoBehaviour, List<string>>();

        if (!infiniteQueues.ContainsKey(instance))
            infiniteQueues.Add(instance, new List<string>());
    }


    /// <summary>
    /// Prepares the dictionary for the coroutines running in the instance.
    /// </summary>
    private static void PrepareRunningCoroutines(MonoBehaviour instance, string queueName = null)
    {
        if (runningCoroutines == null)
            runningCoroutines = new Dictionary<MonoBehaviour, Dictionary<string, List<IEnumerator>>>();

        if (!runningCoroutines.ContainsKey(instance))
            runningCoroutines.Add(instance, new Dictionary<string, List<IEnumerator>>());

        if (queueName != null)
        {
            if (!runningCoroutines[instance].ContainsKey(queueName))
                runningCoroutines[instance].Add(queueName, new List<IEnumerator>());
        }
    }


    /// <summary>
    /// Returns true if a queue is empty.
    /// </summary>
    private static bool IsEmpty(MonoBehaviour instance, string queueName)
    {
        PrepareMainQueue(instance, queueName);

        if (mainQueue[instance][queueName].Count < 1)
            return true;

        return false;
    }


    /// <summary>
    /// Returns true if a queue is locked, and unlocks empty queues (returning false).
    /// </summary>
    private static bool IsLockedOrUnlockEmpty(MonoBehaviour instance, string queueName)
    {
        PrepareLockedQueues(instance);

        // Unlock if empty
        if (IsEmpty(instance, queueName))
        {
            if (lockedQueues[instance].Contains(queueName))
                lockedQueues[instance].Remove(queueName);

            return false;
        }

        return lockedQueues[instance].Contains(queueName);
    }


    /// <summary>
    /// Returns true if a queue is paused.
    /// </summary>
    private static bool IsPaused(MonoBehaviour instance, string queueName)
    {
        PreparePausedQueues(instance);

        return pausedQueues[instance].Contains(queueName);
    }


    /// <summary>
    /// Returns true if a queue is infinite.
    /// </summary>
    private static bool IsInfinite(MonoBehaviour instance, string queueName)
    {
        PrepareInfiniteQueues(instance);

        return infiniteQueues[instance].Contains(queueName);
    }


    /// <summary>
    /// Appends a callback (timed or looped) into a queue.
    /// </summary>
    private static MonoBehaviour ttAdd(this MonoBehaviour instance,
                                       float timeDelay, YieldInstruction yieldDelay,
                                       Action callback, Action<ttHandler> callbackWithHandler,
                                       bool isLoop)
    {
        PrepareCurrentQueueName(instance);
        string queueName = currentQueueName[instance];


        // Ignore locked
        if (IsLockedOrUnlockEmpty(instance, queueName))
            return instance;


        // Adds a new task in the main queue
        ttTask currentTask = new ttTask(instance, queueName,
                                        timeDelay, yieldDelay,
                                        callback, callbackWithHandler,
                                        isLoop);

        mainQueue[instance][queueName].Add(currentTask);


        // Mirrors the main queue in blueprints
        blueprints[instance][queueName].Add(currentTask);


        // Execute when isn't paused
        if (!IsPaused(instance, queueName))
            instance.StartCoroutine(ExecuteQueue(instance, queueName));


        return instance;
    }


    /// <summary>
    /// Appends a timed callback into the current queue.
    /// </summary>
    public static MonoBehaviour ttAdd(this MonoBehaviour instance, float timeDelay, Action callback)
    {
        return instance.ttAdd(timeDelay, null, callback, null, false);
    }


    /// <summary>
    /// Appends a timed callback into the current queue.
    /// </summary>
    public static MonoBehaviour ttAdd(this MonoBehaviour instance, float timeDelay, Action<ttHandler> callback)
    {
        return instance.ttAdd(timeDelay, null, null, callback, false);
    }


    /// <summary>
    /// Appends a timed callback into the current queue.
    /// </summary>
    public static MonoBehaviour ttAdd(this MonoBehaviour instance, YieldInstruction yieldToWait, Action callback)
    {
        return instance.ttAdd(0, yieldToWait, callback, null, false);
    }


    /// <summary>
    /// Appends a timed callback into the current queue.
    /// </summary>
    public static MonoBehaviour ttAdd(this MonoBehaviour instance, YieldInstruction yieldToWait, Action<ttHandler> callback)
    {
        return instance.ttAdd(0, yieldToWait, null, callback, false);
    }


    /// <summary>
    /// Appends a timed interval into the current queue.
    /// </summary>
    public static MonoBehaviour ttAdd(this MonoBehaviour instance, float interval)
    {
        return instance.ttAdd(interval, null, null, null, false);
    }


    /// <summary>
    /// Appends a YieldInstruction into the current queue.
    /// </summary>
    public static MonoBehaviour ttAdd(this MonoBehaviour instance, YieldInstruction yieldToWait)
    {
        return instance.ttAdd(0, yieldToWait, null, null, false);
    }


    /// <summary>
    /// Appends a callback into the current queue.
    /// </summary>
    public static MonoBehaviour ttAdd(this MonoBehaviour instance, Action callback)
    {
        return instance.ttAdd(0, null, callback, null, false);
    }


    /// <summary>
    /// Appends a callback into the current queue.
    /// </summary>
    public static MonoBehaviour ttAdd(this MonoBehaviour instance, Action<ttHandler> callback)
    {
        return instance.ttAdd(0, null, null, callback, false);
    }


    /// <summary>
    /// Appends into the current queue a callback that runs frame by frame for all his duration or until ttHandler.Break().
    /// </summary>
    public static MonoBehaviour ttLoop(this MonoBehaviour instance, float duration, Action<ttHandler> callback)
    {
        return instance.ttAdd(duration, null, null, callback, true);
    }


    /// <summary>
    /// Appends into the current queue an (infinite) callback that runs frame by frame until ttHandler.Break().
    /// </summary>
    public static MonoBehaviour ttLoop(this MonoBehaviour instance, Action<ttHandler> callback)
    {
        return instance.ttAdd(0, null, null, callback, true);
    }


    /// <summary>
    /// Wait for completion.
    /// Locks the current queue ignoring new appends until all his callbacks are completed.
    /// </summary>
    public static MonoBehaviour ttWait(this MonoBehaviour instance)
    {
        PrepareCurrentQueueName(instance);

        if (!IsLockedOrUnlockEmpty(instance, currentQueueName[instance]))
            lockedQueues[instance].Add(currentQueueName[instance]);

        return instance;
    }


    /// <summary>
    /// Pauses the current queue (use .ttPlay() to resume).
    /// </summary>
    public static MonoBehaviour ttPause(this MonoBehaviour instance)
    {
        PrepareCurrentQueueName(instance);

        if (!IsPaused(instance, currentQueueName[instance]))
            pausedQueues[instance].Add(currentQueueName[instance]);

        return instance;
    }


    /// <summary>
    /// Stops the current queue (use .ttPlay() to restart).
    /// </summary>
    public static MonoBehaviour ttStop(this MonoBehaviour instance)
    {
        PrepareCurrentQueueName(instance);
        string queueName = currentQueueName[instance];


        // If empty, just pause and skip the rest
        if (IsEmpty(instance, queueName))
            return instance.ttPause();


        // Cleaning only the callbacks and coroutines, leaving the queue
        // config as they are
        PrepareMainQueue(instance, queueName);
        PrepareRunningQueues(instance);
        PrepareRunningCoroutines(instance, queueName);

        // Cleaning callbacks
        mainQueue[instance][queueName].Clear();

        // Cleaning running queues
        if (runningQueues[instance].Contains(queueName))
            runningQueues[instance].Remove(queueName);

        // Stop & clean coroutines
        foreach (IEnumerator coroutine in runningCoroutines[instance][queueName])
        {
            instance.StopCoroutine(coroutine);
        }
        runningCoroutines[instance][queueName].Clear();


        // Pause
        instance.ttPause();

        // Restart the main queue using blueprints
        mainQueue[instance][queueName].AddRange(blueprints[instance][queueName]);


        return instance;
    }


    /// <summary>
    /// Resume the current queue.
    /// </summary>
    public static MonoBehaviour ttPlay(this MonoBehaviour instance)
    {
        PrepareCurrentQueueName(instance);
        string queueName = currentQueueName[instance];


        if (debugMode)
            Debug.Log("TeaTime :: ttPlay " + queueName);


        // Unpauses the queue
        if (IsPaused(instance, queueName))
            pausedQueues[instance].Remove(queueName);

        // Execute queue
        instance.StartCoroutine(ExecuteQueue(instance, queueName));


        return instance;
    }


    /// <summary>
    /// Stops and resets the current queue (full cleanup).
    /// </summary>
    public static MonoBehaviour ttReset(this MonoBehaviour instance)
    {
        PrepareCurrentQueueName(instance);

        Reset(instance, currentQueueName[instance]);

        return instance;
    }


    /// <summary>
    /// Repeats the current queue n times or infinite (n <= -1).
    /// </summary>
    public static MonoBehaviour ttRepeat(this MonoBehaviour instance, int n = -1)
    {
        PrepareCurrentQueueName(instance);
        string queueName = currentQueueName[instance];


        if (debugMode)
            Debug.Log("TeaTime :: ttRepeat " + queueName + ", n = " + n);


        // If infinite
        if (n < 0)
        {
            instance.ttWait();

            PrepareInfiniteQueues(instance);

            if (!infiniteQueues[instance].Contains(queueName))
                infiniteQueues[instance].Add(queueName);

            return instance;
        }


        // Repeat n
        while (n-- > 0)
        {
            mainQueue[instance][queueName].AddRange(blueprints[instance][queueName]);
        }


        return instance;
    }


    /// <summary>
    /// Creates or changes the current queue.
    /// When used without name the queue id will be random and untrackable.
    /// </summary>
    public static MonoBehaviour tt(this MonoBehaviour instance, string queueName = null)
    {
        if (queueName == null)
            queueName = DEFAULT_QUEUE_NAME + Time.time + UnityEngine.Random.Range(0, int.MaxValue);

        PrepareCurrentQueueName(instance);
        currentQueueName[instance] = queueName;

        return instance;
    }


    /// <summary>
    /// Stops and resets a queue from an instance.
    /// </summary>
    public static void Reset(MonoBehaviour instance, string queueName)
    {
        if (debugMode)
            Debug.Log("TeaTime :: Reset " + queueName + " from " + instance.name);


        PrepareMainQueue(instance, queueName);
        PrepareRunningQueues(instance);
        PrepareLockedQueues(instance);
        PreparePausedQueues(instance);
        PrepareInfiniteQueues(instance);
        PrepareRunningCoroutines(instance, queueName);


        // Clean
        mainQueue[instance][queueName].Clear();
        blueprints[instance][queueName].Clear();

        if (runningQueues[instance].Contains(queueName))
            runningQueues[instance].Remove(queueName);

        if (lockedQueues[instance].Contains(queueName))
            lockedQueues[instance].Remove(queueName);

        if (pausedQueues[instance].Contains(queueName))
            pausedQueues[instance].Remove(queueName);

        if (infiniteQueues[instance].Contains(queueName))
            infiniteQueues[instance].Remove(queueName);


        // Stop & clean coroutines
        foreach (IEnumerator coroutine in runningCoroutines[instance][queueName])
        {
            instance.StopCoroutine(coroutine);
        }
        runningCoroutines[instance][queueName].Clear();
    }


    /// <summary>
    /// Stops and resets all queues from an instance.
    /// </summary>
    public static void Reset(MonoBehaviour instance)
    {
        PrepareMainQueue(instance);
        PrepareRunningQueues(instance);
        PrepareLockedQueues(instance);
        PreparePausedQueues(instance);
        PrepareInfiniteQueues(instance);
        PrepareRunningCoroutines(instance);


        // Clean all
        foreach (KeyValuePair<string, List<ttTask>> taskList in mainQueue[instance])
            taskList.Value.Clear();

        foreach (KeyValuePair<string, List<ttTask>> taskList in blueprints[instance])
            taskList.Value.Clear();

        runningQueues[instance].Clear();
        lockedQueues[instance].Clear();
        pausedQueues[instance].Clear();
        infiniteQueues[instance].Clear();


        // Stop & clean coroutines
        foreach (KeyValuePair<string, List<IEnumerator>> coroutineList in runningCoroutines[instance])
        {
            foreach (IEnumerator coroutine in coroutineList.Value)
            {
                instance.StopCoroutine(coroutine);
            }
            coroutineList.Value.Clear();
        }
        runningCoroutines[instance].Clear();
    }


    /// <summary>
    /// Stops and resets all queues in all instances.
    /// </summary>
    public static void ResetAll()
    {
        // Main queue & Blueprints clear
        if (mainQueue != null)
        {
            foreach (KeyValuePair<MonoBehaviour, Dictionary<string, List<ttTask>>> instanceDict in mainQueue)
            {
                foreach (KeyValuePair<string, List<ttTask>> taskList in instanceDict.Value)
                {
                    taskList.Value.Clear();
                }
            }

            foreach (KeyValuePair<MonoBehaviour, Dictionary<string, List<ttTask>>> instanceDict in blueprints)
            {
                foreach (KeyValuePair<string, List<ttTask>> taskList in instanceDict.Value)
                {
                    taskList.Value.Clear();
                }
            }
        }

        // Running queues clear
        if (runningQueues != null)
        {
            foreach (KeyValuePair<MonoBehaviour, List<string>> runningList in runningQueues)
            {
                runningList.Value.Clear();
            }
        }

        // Queues names clear
        if (currentQueueName != null)
        {
            List<MonoBehaviour> keys = new List<MonoBehaviour>(currentQueueName.Keys);
            foreach (MonoBehaviour key in keys)
            {
                currentQueueName[key] = DEFAULT_QUEUE_NAME;
            }
        }

        // Locked queues clear
        if (lockedQueues != null)
        {
            foreach (KeyValuePair<MonoBehaviour, List<string>> lockedList in lockedQueues)
            {
                lockedList.Value.Clear();
            }
        }

        // Paused queues
        if (pausedQueues != null)
        {
            foreach (KeyValuePair<MonoBehaviour, List<string>> pausedList in pausedQueues)
            {
                pausedList.Value.Clear();
            }
        }

        // Infinite queues clear
        if (infiniteQueues != null)
        {
            foreach (KeyValuePair<MonoBehaviour, List<string>> infiniteList in infiniteQueues)
            {
                infiniteList.Value.Clear();
            }
        }

        // Stop & clean all coroutines
        if (runningCoroutines != null)
        {
            foreach (KeyValuePair<MonoBehaviour, Dictionary<string, List<IEnumerator>>> instanceDict in runningCoroutines)
            {
                foreach (KeyValuePair<string, List<IEnumerator>> coroutineList in instanceDict.Value)
                {
                    foreach (IEnumerator coroutine in coroutineList.Value)
                    {
                        instanceDict.Key.StopCoroutine(coroutine);
                    }
                    coroutineList.Value.Clear();
                }
            }
            runningCoroutines.Clear();
        }
    }


    /// <summary>
    /// Executes all timed callbacks and loops for an instance queue.
    /// </summary>
    private static IEnumerator ExecuteQueue(MonoBehaviour instance, string queueName)
    {
        // Ignore if the queue is empty
        if (IsEmpty(instance, queueName))
            yield break;


        PrepareRunningQueues(instance);

        // Ignore if already running
        if (runningQueues.ContainsKey(instance) && runningQueues[instance].Contains(queueName))
            yield break;

        // Marks the queue as running
        runningQueues[instance].Add(queueName);


        // Coroutines registry
        PrepareRunningCoroutines(instance, queueName);
        IEnumerator coroutine = null;


        // Run over a clone until depleted
        List<ttTask> batch = new List<ttTask>();
        batch.AddRange(mainQueue[instance][queueName]);

        foreach (ttTask task in batch)
        {
            // Selection
            if (task.isLoop)
            {
                if (task.time > 0)
                {
                    coroutine = ExecuteLoop(task.instance, task.queueName,
                                            task.time,
                                            task.callbackWithHandler);
                }
                else
                {
                    coroutine = ExecuteInfiniteLoop(task.instance, task.queueName,
                                                    task.callbackWithHandler);
                }
            }
            else
            {
                coroutine = ExecuteOnce(task.instance, task.queueName,
                                        task.time, task.yieldInstruction,
                                        task.callback, task.callbackWithHandler);
            }

            // Register and execute coroutines
            runningCoroutines[instance][queueName].Add(coroutine);
            yield return instance.StartCoroutine(coroutine);
            runningCoroutines[instance][queueName].Remove(coroutine);

            // Done!
            mainQueue[instance][queueName].Remove(task);
        }

        // The queue has stopped
        runningQueues[instance].Remove(queueName);


        // Try again is there are new items
        if (mainQueue[instance][queueName].Count > 0)
        {
            instance.StartCoroutine(ExecuteQueue(instance, queueName));
        }
        else
        {
            // Repeat if infinite
            if (IsInfinite(instance, queueName))
            {
                // Restart the main queue using blueprints
                mainQueue[instance][queueName].AddRange(blueprints[instance][queueName]);
                instance.StartCoroutine(ExecuteQueue(instance, queueName));
            }
            else
            {
                // Queue completed, remove the lock
                IsLockedOrUnlockEmpty(instance, queueName);
            }
        }

    }


    /// <summary>
    /// Executes a timed callback.
    /// </summary>
    private static IEnumerator ExecuteOnce(MonoBehaviour instance, string queueName,
                                           float timeToWait, YieldInstruction yieldToWait,
                                           Action callback, Action<ttHandler> callbackWithHandler)
    {
        // #fix
        // Inmediate execution breaks the queue order with nested queues
        if (timeToWait < Time.deltaTime && yieldToWait == null)
            yield return new WaitForEndOfFrame();


        // Pause
        while (IsPaused(instance, queueName))
            yield return null;


        // Wait for
        if (timeToWait > 0)
            yield return new WaitForSeconds(timeToWait);

        if (yieldToWait != null)
            yield return yieldToWait;


        // Executes the normal callback
        if (callback != null)
            callback();


        // Executes the callback with handler (and waits his yields / ienumerators)
        if (callbackWithHandler != null)
        {
            ttHandler t = new ttHandler();
            callbackWithHandler(t);


            // Waits all the handler yields
            if (t.yieldsToWait != null)
            {
                foreach (YieldInstruction yi in t.yieldsToWait)
                {
                    yield return yi;
                }
            }


            // Executes and waits all the handler IEnumerators
            if (t.ienumsToWait != null)
            {
                foreach (IEnumerator ien in t.ienumsToWait)
                {
                    yield return instance.StartCoroutine(ien);
                }
            }
        }


        yield return null;
    }


    /// <summary>
    /// Executes a callback inside a loop for all his duration or until ttHandler.Break().
    /// </summary>
    private static IEnumerator ExecuteLoop(MonoBehaviour instance, string queueName,
                                           float duration,
                                           Action<ttHandler> callback)
    {
        // Only for positive values
        if (duration <= 0)
            yield break;


        // #fix
        // Inmediate execution breaks the queue order with nested queues
        yield return new WaitForEndOfFrame();


        // Handler data
        ttHandler loopHandler = new ttHandler();
        float tRate = 1 / duration;


        // Run while active until duration
        while (loopHandler.isActive && loopHandler.t <= 1)
        {
            // deltaTime
            float unityDeltatime = Time.deltaTime;


            // Completion % from 0 to 1
            loopHandler.t += tRate * unityDeltatime;


            // Customized delta that represents the loop duration
            loopHandler.deltaTime = 1 / (duration - loopHandler.timeSinceStart) * unityDeltatime;
            loopHandler.timeSinceStart += unityDeltatime;


            // Pause
            while (IsPaused(instance, queueName))
                yield return null;


            // Callback execution
            if (callback != null)
                callback(loopHandler);


            // Waits all the yields, once
            if (loopHandler.yieldsToWait != null)
            {
                foreach (YieldInstruction yi in loopHandler.yieldsToWait)
                {
                    yield return yi;
                }

                loopHandler.yieldsToWait.Clear();
            }


            // Executes and waits all IEnumerators, once
            if (loopHandler.ienumsToWait != null)
            {
                foreach (IEnumerator ien in loopHandler.ienumsToWait)
                {
                    yield return instance.StartCoroutine(ien);
                }

                loopHandler.ienumsToWait.Clear();
            }


            yield return null;
        }
    }


    /// <summary>
    /// Executes a callback inside an infinite loop until ttHandler.Break().
    /// </summary>
    private static IEnumerator ExecuteInfiniteLoop(MonoBehaviour instance, string queueName, Action<ttHandler> callback)
    {
        // #fix
        // Inmediate execution breaks the queue order with nested queues
        yield return new WaitForEndOfFrame();

        ttHandler loopHandler = new ttHandler();


        // Run while active
        while (loopHandler.isActive)
        {
            // deltaTime
            float unityDeltaTime = Time.deltaTime;
            loopHandler.deltaTime = unityDeltaTime;
            loopHandler.timeSinceStart += unityDeltaTime;


            // Pause
            while (IsPaused(instance, queueName))
                yield return null;


            // Callback execution
            if (callback != null)
                callback(loopHandler);


            // Waits all the yields, once
            if (loopHandler.yieldsToWait != null)
            {
                foreach (YieldInstruction yi in loopHandler.yieldsToWait)
                {
                    yield return yi;
                }

                loopHandler.yieldsToWait.Clear();
            }


            // Executes & waits all IEnumerators, once
            if (loopHandler.ienumsToWait != null)
            {
                foreach (IEnumerator ien in loopHandler.ienumsToWait)
                {
                    yield return instance.StartCoroutine(ien);
                }

                loopHandler.ienumsToWait.Clear();
            }


            yield return null;
        }
    }
}
