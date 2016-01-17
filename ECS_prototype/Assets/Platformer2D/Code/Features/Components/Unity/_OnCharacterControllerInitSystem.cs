using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class _OnCharacterControllerInitSystem : IReactiveSystem {

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities) {
      Debug.LogFormat("_OnCharacterControllerInitSystem : {0} {1} {2}", e._CharacterController.center, e._CharacterController.height, e._CharacterController.radius);
      MTON.__gUtility.AddComponent_mton<CharUpdateController>(e._CharacterController.body.gameObject);
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher._CharacterController.OnEntityAdded();
    }
  }
  #endregion


}
