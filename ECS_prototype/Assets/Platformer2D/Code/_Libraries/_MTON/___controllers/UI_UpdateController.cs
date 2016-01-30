using UnityEngine        ;
using UnityEngine.UI     ;
using System             ;
using System.Collections ;
using Entitas            ;
using MTON               ;

namespace MTON.Controller {

  public class UI_UpdateController : MonoBehaviour {

    public Text txt_DPAD   ;
    public Text txt_Button ;

    void Start () {

      var eDPAD       = Pools.pool.GetGroup(Matcher.DpadEvent  );
      var eButton     = Pools.pool.GetGroup(Matcher.ButtonEvent);
      var eFullTilt   = Pools.pool.GetGroup(Matcher.AnyOf(Matcher.DpadEvent, Matcher.ButtonEvent));
      //      scoreTxt.text = score.GetSingleEntity().componentNames[0] + " ";

      eDPAD.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        this.txt_DPAD.text = entity.dpadEvent.eDirn.ToString();
      };

      eButton.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        this.txt_Button.text = entity.buttonEvent.bType.ToString();
      };

      eFullTilt.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        //        if(group.ContainsEntity(Matcher.DpadEvent)){
        var ioCount = 0;
        foreach (var e in eFullTilt.GetEntities()) {
          if(e.hasDpadEvent){
            ioCount++;
          }
          if(e.hasButtonEvent){
            ioCount++;
          }
//          if(e.hasDpadEvent && e.hasButtonEvent){ // doesn't work
          if(ioCount == 2){
            Debug.LogFormat("FULLTILT {0} {1} ", ioCount, e);
            this.txt_Button.text = "FULL";
            this.txt_DPAD.text   = "FULL";
          }
        }
        //        }

      };

    }

  }

}
