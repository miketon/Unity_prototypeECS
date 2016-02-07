using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using MTON;

public class Event_StateHReactSystem : IReactiveSystem, ISetPool {

  private Group _ccUnits;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      foreach (var unit in _ccUnits.GetEntities()){
        if(unit._CharacterController.ID == e.eventHMotion.ID){  // if event ID matches...
          unit.stateHMotion.hstate = e.eventHMotion.hstate;     // update player vmotion state
          var xform = unit.view.gameobject.transform;
          if(unit.stateHMotion.hstate != _enum.HState.Neutral){ // are moving left/right
            if((unit.stateDpad.dpad & _enum.Dirn.RT) != 0){ // facing right
              Debug.LogFormat("FACING RIGHT : {0}", unit.stateDpad.dpad);
              xform.rotation = Quaternion.Euler(0.0f, -45.0f, 0.0f);
            }
            else{                                       // facing left
              Debug.LogFormat("FACING LEFT  : {0}", unit.stateDpad.dpad);
              xform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
            }
          }
          else{                                         // facing neutral
            Debug.LogFormat("FACING NEUTRAL  : {0}", unit.stateDpad.dpad);
            xform.rotation = unit.rotation.rot;
          }
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
    _ccUnits = pool.GetGroup(Matcher.AllOf(Matcher._CharacterController, Matcher.stateHMotion, Matcher.stateDpad));
  }
  #endregion

}
