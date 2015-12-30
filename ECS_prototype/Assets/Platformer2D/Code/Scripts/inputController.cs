using UnityEngine;
using System.Collections;

public class inputController : MonoBehaviour {

	public KeyCode fire = KeyCode.Space;

	// Update is called once per frame
	void Update () {
		var bFire = Input.GetKey(fire);
		if(bFire){
			Pools.pool.CreateEntity().AddInput(bFire, false);
		}
	}
}
