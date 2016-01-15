using UnityEngine;
using System.Collections;
using System;
using MTON;

public class inputController : MonoBehaviour {

  // Powers of two
  [Flags] 
  public enum IOState {
    // Decimal             // Binary
    None    = 0,           // 000000
    Dir     = 1,           // 000001
    Button  = 2,           // 000010

    Full    = Dir | Button // 000011
  }

	public string  hAxis  = "Horizontal"      ;
	public string  vAxis  = "Vertical"        ;
  public float   _mAxis =  0.0f             ; // make private serialize

	public KeyCode bFire = KeyCode.LeftControl;
	public KeyCode bJump = KeyCode.Space      ;

	// pressStates
	public  IOState onPress ;
	// cache
	public  IOState onpressPREV   ;
	public  IOState onreleasePREV ;

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
//      if(value == _enum.Button.Release){
//        if(value != this.ebutton){
//          Pools.pool.CreateEntity().AddButtonEvent(_enum.Button.Release, _enum.Type.Neutral);
//          Debug.LogFormat("BUTTON : RELEASED {0} {1}", value, ebntype);
//          this.eButton = _enum.Button.Neutral;
//        }
//      }
      if (value == _enum.Button.Neutral){
        if(value != this.ebutton){
          Pools.pool.CreateEntity().AddButtonEvent(_enum.Button.Neutral, _enum.Type.Neutral);
          Debug.LogFormat("BUTTON : NEUTRAL {0} {1}", value, ebntype);
        }
      }
      else{
        Pools.pool.CreateEntity().AddButtonEvent(value, _enum.Type.Jump);
                  Debug.LogFormat("BUTTON : PRESSED {0} {1}", value, ebntype);
      }
      this.ebutton = value;
    }
  }

  private bool bbtn = false;
	public bool bBTN{
		get{ return this.bbtn;  }
		set{
			if(value!=this.bbtn){
				this.bbtn = value;
				if(value==true){
					this.onPress |= IOState.Button  ; //Or ==Set bit
				}
				else{
					this.onPress &= ~IOState.Button ; //Not==Unset bit
				}
			}
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
		var _bPrss = (_bFire || _bJump)  ;

		// OnPress Logic 
		if( _bAxis || _bPrss){                                                       // active : read input
//			Debug.LogFormat("PRESSED : {0} ", onPress);
      // process dpad
			if(_bAxis) {                      // axis is active
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
			if(_bPrss) { 
        bBTN = true  ;
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
        bBTN = false ;
        this.eButton = _enum.Button.Neutral;
        this.ebntype = _enum.Type.Neutral;
      }
			if(this.onPress != this.onpressPREV ){                                   // onFirst Press
				this.onpressPREV = this.onPress ;
//				Debug.LogFormat("FIRST PRESSED : {0} ", onPress);
				Pools.pool.CreateEntity().AddIO_OnFirstPress(500.0f);
				Pools.pool.CreateEntity().AddButtonEvent(_enum.Button.Down , _enum.Type.Attack);
			}
		}
		else {                                                                       // neutral :else set default
			bBTN = false;
			if(this.onPress != this.onpressPREV ){
//				Debug.LogFormat("INPUT NEUTRAL: {0} ", onPress);
        eAxis = _enum.Dirn.Neutral;
//        if(this.eButton==_enum.Button.Release){
          this.eButton = _enum.Button.Neutral;
          this.ebntype = _enum.Type.Neutral;
//        }
        this.onPress = IOState.None;
        this.onpressPREV = this.onPress ;
			}
		}

		// OnRelease Logic
		if(this.onPress!= this.onreleasePREV){
      Debug.LogFormat("RELEASE ALL {0} ", MTON._CONSTANTComponent._CAMERA);
      Pools.pool.CreateEntity().AddIORelease(true, false, false); // Set all Release Events
			this.onreleasePREV = this.onPress ;
		}
	}

}
