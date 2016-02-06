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
    _players = pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.View));
  }
  #endregion

}
