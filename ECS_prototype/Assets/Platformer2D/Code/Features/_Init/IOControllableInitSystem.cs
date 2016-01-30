using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IOControllableInitSystem : IReactiveSystem {

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat("ADDING IO_Controllable : {0}", e.iO_Controllable.ID);
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.IO_Controllable.OnEntityAdded();
    }
  }
  #endregion

}
