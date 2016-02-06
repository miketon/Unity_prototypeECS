using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_StateHReactSystem : IReactiveSystem, ISetPool {

  private Group _ccUnits;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      foreach (var hstate in _ccUnits.GetEntities()){
        if(hstate._CharacterController.ID == e.eventHMotion.ID){  // if event ID matches...
          hstate.stateHMotion.hstate = e.eventHMotion.hstate;     // update player vmotion state
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.eventHMotion.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _ccUnits = pool.GetGroup(Matcher.AllOf(Matcher._CharacterController, Matcher.stateHMotion));
  }
  #endregion

}
