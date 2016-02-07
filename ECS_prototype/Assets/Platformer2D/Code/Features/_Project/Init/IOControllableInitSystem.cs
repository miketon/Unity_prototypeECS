using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using Entitas                    ;
using MTON                       ;

public class IOControllableInitSystem : IReactiveSystem {

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      Debug.LogFormat("ADDING IO_Controllable : {0} {1}", e.iO_Controllable.ID, e);
      e.AddstateDpad(_enum.Dirn.Neutral);
      e.AddstateButton(_enum.Button.Neutral, _enum.Type.Neutral);
      if(e.hasView){
        var ioController = __gUtility.AddComponent_mton<InputGetController>(e.view.gameobject); // TODO : need to populate input based on ID
        if(ioController){
          ioController.setID(e.iO_Controllable.ID);
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.IO_Controllable.OnEntityAdded();
    }
  }
  #endregion

}
