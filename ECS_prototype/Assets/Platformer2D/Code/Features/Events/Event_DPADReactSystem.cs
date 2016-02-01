using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class Event_DPADReactSystem : IReactiveSystem, ISetPool {

  private Group _group;
  private Group _states;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in _group.GetEntities()){
//      Debug.LogFormat("Event_DPADReactSystem : {0}", e._CharacterController.body);
        foreach (var eDPAD in entities){
          e._CharacterController.doMove(eDPAD.dpadEvent.eDirn);
          foreach (var state in _states.GetEntities()){
            Debug.LogFormat("DPAD STATE : {0}", state.vstateEvent.vstate);
            if(eDPAD.dpadEvent.eDirn == MTON._enum.Dirn.DN && state.vstateEvent.vstate == MTON._enum.VState.Ground){
              Debug.LogFormat("I AM CROUCHING {0} ", state.vstateEvent.ID);
            }
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
//    _group = pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.View, Matcher._CharacterController));
    _group = pool.GetGroup(Matcher.AllOf( Matcher.View, Matcher._CharacterController));
    _states = pool.GetGroup(Matcher.AnyOf(Matcher.VstateEvent));
  }
  #endregion

}
