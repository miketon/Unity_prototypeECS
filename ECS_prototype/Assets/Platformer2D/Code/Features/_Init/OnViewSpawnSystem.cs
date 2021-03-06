﻿using UnityEngine;
using System.Collections;
using Entitas;

public class OnViewSpawnSystem : IReactiveSystem {
	
	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities){
		foreach (var e in entities) {
			var rbody = e.view.gameobject.GetComponent<Rigidbody>();
			if(rbody!=null){
				e.Add_RigidBody(rbody);
			}
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.View.OnEntityAdded();
		}
	}
	#endregion



}
