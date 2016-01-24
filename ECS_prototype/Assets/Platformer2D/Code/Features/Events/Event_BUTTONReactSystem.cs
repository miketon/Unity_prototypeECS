using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_BUTTONReactSystem : IReactiveSystem, ISetPool {

  private Group _group;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat("Event_BUTTONReactSystem : {0}", e.buttonEvent.bType);
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.ButtonEvent.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _group = pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.View, Matcher._CharacterController));
  }
  #endregion


}
