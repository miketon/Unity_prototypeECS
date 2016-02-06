using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IO_OnFirstPressSystem : IReactiveSystem, ISetPool {

	private Group _group;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
    foreach (var e in entities){
//      Debug.LogFormat("First Press Bonus : {0} ",  e.iO_OnFirstPress.ID);
    }
	}

	public TriggerOnEvent trigger {
		get {
      return Matcher.AnyOf(Matcher.event_IO_OnFirstPress).OnEntityAdded();
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
    _group = pool.GetGroup(Matcher.AllOf(Matcher.IO_Controllable));
	}
	#endregion

}
