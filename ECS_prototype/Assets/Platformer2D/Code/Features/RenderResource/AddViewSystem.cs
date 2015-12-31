using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class AddViewSystem : IReactiveSystem {

	readonly Transform _viewContainer = new GameObject("View").transform; // name of transform parent of new View Object
	
	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		Debug.LogFormat("Add View : {0} ", entities);
		foreach (var e in entities) {
			var res = Resources.Load<GameObject>(e.resource.name);
			GameObject viewObject = null;
			try {
				viewObject = UnityEngine.Object.Instantiate(res);// as GameObject;
			} catch (System.Exception) {
				Debug.LogFormat("Can not instantiate : {0} ", res);
			}
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.Resource.OnEntityAdded();
		}
	}
	#endregion


}
