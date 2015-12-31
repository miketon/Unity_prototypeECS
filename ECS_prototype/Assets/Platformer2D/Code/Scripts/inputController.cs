using UnityEngine;
using System.Collections;

public class inputController : MonoBehaviour {

	public KeyCode fire = KeyCode.LeftControl;
	public KeyCode jump = KeyCode.Space;

	private bool bPress = false;

	// Update is called once per frame
	void Update () {
		var bFire = Input.GetKey(fire);
		var bJump = Input.GetKey(jump);
		if(bFire || bJump){
			Pools.pool.CreateEntity().AddInputPress(bFire, bJump, false);
			bPress = true; // Pressing buttons
		}
		else if(bPress){
			Pools.pool.CreateEntity().AddInputPress(bFire, bJump, true);
			bPress = false; // Releasing buttons
		}
	}
}
