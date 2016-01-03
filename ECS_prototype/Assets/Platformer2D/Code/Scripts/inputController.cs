using UnityEngine;
using System.Collections;
using System;

public class inputController : MonoBehaviour {

	public string hAxis = "Horizontal";
	public string vAxis = "Vertical";

	public KeyCode bFire = KeyCode.LeftControl;
	public KeyCode bJump = KeyCode.Space;

	// pressStates
	public  IOState onPress ;
	// cache
	public  IOState onpressPREV   ;
	public  IOState onreleasePREV ;

	// Powers of two
	[Flags] public enum IOState {
		// Decimal             // Binary
		None    = 0,           // 000000
		Dir     = 1,           // 000001
		Button  = 2,           // 000010

		Full    = Dir | Button // 000011
	}

	private bool bRLpad = false;

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

	// Update is called once per frame
	void Update () {
		// dirPad state
		var _hAxis = Input.GetAxisRaw(hAxis);
		var _vAxis = Input.GetAxisRaw(vAxis);
		var _mAxis = new Vector2(_hAxis, _vAxis).magnitude;
		var _bAxis = (Mathf.Abs(_mAxis) > Mathf.Epsilon);
		// button state
		var _bFire = Input.GetKey(bFire);
		var _bJump = Input.GetKey(bJump);
		var _bPrss = (_bFire || _bJump);

		// OnPress Logic 
		if( _bAxis || _bPrss){                                                       // active : read input
//			Debug.LogFormat("PRESSED : {0} ", onPress);
			if(_bAxis) { bDIR = true  ; }
			else       { bDIR = false ; }
			if(_bPrss) { bBTN = true  ; }
			else       { bBTN = false ; }
			if(this.onPress != this.onpressPREV ){                                   // onFirst Press
				this.onpressPREV = this.onPress ;
//				Debug.LogFormat("FIRST PRESSED : {0} ", onPress);
				Pools.pool.CreateEntity().AddIO_OnFirstPress(500.0f);
			}
			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump);
		}
		else {                                                                       // neutral :else set default
			bDIR = false;
			bBTN = false;
			onPress = IOState.None;
			if(this.onPress != this.onpressPREV ){
//				Debug.LogFormat("INPUT NEUTRAL: {0} ", onPress);
				this.onpressPREV = this.onPress ;
				Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump);
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
				Pools.pool.CreateEntity().AddIORelease(true, false, false); // Set all Release Events
			}
			this.onreleasePREV = this.onPress ;
		}


	}
}
