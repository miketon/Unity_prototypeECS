using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnFirstPressSystem : IReactiveSystem, ISetPool {

	private Pool _pool;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		throw new System.NotImplementedException ();
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.AnyOf(Matcher.IO_OnFirstPress).OnEntityAdded();
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool = pool;
	}
	#endregion

}
