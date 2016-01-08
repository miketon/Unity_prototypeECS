using UnityEngine;
using System.Collections;

public class collisionController : MonoBehaviour {

	void OnCollisionEnter(Collision coll){
		Pools.pool.CreateCollision(coll);
	}

}
