using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using MTON;

public class Event_DPADReactSystem : IReactiveSystem, ISetPool {

  private Group _ioControllables;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      foreach (var control in _ioControllables.GetEntities()){
//        Debug.LogFormat("DPAD STATE : {0}", e.dpadEvent.eDirn);
        if(e.dpadEvent.eDirn == _enum.Dirn.DN && control.stateVMotion.vstate == _enum.VState.Ground){
          Debug.LogFormat("I AM CROUCHING {0} ", control.player.ID);
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.DpadEvent.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) { // get all controllable character
    _ioControllables = pool.GetGroup(Matcher.AllOf(Matcher.IO_Controllable, Matcher._CharacterController, Matcher.stateVMotion));
  }
  #endregion

}
