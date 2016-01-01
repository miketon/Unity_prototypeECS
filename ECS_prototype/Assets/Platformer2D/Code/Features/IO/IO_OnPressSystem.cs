using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnPressSystem : IReactiveSystem, ISetPool {

	private Pool  _pool;
	private Group _group;
	private float _speed;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var hPos = entities.SingleEntity().iOGamePad.hAxis;
		foreach (var e in _group.GetEntities()) {
			var pos   = e.position;
			_speed   += e.force.speed * hPos * Time.deltaTime;
			e.ReplacePosition(pos.x + _speed, pos.y, pos.z);
//			Debug.LogFormat(" UpdatePosHSystem : {0} ", _speed);
		}
		var ioRelease = true;// entities.SingleEntity().iOGamePad.bNeutral;
		if(ioRelease){
			_speed = 0.0f;
			Debug.LogFormat("IO_ForceSystem : Neutral : {0}", ioRelease);
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
