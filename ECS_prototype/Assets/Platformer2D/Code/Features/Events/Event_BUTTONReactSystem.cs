using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using Entitas                    ;

public class Event_BUTTONReactSystem : IReactiveSystem, ISetPool {

  private Group _ioControllables ;

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      foreach (var _io in _ioControllables .GetEntities()){
        if(e.eventButton.ID == _io.iO_Controllable.ID){
          _io.stateButton.bMode = e.eventButton.bMode;
          _io.stateButton.bType = e.eventButton.bType;
        }  
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.eventButton.OnEntityAdded();
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _ioControllables  = pool.GetGroup(Matcher.AllOf(Matcher.IO_Controllable, Matcher.View, Matcher._CharacterController));
  }
  #endregion

}
