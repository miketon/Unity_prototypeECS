using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class OnCollisionEnterSystem : IReactiveSystem, ISetPool {

	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		foreach (var e in entities) {
			Debug.LogFormat("Colliding ! : {0} ", e.onCollisionEnter.collision.gameObject);
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.OnCollisionEnter.OnEntityAdded();
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_group = pool.GetGroup(Matcher.AllOf(Matcher.OnCollisionEnter));
	}
	#endregion

}
