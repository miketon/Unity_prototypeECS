using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class OnAddViewSystem : IReactiveSystem {

	readonly Transform _viewContainer = new GameObject("View").transform; // name of transform parent of new View Object
	
	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		Debug.LogFormat("Add View : {0} ", entities);
		foreach (var e in entities) {
			var res = Resources.Load<GameObject>(e.viewResource.name);
			GameObject viewObject = null;
			try {
				viewObject = UnityEngine.Object.Instantiate(res);// as GameObject;
			} catch (System.Exception) {
				Debug.LogFormat("Can not instantiate : {0} ", res);
			}
			// parent to view container
			if(viewObject != null){
				viewObject.transform.SetParent(_viewContainer, false);
				e.AddView(viewObject);

				if(e.hasPosition){
					var pos = e.position;
					viewObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
				}
			}
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.ViewResource.OnEntityAdded();
		}
	}
	#endregion


}
