using UnityEngine;
using System.Collections;
using Entitas;

public class _CharacterControllerSystem : IExecuteSystem, ISetPool {

  private Group _group;

  #region IExecuteSystem implementation
  public void Execute() {
   foreach (var e in _group.GetEntities()){
     Debug.LogFormat("EXECUTING : {0}", e);
   }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _group = pool.GetGroup(Matcher.AllOf(Matcher._CharacterController));
  }
  #endregion

}
