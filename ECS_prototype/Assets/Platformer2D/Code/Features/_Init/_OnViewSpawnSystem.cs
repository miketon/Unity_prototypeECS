﻿using UnityEngine;
using System.Collections;
using Entitas;

public class _OnViewSpawnSystem : IReactiveSystem {
	
	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities){
		foreach (var e in entities) {
			var rbody = e.view.gameobject.GetComponent<Rigidbody>();
			if(rbody!=null){
				e.AddRigidBody(rbody);
			}
			var cbody = e.view.gameobject.GetComponent<CharacterController>();
			if(cbody!=null){
        e.AddCharacterController(cbody);
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