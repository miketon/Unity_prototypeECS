using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using MTON;

public class IO_OnReleaseSystem : IReactiveSystem, ISetPool {

	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var ioRelease = entities.SingleEntity().iORelease;
		foreach (var e in _group.GetEntities()) {
      if(ioRelease.GPAD == _enum.GPAD.FULL){
        Debug.LogFormat("IO_OnReleaseSystem : ALL : {0}", ioRelease.GPAD);
      }
      else if(ioRelease.GPAD == _enum.GPAD.DPAD){; 
				Debug.LogFormat("IO_OnReleaseSystem : Neutral : bDIRPAD {0}", ioRelease.GPAD);
        e.force.speed = 0.0f;
			}
      else if(ioRelease.GPAD == _enum.GPAD.BTTN){
        e.force.speed = 0.0f;
				var pos = e.position;
				e.ReplacePosition(pos.x, pos.y+0.5f, pos.z);
				Debug.LogFormat("IO_OnReleaseSystem : Neutral : bBUTTON {0} speed : {1}", ioRelease.GPAD, e.force.speed);
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
