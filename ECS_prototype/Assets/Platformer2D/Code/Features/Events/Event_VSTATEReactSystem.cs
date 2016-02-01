using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_VSTATEReactSystem : IReactiveSystem {

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat(" VSTATE : {0} {1}",e.vstateEvent.ID, e.vstateEvent.vstate);
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.VstateEvent.OnEntityAdded();
    }
  }
  #endregion
  
  

}
