﻿using UnityEngine        ;
using System.Collections ;
using System             ;
using MTON               ;

public class InputGetController : MonoBehaviour {

  [SerializeField]
  private int io_ID = -1 ;
  public void setID(int ID){
    this.io_ID = ID;
  }

  public string  hAxis  = "Horizontal"      ;
  public string  vAxis  = "Vertical"        ;
  public float   _mAxis =  0.0f             ; // make private serialize

  public KeyCode bFire = KeyCode.LeftControl;
  public KeyCode bJump = KeyCode.Space      ;

  private _enum.GPAD epad = _enum.GPAD.Neutral;
//  public  _enum.GPAD ePAD = _enum.GPAD.Neutral;
  public  _enum.GPAD ePAD {
    get{
      return this.epad;
    }
    set{
      if(value == _enum.GPAD.Neutral){
        if(value != this.epad){
          Pools.pool.CreateEntity().AddeventGamePad(this.io_ID, _enum.GPAD.Neutral);
        }
      }
      else{
        Pools.pool.CreateEntity().AddeventGamePad(this.io_ID, value);
      }
      this.epad = value;
    }
  }

  public _enum.Dirn eaxis = _enum.Dirn.Neutral;
  public _enum.Dirn eAxis {
    get{
      return this.eaxis;
    }
    set{
      if(value == _enum.Dirn.Neutral){
        if(value != this.eaxis){
          Pools.pool.CreateEntity().AddeventDpad(this.io_ID, _enum.Dirn.Neutral, 0.0f);
        }
      }
      else{
          Pools.pool.CreateEntity().AddeventDpad(this.io_ID, value, this._mAxis);
      }
      this.eaxis = value;
    }
  }

  public _enum.Type   ebntype = _enum.Type.Jump;
  public _enum.Button ebutton = _enum.Button.Neutral;
  public _enum.Button eButton {
    get{
      return this.ebutton ;
    }
    set{
      if (value == _enum.Button.Neutral || value == _enum.Button.Release){
        if(value != this.ebutton){
          Pools.pool.CreateEntity().AddeventButton(this.io_ID, value, _enum.Type.Neutral);
//          Debug.LogFormat("BUTTON : NEUTRAL {0} {1}", value, ebntype);
        }
      }
      else{
        if(value == this.ebutton){ // if same as previous mode : holding
          Pools.pool.CreateEntity().AddeventButton(this.io_ID, _enum.Button.Hold, this.ebntype);
        }
        else{                      // else new mode
          Pools.pool.CreateEntity().AddeventButton(this.io_ID, value, this.ebntype);
        }
//        Debug.LogFormat("BUTTON : PRESSED {0} {1}", value, ebntype);
        }
      this.ebutton = value;
    }
  }

  // Update is called once per frame
  void Update () {
    // dirPad state
    var _hAxis = Input.GetAxisRaw(hAxis)               ;
    var _vAxis = Input.GetAxisRaw(vAxis)               ;
    this._mAxis = (new Vector2(_hAxis, _vAxis).normalized).magnitude ; // magnitude
    var _bAxis = (Mathf.Abs(_mAxis) > Mathf.Epsilon)   ; // bool=>if magnitude > 0 == true
    // button state
    var _bFire = Input.GetKey(bFire) ;
    var _bJump = Input.GetKey(bJump) ;
    
    // reset all enums
    var   axis   = _enum.Dirn.Neutral   ; // temp var force release on every frame so that bitwise ops starts from nuetral
    var   btype  = _enum.Type.Neutral   ; // temp var force release on every frame so that bitwise ops starts from nuetral
    var   bmode  = _enum.Button.Neutral ; // temp var force release on every frame so that bitwise ops starts from nuetral
    var   epadt  = _enum.GPAD.Neutral   ;

		// OnPress Logic 
		if(_bAxis || _bFire || _bJump){   // active : read input
//			Debug.LogFormat("PRESSED : {0} ", onPress);
      // process dpad
			if(_bAxis) {                    // axis is active
        epadt |= _enum.GPAD.DPAD;
				// horizontal
				if(_hAxis > 0.0f){
					axis |= _enum.Dirn.RT;
				}
				else if(_hAxis < 0.0f){       // must do explicit check otherwise left is default
					axis |= _enum.Dirn.LT;
				}
				// vertical
				if(_vAxis > 0.0f){
					axis |= _enum.Dirn.UP;
				}
				else if(_vAxis < 0.0f){       // must do explicit check otherwise down is default
					axis |= _enum.Dirn.DN;
				}
      }
      else{
        axis = _enum.Dirn.Neutral;
      }

      // process buttons
			if(_bFire || _bJump) {          // pressed
        epadt   |= _enum.GPAD.BTTN   ; 
        bmode   |= _enum.Button.Down ;
        if(_bFire){
          btype |= _enum.Type.Attack;
        }
        if(_bJump){
          btype |= _enum.Type.Jump;
        }
      }
      else{                           // released
        btype = _enum.Type.Neutral   ;
        bmode = _enum.Button.Neutral;
//        bmode = _enum.Button.Release ;
      }

      this.eAxis   = axis ;
      this.ebntype = btype;
      this.eButton = bmode;
      this.ePAD    = epadt;

      // process on FirstPressed Events
      if(this.ePAD != this.epad ){    // onFirst Press
//        Debug.LogFormat("FIRST PRESSED : {0} ", this.ePAD)                              ;
        Pools.pool.CreateEntity().Addevent_IO_OnFirstPress(this.io_ID) ;
      }
    }
    // OnRelease Logic
    else {                                                                       
      if(this.ePAD != this.epad ){                                   // onFirst Release
//        Debug.LogFormat("RELEASE ALL {0} ", MTON._CONSTANTComponent._CAMERA);
        Pools.pool.CreateEntity().Addevent_IO_OnFirstRelease(this.io_ID) ;
        Pools.pool.CreateEntity().Addevent_IO_OnRelease(_enum.GPAD.FULL) ;  // Set all Release Events
      }
      this.eAxis   = _enum.Dirn.Neutral   ;
      this.ebntype = _enum.Type.Neutral   ;
      this.eButton = _enum.Button.Neutral ;
      this.ePAD    = _enum.GPAD.Neutral   ;
    }
  }
}
