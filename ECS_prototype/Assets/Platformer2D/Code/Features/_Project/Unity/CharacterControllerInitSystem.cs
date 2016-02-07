using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Entitas;

public class CharacterControllerInitSystem : IReactiveSystem , ISetPool {

  private Group _cControls ;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var control in _cControls.GetEntities()){
      var cbody = control.view.gameobject.GetComponent<CharacterController>();
      if(cbody){
        var cControl = MTON.__gUtility.AddComponent_mton<MTON.Controller.CharUpdateController>(cbody.gameObject)  ;
        cControl.setID(control._CharacterController.ID)                                                           ; //fires off vState events, needs ID
        var onColl   = MTON.__gUtility.AddComponent_mton<OnCollisionController>(cbody.gameObject) ;
        this.Init(cbody, control._CharacterController);
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher._CharacterController.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _cControls = pool.GetGroup(Matcher.AllOf(Matcher._CharacterController, Matcher.View));
  }
  #endregion

  private void Init(CharacterController body, _CharacterControllerComponent cc){
    if(body){
      cc.center   = body.center;
      cc.height   = (body.height * body.transform.localScale.y * 0.5f) + body.skinWidth ; 
      cc.radius   = body.radius * body.transform.localScale.x ;
      cc.initRo   = body.transform.rotation;
    }
  }

}
