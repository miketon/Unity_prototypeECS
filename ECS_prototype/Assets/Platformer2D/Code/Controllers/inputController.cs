using UnityEngine;
using System.Collections;
using System;
using MTON;

public class inputController : MonoBehaviour {

	public string     hAxis = "Horizontal"      ;
	public string     vAxis = "Vertical"        ;
  public _enum.Dirn eaxisPRE = _enum.Dirn.Neutral;
  public _enum.Dirn eaxisCUR = _enum.Dirn.Neutral;
  public _enum.Dirn eAxis {
    get{
      return eaxisCUR;
    }
    set{
      eaxisCUR = value;
      if(eaxisCUR == _enum.Dirn.Neutral){
        if(value != eaxisPRE){
          Pools.pool.CreateEntity().AddDpadEvent(_enum.Press.Neutral, _enum.Dirn.Neutral);
        }
      }
      else{
        Pools.pool.CreateEntity().AddDpadEvent(_enum.Press.Down, eaxisCUR);
      }
      eaxisPRE = value;
    }
  }
	public KeyCode bFire = KeyCode.LeftControl;
	public KeyCode bJump = KeyCode.Space;

	// pressStates
	public  IOState onPress ;
	// cache
	public  IOState onpressPREV   ;
	public  IOState onreleasePREV ;

	// Powers of two
	[Flags] 
	public enum IOState {
		// Decimal             // Binary
		None    = 0,           // 000000
		Dir     = 1,           // 000001
		Button  = 2,           // 000010

		Full    = Dir | Button // 000011
	}

	private bool bdir = false;
	public bool bDIR{
		get{ return this.bdir;  }
		set{
			if(value!=this.bdir){
				this.bdir = value;
				if(value==true){
					this.onPress |= IOState.Dir ; //Or ==Set bit
				}
				else{
					this.onPress &= ~IOState.Dir; //Not==Unset bit
				}
			}
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
		var _mAxis = new Vector2(_hAxis, _vAxis).magnitude ; // magnitude
		var _bAxis = (Mathf.Abs(_mAxis) > Mathf.Epsilon)   ; // bool=>if magnitude > 0 == true
		// button state
		var _bFire = Input.GetKey(bFire) ;
		var _bJump = Input.GetKey(bJump) ;
		var _bPrss = (_bFire || _bJump)  ;

		// OnPress Logic 
		if( _bAxis || _bPrss){                                                       // active : read input
//			Debug.LogFormat("PRESSED : {0} ", onPress);
			if(_bAxis) {                      // axis is active
				bDIR = true  ;
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
			else       { bDIR = false ; }
			if(_bPrss) { bBTN = true  ; }
			else       { bBTN = false ; }
			if(this.onPress != this.onpressPREV ){                                   // onFirst Press
				this.onpressPREV = this.onPress ;
//				Debug.LogFormat("FIRST PRESSED : {0} ", onPress);
				Pools.pool.CreateEntity().AddIO_OnFirstPress(500.0f);
				Pools.pool.CreateEntity().AddButtonEvent(_enum.Press.Down , _enum.Type.Attack);
			}
//			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump);
		}
		else {                                                                       // neutral :else set default
			bDIR = false;
			bBTN = false;
			eAxis = _enum.Dirn.Neutral;
			onPress = IOState.None;
			if(this.onPress != this.onpressPREV ){
//				Debug.LogFormat("INPUT NEUTRAL: {0} ", onPress);
				this.onpressPREV = this.onPress ;
//				Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump);
			}
		}

		// OnRelease Logic
		if(this.onPress!= this.onreleasePREV){
			if( _bAxis || _bPrss){                                                       // active : read input
				if(this.onPress==IOState.Button){
//					Debug.LogFormat("RELEASE DIR");
					Pools.pool.CreateEntity().AddIORelease(false, false, true); // Set all Release Events
				}
				else if(this.onPress==IOState.Dir){
//					Debug.LogFormat("RELEASE BUTTON");
					Pools.pool.CreateEntity().AddIORelease(false, true, false); // Set all Release Events
				}
			}
			else{
//				Debug.LogFormat("RELEASE ALL");
        Debug.LogFormat("RELEASE ALL {0} ", MTON._CONSTANTComponent._CAMERA);
        Pools.pool.CreateEntity().AddIORelease(true, false, false); // Set all Release Events
			}
			this.onreleasePREV = this.onPress ;
		}
	}

}
