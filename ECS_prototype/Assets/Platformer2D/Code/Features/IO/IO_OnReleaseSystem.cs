using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnReleaseSystem : IReactiveSystem, ISetPool {

	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		throw new System.NotImplementedException ();
	}

	public TriggerOnEvent trigger {
		get {
			throw new System.NotImplementedException ();
		}
	}
	#endregion
	
	#region ISetPool implementation
	public void SetPool (Pool pool){
		_group = pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.Force, Matcher.Position));
	}
	#endregion

}
