﻿using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.Unity.VisualDebugging;

public class GameUpdateController : MonoBehaviour {

	private Entity  _e;
	private Systems _systems;

	// Use this for initialization
	void Start () {
		var pool = Pools.pool;
		pool.GetGroup(Matcher.Position);
//		pool.GetGroup(Matcher.AllOf(Matcher.Position)); // what does this do???
		_e = pool.CreateEntity();
		_e.AddPosition(0.0f, 3.0f, 1.9f);

		_systems = createSystems(pool);
		_systems.Initialize();

	}

	void Update(){
		_systems.Execute();
	}

	Systems createSystems(Pool pool){
		#if (UNITY_EDITOR)
		return new DebugSystems()
		#else
		return new Systems()
		#endif	

    // Init
    .Add(pool.CreateSystem<_LevelInitSystem>())       // Configs gravity based on proj settings
    .Add(pool.CreateSystem<_StartSystem>())

    // Spawn
    .Add(pool.CreateSystem<OnAddViewSystem>())        // instantiate Unity game objects based on view resource
    .Add(pool.CreateSystem<OnViewSpawnSystem>())      // reacts to new view : inits rbody vs. cbody
    .Add(pool.CreateSystem<OnCharacterInitSystem>())  // adds Update and Collision controller

		// Input
    .Add(pool.CreateSystem<IO_OnPressSystem>())       // reacts to press tokens generated by InputGetController.cs
		.Add(pool.CreateSystem<IO_OnFirstPressSystem>())  // filters for OnFirst pressed event : boost acceleration to make char feel snappier
		.Add(pool.CreateSystem<IO_OnReleaseSystem>())     // reacts to release tokens generated by InputGetController.cs

		// Update
    .Add(pool.CreateSystem<CharacterUpdateSystem>())  // move, ground check lletc
//		.Add(pool.CreateSystem<MoveSystem>())

		// Physics
//		.Add(pool.CreateSystem<RigidBodyUpdateSystem>())
		.Add(pool.CreateSystem<OnCollisionEnterSystem>()) // reacts to collision events

		// Render
		.Add(pool.CreateSystem<RenderPositionSystem>())   // renders position

		// Destroy
		.Add(pool.CreateSystem<IO_OnDestroySystem>());    // this destroys IO tokens generated by InputGetController.cs
		
	}
	
}
