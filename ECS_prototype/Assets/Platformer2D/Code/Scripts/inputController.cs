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
	public  IOState onpressPREV ;

	// releaseStates
	public  IOState onRelease ;
	public  IOState onreleasePREV ;

	// Powers of two
	[Flags] public enum IOState {
		// Decimal     // Binary
		None    = 0,    // 000000
		Dir     = 1,    // 000001
		Button  = 2,    // 000010

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
					onPress |= IOState.Dir ; //Or ==Set bit
				}
				else{
					onPress &= ~IOState.Dir; //Not==Unset bit
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
					onPress |= IOState.Button  ; //Or ==Set bit
				}
				else{
					onPress &= ~IOState.Button ; //Not==Unset bit
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		// dirPad state
		var _hAxis = Input.GetAxisRaw(hAxis);
		var _vAxis = Input.GetAxisRaw(vAxis);
		var _axisM = new Vector2(_hAxis, _vAxis).magnitude;
		var _bDirn = (Mathf.Abs(_axisM) > Mathf.Epsilon);
		// button state
		var _bFire = Input.GetKey(bFire);
		var _bJump = Input.GetKey(bJump);
		var _bPrss = (_bFire || _bJump);

		// OnPress Logic
		if( _bDirn || _bPrss){
			if(_bDirn) { bDIR = true  ; }
			else       { bDIR = false ; }
			if(_bPrss) { bBTN = true  ; }
			else       { bBTN = false ; }
			if(this.onPress != this.onpressPREV ){
				this.onpressPREV = this.onPress ;
				Debug.LogFormat("FIRST PRESSED : {0} ", onPress);
			}
			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump);
		}
		// OnRelease Logic
		else {
			bDIR = false;
			bBTN = false;
			onPress = IOState.None;
			if(this.onPress != this.onpressPREV ){
				this.onpressPREV = this.onPress ;
				Debug.LogFormat("RELEASED : {0} ", onPress);
				Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump);
				Pools.pool.CreateEntity().AddIORelease(this.bRLpad, !this.bDIR, !this.bBTN); // Set all Release Events
			}
		}

	}
}
