using UnityEngine;
using System.Collections;

public class inputController : MonoBehaviour {

	public string hAxis = "Horizontal";
	public string vAxis = "Vertical";

	public KeyCode bFire = KeyCode.LeftControl;
	public KeyCode bJump = KeyCode.Space;

	private bool bPressed = false;

	// Update is called once per frame
	void Update () {
		var _hAxis = Input.GetAxisRaw(hAxis);
		var _vAxis = Input.GetAxisRaw(vAxis);
		var _axisM = new Vector2(_hAxis, _vAxis).magnitude;
		var _bFire = Input.GetKey(bFire);
		var _bJump = Input.GetKey(bJump);
		if((Mathf.Abs(_axisM) > Mathf.Epsilon) || _bFire || _bJump){
			bPressed = true; // Pressing buttons
	        Pools.pool.CreateEntity().AddInputButton(_hAxis, _vAxis, _bFire, _bJump, bPressed);
		}
		else if(bPressed){
			bPressed = false; // Releasing buttons
	        Pools.pool.CreateEntity().AddInputButton(_hAxis, _vAxis, _bFire, _bJump, bPressed);
		}
	}
}
