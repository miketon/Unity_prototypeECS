using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using Entitas                    ;
using MTON                       ;

public class PlayerInitSystem : IReactiveSystem, ISetPool {

  private Group _group;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat("ADDING Player : {0} {1}", e.player.ID, _group.GetEntities());
      foreach (var player in entities){
        var cbody = e.view.gameobject.GetComponent<CharacterController>();
        if(cbody){
          e.Add_CharacterController(cbody)   ; 
          e._CharacterController.Init(cbody) ; // TODO: manual injection to auto injection
          e._CharacterController.cControl.setID(e.player.ID);
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.Player.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _group = pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.View));
  }
  #endregion

}
