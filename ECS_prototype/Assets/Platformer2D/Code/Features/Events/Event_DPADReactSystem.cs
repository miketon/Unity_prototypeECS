using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using Entitas                    ;
using MTON                       ;

public class Event_DPADReactSystem : IReactiveSystem, ISetPool {

  private Group _ioControllables ;

#region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      foreach (var _io in _ioControllables .GetEntities()){
        if(_io.iO_Controllable.ID == e.eventDpad.ID){
          _io.stateDpad.dpad = e.eventDpad.dpad;
          if(e.eventDpad.dpad == _enum.Dirn.DN && _io.stateVMotion.vstate == _enum.VState.Ground){
            if(_io.stateCrouch.bCrouch != true){ // doCrouch
              _io.stateCrouch.bCrouch = true ;
              Pools.pool.CreateEntity().AddeventCrouch(_io.player.ID, true);
            }
          }
          else{
            if(_io.stateCrouch.bCrouch != false){ // doStand
              _io.stateCrouch.bCrouch = false ;
              Pools.pool.CreateEntity().AddeventCrouch(_io.player.ID, false);
            }
          }
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.eventDpad.OnEntityAdded();
    }
  }
#endregion

#region ISetPool implementation
  public void SetPool(Pool pool) { // get all controllable character
    _ioControllables  = pool.GetGroup(Matcher.AllOf( Matcher.IO_Controllable, Matcher.stateVMotion, 
          Matcher.stateHMotion, Matcher.stateCrouch));
  }
#endregion

}
