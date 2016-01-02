﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnReleaseSystem : IReactiveSystem, ISetPool {

	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var ioRelease = entities.SingleEntity().iORelease;
		foreach (var e in _group.GetEntities()) {
			if(ioRelease.bDIRPAD || ioRelease.bNEUTRAL){
				Debug.LogFormat("IO_ForceSystem : Neutral : bDIRPAD {0}", ioRelease);
				e.force.speed = 0.0f;
			}
			else if(ioRelease.bBUTTON){
				Debug.LogFormat("IO_ForceSystem : Neutral : bBUTTON {0} speed : {1}", ioRelease.bBUTTON, e.force.speed);
			}
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.IORelease.OnEntityAdded();
		}
	}
	#endregion
	
	#region ISetPool implementation
	public void SetPool (Pool pool){
		_group = pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.Force, Matcher.Position));
	}
	#endregion

}
