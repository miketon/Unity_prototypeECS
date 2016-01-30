using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using Entitas                    ;

public class Event_BUTTONReactSystem : IReactiveSystem, ISetPool {

  private Group _group;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
//      Debug.LogFormat("Event_BUTTONReactSystem : {0} {1}", e.buttonEvent.bType, e.buttonEvent.bMode);
      if(e.buttonEvent.bMode == MTON._enum.Button.Down){ // is button down
        if(e.buttonEvent.bType == MTON._enum.Type.Jump){ // handle jumps
//          Debug.LogFormat("Event_BUTTONReactSystem : JUMP! {0} {1}", e.buttonEvent.bType, e.buttonEvent.bMode);
          foreach (var cc in _group.GetEntities()){
            cc._CharacterController.doJump();
          }
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.ButtonEvent.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _group = pool.GetGroup(Matcher.AllOf(Matcher.IO_Controllable, Matcher.View, Matcher._CharacterController));
  }
  #endregion


}
