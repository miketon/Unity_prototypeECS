using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_DPADReactSystem : IReactiveSystem, ISetPool {

  private Group _players;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      foreach (var player in _players.GetEntities()){
//        Debug.LogFormat("DPAD STATE : {0}", e.dpadEvent.eDirn);
        if(e.dpadEvent.eDirn == MTON._enum.Dirn.DN && player.stateVMotion.vstate == MTON._enum.VState.Ground){
          Debug.LogFormat("I AM CROUCHING {0} ", player.player.ID);
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.DpadEvent.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) { // get all controllable character
    _players = pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.View, Matcher.stateVMotion, Matcher._CharacterController));
  }
  #endregion

}
