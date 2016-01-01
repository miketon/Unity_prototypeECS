using UnityEngine;
using System.Collections;

public class inputController : MonoBehaviour {

	public string hAxis = "Horizontal";
	public string vAxis = "Vertical";

	public KeyCode bFire = KeyCode.LeftControl;
	public KeyCode bJump = KeyCode.Space;

	private bool bPAD = false;
	private bool bDIR = false;
	private bool bBTN = false;

	// Update is called once per frame
	void Update () {
		var _hAxis = Input.GetAxisRaw(hAxis);
		var _vAxis = Input.GetAxisRaw(vAxis);
		var _axisM = new Vector2(_hAxis, _vAxis).magnitude;
		var _bDirn = Mathf.Abs(_axisM) > Mathf.Epsilon;
		var _bFire = Input.GetKey(bFire);
		var _bJump = Input.GetKey(bJump);
		var _bPrss = _bFire || _bJump;
		if( _bDirn || _bPrss){
			bPAD = true; // Pressing buttons
			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump, bPAD);
			if(_bDirn==false){ // Dir Neutral
				if(bPAD==false){
					Pools.pool.CreateEntity().AddIORelease(false, true, false);
					bPAD = true;
				}
			}
			else if(_bPrss==false){ // Button Neutral
				if(bBTN==false){
					Pools.pool.CreateEntity().AddIORelease(false, false, true);
					bBTN = true;
				}	
			}
		}
		else if(bPAD){
			bPAD = false; // Releasing buttons
			bDIR = false;
			bBTN = false;
			Pools.pool.CreateEntity().AddIOGamePad(_hAxis, _vAxis, _bFire, _bJump, bPAD); //Set GamePad to Neutral
			Pools.pool.CreateEntity().AddIORelease(bPAD, bDIR, bBTN); // Set all Release Events
		}
	}
}
