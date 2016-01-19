using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class OnCharacterInitSystem : IReactiveSystem {

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities) {
      var cc = e._CharacterController;
      Debug.LogFormat("_OnCharacterControllerInitSystem : {0} {1} {2}", cc.center, cc.height, cc.radius);
//      cc.Init(cc.body); // NOTE: cc.body not yet set...can't do this here
      MTON.__gUtility.AddComponent_mton<MTON.Controller.CharUpdateController>(cc.body.gameObject);
      MTON.__gUtility.AddComponent_mton<OnCollisionController>(cc.body.gameObject);
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher._CharacterController.OnEntityAdded();
    }
  }
  #endregion


}
