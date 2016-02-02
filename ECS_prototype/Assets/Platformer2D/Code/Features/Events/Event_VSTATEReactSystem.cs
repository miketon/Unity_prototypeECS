using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_VSTATEReactSystem : IReactiveSystem, ISetPool {

  private Group _players;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat(" VSTATE : {0} {1}",e.vstateEvent.ID, e.vstateEvent.vstate);
      foreach (var player in _players.GetEntities()){
        if(player.player.ID == e.vstateEvent.ID){
          player.stateVMotion.vstate = e.vstateEvent.vstate; //update player vmotion state
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
    _players = pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.View, Matcher.stateVMotion));
  }
  #endregion
  
  

}
