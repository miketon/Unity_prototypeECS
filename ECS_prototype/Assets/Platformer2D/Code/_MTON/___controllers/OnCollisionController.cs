using UnityEngine;
using System.Collections;

public class OnCollisionController : MonoBehaviour {

  void OnCollisionEnter(Collision coll){
    Pools.pool.CreateCollision(coll);
  }

}
