using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class rbodySystem: IReactiveSystem, ISetPool{

	private Group _group;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
//    Debug.LogFormat("RbodySystem : {0}", entities);
  }
  
  public TriggerOnEvent trigger {
    get {
      return Matcher.AnyOf(Matcher.DpadEvent, Matcher.ButtonEvent).OnEntityAdded();
    }
  }
  #endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_group = pool.GetGroup(Matcher.AllOf(Matcher.GameObject, Matcher.Position, Matcher.Velocity, Matcher.Force));
	}
	#endregion

}
