using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnDestroySystem : IReactiveSystem, ISetPool {

	private Pool _pool;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		foreach (var e in entities) {
		  _pool.DestroyEntity(e); // destroy input entity
		}
	}

	public TriggerOnEvent trigger { // triggered by on add of any input entity
		get {
			return Matcher.AnyOf(Matcher.IOGamePad, Matcher.IORelease, Matcher.IO_OnFirstPress).OnEntityAdded(); //AnyOf == either matches will trigger
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool  = pool;
	}
	#endregion
}
