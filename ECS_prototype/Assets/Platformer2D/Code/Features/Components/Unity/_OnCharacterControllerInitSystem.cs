using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class _OnCharacterControllerInitSystem : IReactiveSystem {

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities) {
      Debug.LogFormat("_OnCharacterControllerInitSystem : {0}", e._CharacterController.Center);
    }

  }

  public TriggerOnEvent trigger {
    get {
      return Matcher._CharacterController.OnEntityAdded();
    }
  }
  #endregion


}
