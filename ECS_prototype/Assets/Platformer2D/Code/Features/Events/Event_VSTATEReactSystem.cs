using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_VSTATEReactSystem : IReactiveSystem, ISetPool {

  private Group _ccUnits;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat(" VSTATE : {0} {1}",e.vstateEvent.ID, e.vstateEvent.vstate);
      foreach (var vstate in _ccUnits.GetEntities()){
        if(vstate._CharacterController.ID == e.vstateEvent.ID){
          vstate.stateVMotion.vstate = e.vstateEvent.vstate; //update player vmotion state
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.VstateEvent.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _ccUnits = pool.GetGroup(Matcher.AllOf(Matcher._CharacterController, Matcher.stateVMotion));
  }
  #endregion
  
  

}
