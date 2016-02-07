using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using Entitas                    ;
using MTON                       ;

public class PlayerInitSystem : IReactiveSystem, ISetPool {

  private Group _players;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      // store init position
      e.position.x = e.view.gameobject.transform.position.x;
      e.position.y = e.view.gameobject.transform.position.y;
      e.position.z = e.view.gameobject.transform.position.z;
      // store init scale
      e.scale.scale = e.view.gameobject.transform.localScale;

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
    _players = pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.View, Matcher.Position, Matcher.Scale));
  }
  #endregion

}
