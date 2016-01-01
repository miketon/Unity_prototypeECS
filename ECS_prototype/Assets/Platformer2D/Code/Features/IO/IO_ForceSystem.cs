using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_ForceSystem : IReactiveSystem, ISetPool {

	private Pool _pool;
	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var hPos = entities.SingleEntity().iOGamePad.hAxis;
		Debug.LogFormat(" UpdatePosHSystem : {0} ", hPos);
		foreach (var e in _group.GetEntities()) {
			var pos   = e.position;
//			e.ReplaceForce(hPos);
			e.ReplacePosition(pos.x + hPos, pos.y, pos.z);
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.IOGamePad.OnEntityAdded();
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool  = pool;
		_group = _pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.Force, Matcher.Position));
	}
	#endregion

}
