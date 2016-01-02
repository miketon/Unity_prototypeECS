using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnPressSystem : IReactiveSystem, ISetPool {

	private Pool  _pool;
	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var hPos = entities.SingleEntity().iOGamePad.hAxis;
		foreach (var e in _group.GetEntities()) {
			var pos   = e.position;
			e.force.speed += e.force.accel * hPos * Time.deltaTime;
			e.ReplacePosition(pos.x + e.force.speed, pos.y, pos.z);
//			Debug.LogFormat(" IO_OnPressSystem : {0} ", e.force.speed);
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
