using UnityEngine;
using System.Collections;
using System;
using MTON;

public class inputController : MonoBehaviour {

	public string  hAxis  = "Horizontal"      ;
	public string  vAxis  = "Vertical"        ;
  public float   _mAxis =  0.0f             ; // make private serialize

	public KeyCode bFire = KeyCode.LeftControl;
	public KeyCode bJump = KeyCode.Space      ;

  [Flags] // Powers of two
  public enum GPAD {
    // Decimal              // Binary
    Neutral  = 0,           // 000000
    DPAD     = 1,           // 000001
    BTTN     = 2,           // 000010
    
    FULL     = DPAD|BTTN,   // 000011
  }

  private GPAD epad = GPAD.Neutral;
  public  GPAD ePAD = GPAD.Neutral;

  public _enum.Dirn eaxis = _enum.Dirn.Neutral;
  public _enum.Dirn eAxis {
    get{
      return this.eaxis;
    }
    set{
      if(value == _enum.Dirn.Neutral){
        if(value != this.eaxis){
          Pools.pool.CreateEntity().AddDpadEvent(_enum.Dirn.Neutral, 0.0f);
        }
      }
      else{
          Pools.pool.CreateEntity().AddDpadEvent(this.eaxis, this._mAxis);
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
      if (value == _enum.Button.Neutral){
        if(value != this.ebutton){
          Pools.pool.CreateEntity().AddButtonEvent(_enum.Button.Neutral, _enum.Type.Neutral);
//          Debug.LogFormat("BUTTON : NEUTRAL {0} {1}", value, ebntype);
        }
      }
      else{
        Pools.pool.CreateEntity().AddButtonEvent(value, _enum.Type.Jump);
//        Debug.LogFormat("BUTTON : PRESSED {0} {1}", value, ebntype);
      }
      this.ebutton = value;
    }
  }

	public string myString = "Babies/Are/Deformed/Eggs/BreakfastJingle.mp3";
	void Start(){
		var printME = MTON._enum.TokenizeAndReturnPath(this.myString, "/");
		Debug.LogFormat("PRINTING THE TRUTH : {0}", printME);
		Debug.Log("ONE LINER: " +System.IO.Path.GetDirectoryName(this.myString));
	}

	// Update is called once per frame
	void Update () {
		// dirPad state
		var _hAxis = Input.GetAxisRaw(hAxis)               ;
		var _vAxis = Input.GetAxisRaw(vAxis)               ;
    _mAxis = (new Vector2(_hAxis, _vAxis).normalized).magnitude ; // magnitude
		var _bAxis = (Mathf.Abs(_mAxis) > Mathf.Epsilon)   ; // bool=>if magnitude > 0 == true
		// button state
		var _bFire = Input.GetKey(bFire) ;
		var _bJump = Input.GetKey(bJump) ;

    this.ePAD = GPAD.Neutral;

		// OnPress Logic 
		if(_bAxis || _bFire || _bJump){                                                       // active : read input
//			Debug.LogFormat("PRESSED : {0} ", onPress);
      // process dpad
			if(_bAxis) {                      // axis is active
        this.ePAD |= GPAD.DPAD;
				// horizontal
				if(_hAxis > 0.0f){
					eAxis |= _enum.Dirn.RT;
				}
				else if(_hAxis < 0.0f){       // must do explicit check otherwise left is default
					eAxis |= _enum.Dirn.LT;
				}
				// vertical
				if(_vAxis > 0.0f){
					eAxis |= _enum.Dirn.UP;
				}
				else if(_vAxis < 0.0f){       // must do explicit check otherwise down is default
					eAxis |= _enum.Dirn.DN;
				}
      }
      // process buttons
			if(_bFire || _bJump) { 
        this.ePAD |= GPAD.BTTN;
        this.eButton = _enum.Button.Down;
        if(_bFire){
          this.ebntype |= _enum.Type.Attack;
        }
        else{
          this.ebntype &= ~ _enum.Type.Attack; //not
        }
        if(_bJump){
          this.ebntype |= _enum.Type.Jump;
        }
        else{
          this.ebntype &= ~ _enum.Type.Jump; //not
        }      
      }
      else       { 
        this.eButton = _enum.Button.Neutral;
        this.ebntype = _enum.Type.Neutral;
      }
			if(this.ePAD != this.epad ){                                   // onFirst Press
				Debug.LogFormat("FIRST PRESSED : {0} ", this.ePAD);
				Pools.pool.CreateEntity().AddIO_OnFirstPress(500.0f);
				Pools.pool.CreateEntity().AddButtonEvent(_enum.Button.Down , _enum.Type.Attack);
			}
		}
		else {                                                                       // neutral :else set default
      this.eAxis   = _enum.Dirn.Neutral;
      this.eButton = _enum.Button.Neutral;
      this.ebntype = _enum.Type.Neutral;			
//      Debug.LogFormat("RELEASE ALL {0} ", MTON._CONSTANTComponent._CAMERA);
//      Pools.pool.CreateEntity().AddIORelease(true, false, false); // Set all Release Events
    }
    this.epad = this.ePAD;
  }
}
