using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using MTON                       ;
using Entitas                    ;

public class Event_Audio_uiSystem : IInitializeSystem, IReactiveSystem {

  private SfxrSynth sOnPress   ;
  private SfxrSynth sOnHold    ;
  private SfxrSynth sOnNeutral ;

#region IInitializeSystem implementation
  public void Initialize() {
    Debug.LogFormat("Event_Audio_uiSystem : INIT SYNTH! ");
    Debug.Log("Key: F (Random coin/pickup sound, automatically generated)");
    this.sOnPress   = new SfxrSynth();
    this.sOnNeutral = new SfxrSynth();
    this.sOnPress.parameters.GenerateJump();
    this.sOnNeutral.parameters.GenerateBlipSelect();
    this.sOnNeutral.parameters.masterVolume = 0.05f;
  }
#endregion

#region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      if(e.hasButtonEvent){
        if(e.buttonEvent.bMode == _enum.Button.Down){
          if(e.buttonEvent.bType == _enum.Type.Jump){
            this.sOnPress.Play();
          }
        }
        else if(e.buttonEvent.bMode == _enum.Button.Neutral){
          this.sOnNeutral.Play();
        }
      }
      if(e.hasDpadEvent){
        if(e.dpadEvent.eDirn == _enum.Dirn.Neutral){
          this.sOnNeutral.Play();
        }
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
//      return Matcher.ButtonEvent.OnEntityAdded();
        return Matcher.AnyOf(Matcher.DpadEvent, Matcher.ButtonEvent).OnEntityAdded();
    }
  }
#endregion

}
