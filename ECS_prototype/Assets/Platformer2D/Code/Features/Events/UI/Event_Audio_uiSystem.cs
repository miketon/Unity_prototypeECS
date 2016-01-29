using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MTON;
using Entitas;

public class Event_Audio_uiSystem : IInitializeSystem, IReactiveSystem {

  private SfxrSynth sOnPress   ;
  private SfxrSynth sOnHold    ;
  private SfxrSynth sOnRelease ;

  #region IInitializeSystem implementation
  public void Initialize() {
    Debug.LogFormat("Event_Audio_uiSystem : INIT SYNTH! ");
    Debug.Log("Key: F (Random coin/pickup sound, automatically generated)");
    this.sOnPress   = new SfxrSynth();
    this.sOnRelease = new SfxrSynth();
    this.sOnPress.parameters.GenerateJump();
    this.sOnRelease.parameters.GenerateBlipSelect();
    this.sOnRelease.parameters.masterVolume = 0.05f;
  }
  #endregion

  #region IReactiveExecuteSystem implementation
  public void Execute(List<Entity> entities) {
    foreach (var e in entities){
      if(e.buttonEvent.bType == _enum.Type.Jump){
        if(e.buttonEvent.bMode == _enum.Button.Down){
          this.sOnPress.Play();
        }
      }
      else if(e.buttonEvent.bType == _enum.Type.Neutral && e.buttonEvent.bMode == _enum.Button.Neutral){
        this.sOnRelease.Play();
      }
    }
  }

  public TriggerOnEvent trigger {
    get {
      return Matcher.ButtonEvent.OnEntityAdded();
    }
  }
  #endregion

}
