using UnityEngine;
using System.Collections;
using System;

public class inputController : MonoBehaviour {

	public string hAxis = "Horizontal";
	public string vAxis = "Vertical";

	public KeyCode bFire = KeyCode.LeftControl;
	public KeyCode bJump = KeyCode.Space;

	public  OnPressState rState     ;
	public  OnPressState rStatePrev ;

	// Powers of two
	[Flags] public enum OnPressState {
		// Decimal     // Binary
		None    = 0,    // 000000
		Dir     = 1,    // 000001
		Button  = 2,    // 000010

		Full    = Dir | Button // 000011
	}

	private bool bRLpad = false;
	private bool bRLdir = false;
	private bool bRLbtn = false;

	private bool bdir = false;
	public bool bDIR{
		get{ return this.bdir;  }
		set{
			if(value!=this.bdir){
				this.bdir = value;
				this.bRLdir = !value;
				rState = OnPressState.Dir ;
			}
		}
	}

	private bool bbtn = false;
	public bool bBTN{
		get{ return this.bbtn;  }
		set{
			if(value!=this.bbtn){
				this.bbtn = value;
				this.bRLbtn = !value;
				rState = OnPressState.Button;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		var _hAxis = Input.GetAxisRaw(hAxis);
		var _vAxis = Input.GetAxisRaw(vAxis);
		var _axisM = new Vector2(_hAxis, _vAxis).magnitude;
		var _bDirn = (Mathf.Abs(_axisM) > Mathf.Epsilon);
		var _bFire = Input.GetKey(bFire);
		var _bJump = Input.GetKey(bJump);
		var _bPrss = (_bFire || _bJump);
		if( _bDirn || _bPrss){
			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump);
			if(_bDirn) { bDIR = true  ; }
			else       { bDIR = false ; }
			if(_bPrss) { bBTN = true  ; }
			else       { bBTN = false ; }
		}
		else {
			bDIR = false;
			bBTN = false;
			rState = OnPressState.None;
			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump); //Set GamePad to Neutral
		}
		if(this.rState != this.rStatePrev ){
			Pools.pool.CreateEntity().AddIORelease(this.bRLpad, this.bRLdir, this.bRLbtn); // Set all Release Events
			this.rStatePrev = this.rState ;
		}
	}
}
