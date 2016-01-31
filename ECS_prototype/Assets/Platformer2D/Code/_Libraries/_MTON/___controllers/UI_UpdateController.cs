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
//      var eFullTilt   = Pools.pool.GetGroup(Matcher.AnyOf(Matcher.DpadEvent, Matcher.ButtonEvent));
      var eFullTilt   = Pools.pool.GetGroup(Matcher.GpadEvent);
      var eFirstRelease = Pools.pool.GetGroup(Matcher.IO_OnFirstRelease);
      //      scoreTxt.text = score.GetSingleEntity().componentNames[0] + " ";

      eDPAD.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        this.txt_DPAD.text = entity.dpadEvent.eDirn.ToString();
      };

      eButton.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        this.txt_Button.text = entity.buttonEvent.bType.ToString();
      };

      eFullTilt.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        var ioCount = 0;
        foreach (var e in eFullTilt.GetEntities()) {
//          if(e.hasDpadEvent){
//            if(e.dpadEvent.ID > 0){
//              ioCount++;
//            }
//          }
//          if(e.hasButtonEvent){
//            ioCount++;
//          }
//          if(ioCount == 2){
            if(e.gpadEvent.gpad == _enum.GPAD.FULL){
            Debug.LogFormat("FULLTILT {0} {1} ", ioCount, e.gpadEvent.gpad);
            this.txt_Button.text = "FULL";
            this.txt_DPAD.text   = "FULL";
            }
//          }
        }
      };

      eFirstRelease.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        // Setting ID == -2, menu triggered event
        Pools.pool.CreateEntity().AddDpadEvent(-2, _enum.Dirn.Neutral, 0.0f)                   ;
        Pools.pool.CreateEntity().AddButtonEvent(-2, _enum.Button.Neutral, _enum.Type.Neutral) ;
      };

    }

  }

}
