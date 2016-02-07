using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class RenderPositionSystem : IReactiveSystem, IEnsureComponents {
	
	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
//		Debug.LogFormat("RenderPosition : {0} ", entities);
		foreach (var e in entities) {
			var pos = e.position;
			e.view.gameobject.transform.position = new Vector3(pos.x, pos.y, pos.z);
		}
	}
	public TriggerOnEvent trigger {
		get {
			return Matcher.AllOf(Matcher.View, Matcher.Position).OnEntityAdded();
		}
	}
	#endregion

	#region IEnsureComponents implementation
	public IMatcher ensureComponents { // What is ensureComponents ???
		get {
			return Matcher.View;
		}
	}
	#endregion

}
