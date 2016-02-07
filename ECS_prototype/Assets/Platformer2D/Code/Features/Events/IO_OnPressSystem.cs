using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using MTON;

public class IO_OnPressSystem : IReactiveSystem, ISetPool {

	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
    foreach(var e in entities){
//      Debug.LogFormat("IO_OnPressSystem DPAD {0} {1}", e.dpadEvent.eDirn, e.dpadEvent.magDr);
//      if(e.dpadEvent.eDirn == _enum.Dirn.LT){
//        Debug.Log("Moving LEFT DPAD");
//      }
    }
//		foreach (var e in _group.GetEntities()) {
//			var pos   = e.position;
////			e.force.speed += e.force.accel * hPos * Time.deltaTime * PowerUpAttributesComponent.fSpeed;
//			e.ReplacePosition(pos.x, pos.y, pos.z);
////			Debug.LogFormat(" IO_OnPressSystem : {0} ", e.force.speed);
//		}
	}

	public TriggerOnEvent trigger {
		get {
      return Matcher.eventDpad.OnEntityAdded();
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
    _group = pool.GetGroup(Matcher.AllOf(Matcher.IO_Controllable));
	}
	#endregion

}
