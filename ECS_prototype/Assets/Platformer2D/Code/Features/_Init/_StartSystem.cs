using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class _StartSystem : IReactiveSystem {

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat("_OnStartSystem : {0}", e);
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.AnyOf(Matcher._OnStart).OnEntityAdded();
    }
  }
  #endregion


}
