using UnityEngine;
using System.Collections;
using System;
using Entitas;

public class CharacterUpdateSystem : IExecuteSystem, ISetPool, IInitializeSystem {

  private Group     _group       ;

  #region IExecuteSystem implementation
  public void Execute() {
    foreach (var e in _group.GetEntities()){
//     Debug.LogFormat("EXECUTING : {0}", e);
      var cc = e._CharacterController;
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _group = pool.GetGroup(Matcher.AllOf(Matcher._CharacterController));
  }
  #endregion

  #region IInitializeSystem implementation
  public void Initialize() {
//    Debug.LogFormat("INITIALIZING _CharacterControllerSystem : LAYERGROUND {0}", this._layerGround);
  }
  #endregion

}
