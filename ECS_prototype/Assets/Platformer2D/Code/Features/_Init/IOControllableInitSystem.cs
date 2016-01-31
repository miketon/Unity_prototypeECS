using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using MTON;

public class IOControllableInitSystem : IReactiveSystem, ISetPool {

  private Group _group;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat("ADDING IO_Controllable : {0} {1}", e.iO_Controllable.ID, _group.GetEntities());
      foreach (var player in _group.GetEntities()){ // init controller
        Debug.LogFormat("PlayerIO: {0} ", player.view.gameobject);
        var ioController = __gUtility.AddComponent_mton<InputGetController>(player.view.gameobject);
        if(ioController){
          ioController.setID(e.iO_Controllable.ID);
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.IO_Controllable.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _group = pool.GetGroup(Matcher.AllOf(Matcher.IO_Controllable, Matcher.View));
  }
  #endregion

}
