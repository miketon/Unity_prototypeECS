using UnityEngine;
using System.Collections;

public class inputController : MonoBehaviour {

	public string hAxis = "Horizontal";
	public string vAxis = "Vertical";

	public KeyCode bFire = KeyCode.LeftControl;
	public KeyCode bJump = KeyCode.Space;

	private bool bRLpad = false;
	private bool bRLdir = false;
	private bool bRLbtn = false;
	private bool[] brelease;
	private bool[] bRelease = new bool[3]{false, false, false};

	private bool bpad = false;
	public bool bPAD{
		get{ return this.bpad;  }
		set{
			if(value!=this.bpad){
				this.bpad = value;
//				Debug.LogFormat("Release : ALL : {0}", !value);
				this.bRLpad = !value;
				this.bRelease[0] = !value;
			}
		}
	}

	private bool bdir = false;
	public bool bDIR{
		get{ return this.bdir;  }
		set{
			if(value!=this.bdir){
				this.bdir = value;
//				Debug.LogFormat("Release : DIR : {0}", !value);
				this.bRLdir = !value;
				this.bRelease[1] = !value;
			}
		}
	}

	private bool bbtn = false;
	public bool bBTN{
		get{ return this.bbtn;  }
		set{
			if(value!=this.bbtn){
				this.bbtn = value;
//				Debug.LogFormat("Release : BTN : {0}", !value);
				this.bRLbtn = !value;
				this.bRelease[2] = !value;
			}
		}
	}

	void Start(){
		this.brelease = this.bRelease;
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
			bPAD = true; // Pressing buttons
			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump);
			if(_bDirn) { bDIR = true  ; }
			else       { bDIR = false ; }
			if(_bPrss) { bBTN = true  ; }
			else       { bBTN = false ; }
		}
		else {
			bPAD = false; // Releasing buttons
			bDIR = false;
			bBTN = false;
			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump); //Set GamePad to Neutral
		}
		if(this.brelease != this.bRelease){
			Debug.LogFormat("RELEASE : {0} {1}", this.brelease, this.bRelease );
			Pools.pool.CreateEntity().AddIORelease(this.bRLpad, this.bRLdir, this.bRLbtn); // Set all Release Events
			this.brelease = this.bRelease;
		}
	}
}
