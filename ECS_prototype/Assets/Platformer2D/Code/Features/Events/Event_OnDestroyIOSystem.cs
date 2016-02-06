using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_OnDestroyIOSystem : IReactiveSystem, ISetPool {

	private Pool _pool;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
//		Debug.LogFormat("IO_OnDestroy :  {0} ", entities);
		foreach (var e in entities) {
			if(e.hasevent_IO_OnFirstPress){
//				Debug.LogFormat("IO_OnFirstPress :  {0} ", e);
				PowerUpAttributesComponent.fSpeed = 1.0f; // reset PowerUp
			}
			_pool.DestroyEntity(e); // destroy input entity
		}
	}

	public TriggerOnEvent trigger { // triggered by on add of any input entity
		get {
      return Matcher.AnyOf(Matcher.eventGamePad, Matcher.eventDpad, Matcher.eventButton, Matcher.event_IO_OnRelease, 
      Matcher.event_IO_OnFirstPress, Matcher.event_IO_OnFirstRelease).OnEntityAdded(); //AnyOf == either matches will trigger
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool  = pool;
	}
	#endregion
}
