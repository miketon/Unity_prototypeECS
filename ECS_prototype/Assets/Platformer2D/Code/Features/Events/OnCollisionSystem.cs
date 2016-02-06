using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class OnCollisionSystem : IReactiveSystem {

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		foreach (var e in entities) {
      Debug.LogFormat("Colliding ! : {0} ", e.eventOnCollision.collision.gameObject);
		}
	}

	public TriggerOnEvent trigger {
		get {
      return Matcher.eventOnCollision.OnEntityAdded();
		}
	}
	#endregion

}
