using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class StateCrouchReactSystem : IReactiveSystem, ISetPool{

  private Group _players ;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      foreach (var player in _players.GetEntities()){
        if(e.eventCrouch.ID == player.player.ID){
          if(e.eventCrouch.bCrouch==true){
            player.view.gameobject.transform.localScale = new Vector3(player.scale.x, player.scale.y, player.scale.z) * 0.5f;
          }
          else{
            player.view.gameobject.transform.localScale = new Vector3(player.scale.x, player.scale.y, player.scale.z);
          }
        }
      }
      
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.eventCrouch.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _players  = pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.View, Matcher.Scale));
  }
  #endregion


}
