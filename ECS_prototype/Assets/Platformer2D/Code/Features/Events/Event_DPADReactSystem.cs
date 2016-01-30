using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_DPADReactSystem : IReactiveSystem, ISetPool {

  private Group _group;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in _group.GetEntities()){
//      Debug.LogFormat("Event_DPADReactSystem : {0}", e._CharacterController.body);
        foreach (var eDPAD in entities){
          e._CharacterController.doMove(eDPAD.dpadEvent.eDirn);
        }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.DpadEvent.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) { // get all controllable character
//    _group = pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.View, Matcher._CharacterController));
    _group = pool.GetGroup(Matcher.AllOf( Matcher.View, Matcher._CharacterController));
  }
  #endregion

}
