using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using MTON;

public class Event_DPADReactSystem : IReactiveSystem, ISetPool {

  private Group _playerStates ;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      foreach (var state in _playerStates .GetEntities()){
//        Debug.LogFormat("DPAD STATE : {0}", e.dpadEvent.eDirn);
        if(e.eventDpad.dpad == _enum.Dirn.DN && state.stateVMotion.vstate == _enum.VState.Ground){
          if(state.stateCrouch.bCrouch != true){
//            Debug.LogFormat("I AM CROUCHING {0} ", state.player.ID);
            state.stateCrouch.bCrouch = true ;
            Pools.pool.CreateEntity().AddeventCrouch(state.player.ID, true);
          }
        }
        else{
          if(state.stateCrouch.bCrouch != false){
//            Debug.LogFormat("I AM STANDING {0} ", state.player.ID);
            state.stateCrouch.bCrouch = false ;
            Pools.pool.CreateEntity().AddeventCrouch(state.player.ID, false);
          }
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.eventDpad.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) { // get all controllable character
    _playerStates  = pool.GetGroup(Matcher.AllOf( Matcher.Player, Matcher.stateVMotion, Matcher.stateHMotion, Matcher.stateCrouch));
  }
  #endregion

}
