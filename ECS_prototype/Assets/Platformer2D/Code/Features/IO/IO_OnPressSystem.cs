using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnPressSystem : IReactiveSystem, ISetPool {

	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
//		var hPos = entities.SingleEntity().iOGamePad.hAxis;
		foreach (var e in _group.GetEntities()) {
			var pos   = e.position;
//			e.force.speed += e.force.accel * hPos * Time.deltaTime * PowerUpAttributesComponent.fSpeed;
			e.ReplacePosition(pos.x, pos.y, pos.z);
//			Debug.LogFormat(" IO_OnPressSystem : {0} ", e.force.speed);
		}
	}

	public TriggerOnEvent trigger {
		get {
      return Matcher.DpadEvent.OnEntityAdded();
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_group = pool.GetGroup(Matcher.AllOf(Matcher.IOControl));
	}
	#endregion

}
