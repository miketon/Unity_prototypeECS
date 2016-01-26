using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class RbodyEventSystem : IReactiveSystem {

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
//      Debug.LogFormat("RbodyEventSystem : {0} {1} ", e.rbodyEvent.cc, e.rbodyEvent.vState);
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.RbodyEvent.OnEntityAdded();
    }
  }
  #endregion

}
