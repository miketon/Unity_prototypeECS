using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class InputReleaseSystem : IReactiveSystem, ISetPool {

	private Pool _pool;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var inputEntity = entities.SingleEntity();
		_pool.DestroyEntity(inputEntity);
	}
	public TriggerOnEvent trigger {
		get {
			return Matcher.InputRelease.OnEntityAdded();
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool = pool;
	}
	#endregion

}
