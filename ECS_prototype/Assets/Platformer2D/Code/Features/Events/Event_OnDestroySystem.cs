using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using Entitas                    ;

public class Event_OnDestroySystem : IReactiveSystem, ISetPool {

  private Pool _pool;

  #region IReactiveExecuteSystem implementation
  public void Execute (List<Entity> entities){
    foreach (var e in entities) {
      _pool.DestroyEntity(e); // destroy event entity
    }
  }

  public TriggerOnEvent trigger { // triggered by on add of any matched event entity
    get {
       return Matcher.AnyOf(Matcher.eventVMotion, Matcher.eventHMotion, Matcher.eventCrouch).OnEntityAdded(); //AnyOf == either matches will trigger
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool (Pool pool){
    _pool  = pool;
  }
  #endregion
}
