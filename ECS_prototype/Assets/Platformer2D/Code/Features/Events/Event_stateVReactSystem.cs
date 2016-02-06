using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_StateVReactSystem : IReactiveSystem, ISetPool {

  private Group _ccUnits;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
//      Debug.LogFormat(" VSTATE : {0} {1}",e.eventVMotion.ID, e.eventVMotion.vstate);
      foreach (var vstate in _ccUnits.GetEntities()){
        if(vstate._CharacterController.ID == e.eventVMotion.ID){  // if event ID matches...
          vstate.stateVMotion.vstate = e.eventVMotion.vstate;     // update player vmotion state
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.eventVMotion.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _ccUnits = pool.GetGroup(Matcher.AllOf(Matcher._CharacterController, Matcher.stateVMotion));
  }
  #endregion
  
  

}
