using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_IOReactSystem : IReactiveSystem, ISetPool {

  private Group _group;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var item in entities){
    if(item.rbodyEvent.vState == MTON._enum.VState.Ground){
    foreach (var e in _group.GetEntities()){
      Debug.LogFormat("Event_IOReactSystem : {0}", e);
    }
    }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.RbodyEvent.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _group = pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.View, Matcher._CharacterController));
  }
  #endregion

}
