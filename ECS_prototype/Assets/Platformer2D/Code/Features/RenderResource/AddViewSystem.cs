using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class AddViewSystem : IReactiveSystem {

	readonly Transform _viewContainer = new GameObject("View").transform; // name of transform parent of new View Object
	
	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		Debug.LogFormat("Add View : {0} ", entities);
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.Resource.OnEntityAdded();
		}
	}
	#endregion


}
