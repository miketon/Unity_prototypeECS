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

      var eDPAD       = Pools.pool.GetGroup(Matcher.eventDpad  ) ;
      var eButton     = Pools.pool.GetGroup(Matcher.eventButton) ;
      var eFullTilt   = Pools.pool.GetGroup(Matcher.GpadEvent)   ;
      //      scoreTxt.text = score.GetSingleEntity().componentNames[0] + " ";

      eDPAD.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        this.txt_DPAD.text = entity.eventDpad.eDirn.ToString();
      };

      eButton.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        this.txt_Button.text = entity.eventButton.bType.ToString();
      };

      eFullTilt.OnEntityAdded += (Group group, Entity entity, int index, IComponent component) => {
        if(entity.gpadEvent.gpad == _enum.GPAD.FULL){
          this.txt_DPAD.text   = "DPAD"   ;
          this.txt_Button.text = "BUTTON" ;
        }
      };

    }

  }

}
