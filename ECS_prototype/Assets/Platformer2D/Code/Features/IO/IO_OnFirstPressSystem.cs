﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnFirstPressSystem : IReactiveSystem, ISetPool {

	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var fBonus = entities.SingleEntity().iO_OnFirstPress.fBonus;
		foreach (var e in _group.GetEntities()) {
			Debug.LogFormat("First Press Bonus : {0} on {1} ", fBonus, e);
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.AnyOf(Matcher.IO_OnFirstPress).OnEntityAdded();
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_group = pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.Force, Matcher.Position));
	}
	#endregion

}
