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
      if(e.haseventButton){
        if(e.eventButton.bMode == _enum.Button.Down){
          if(e.eventButton.bType == _enum.Type.Jump){
            this.sOnPress.Play();
          }
        }
        else if(e.eventButton.bMode == _enum.Button.Neutral){
          this.sOnNeutral.Play();
        }
      }
      if(e.haseventDpad){
        if(e.eventDpad.eDirn == _enum.Dirn.Neutral){
          this.sOnNeutral.Play();
        }
      }
      if(e.haseventGamePad){
        if(e.eventGamePad.gpad == _enum.GPAD.FULL){
          if(e.eventGamePad.gpad != this.prevGPAD){
            this.sOnCombo.Play();
          }
        }
        this.prevGPAD = e.eventGamePad.gpad;
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.AnyOf(Matcher.eventDpad, Matcher.eventButton, Matcher.eventGamePad).OnEntityAdded();
    }
  }
#endregion

}
