using UnityEngine                ;
using System.Collections         ;
using System.Collections.Generic ;
using MTON                       ;
using Entitas                    ;

public class Event_Audio_uiSystem : IInitializeSystem, IReactiveSystem {

  private SfxrSynth sOnPress   ;
  private SfxrSynth sOnHold    ;
  private SfxrSynth sOnCombo   ;
  private SfxrSynth sOnNeutral ;

  private _enum.GPAD prevGPAD = _enum.GPAD.Neutral;

#region IInitializeSystem implementation
  public void Initialize() {
    Debug.LogFormat("Event_Audio_uiSystem : INIT SYNTH! ");
    Debug.Log("Key: F (Random coin/pickup sound, automatically generated)");
    this.sOnPress   = new SfxrSynth();
    this.sOnNeutral = new SfxrSynth();
    this.sOnCombo   = new SfxrSynth();
    this.sOnPress.parameters.GenerateJump();
    this.sOnPress.CacheSound();
    this.sOnNeutral.parameters.GenerateBlipSelect();
    this.sOnNeutral.parameters.masterVolume = 0.05f;
    this.sOnNeutral.CacheSound();
//    this.sOnCombo.parameters.GeneratePickupCoin();
    this.sOnCombo.parameters.SetSettingsString("0,,0.032,0.4138,0.4365,0.834,,,,,,0.3117,0.6925,,,,,,1,,,,,0.5");
    this.sOnCombo.CacheSound();
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
      if(e.hasGpadEvent){
        if(e.gpadEvent.gpad == _enum.GPAD.FULL){
          if(e.gpadEvent.gpad != this.prevGPAD){
            this.sOnCombo.Play();
          }
        }
        this.prevGPAD = e.gpadEvent.gpad;
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
//      return Matcher.ButtonEvent.OnEntityAdded();
        return Matcher.AnyOf(Matcher.DpadEvent, Matcher.ButtonEvent, Matcher.GpadEvent).OnEntityAdded();
    }
  }
#endregion

}
