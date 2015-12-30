using UnityEngine;
using System.Collections;
using Entitas;

public class gameController : MonoBehaviour {

	private Entity _e;

	// Use this for initialization
	void Start () {
		var pool = Pools.pool;
		pool.GetGroup(Matcher.Position);
//		pool.GetGroup(Matcher.AllOf(Matcher.Position)); // what does this do???
		_e = pool.CreateEntity();
		_e.AddPosition(0.0f, 3.0f, 1.9f);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.LogFormat("Key Pressed : {0} ", KeyCode.Space)	;
			_e.ReplacePosition(0.0f, 0.0f, 0.0f);
		}
		else if(Input.GetKeyUp(KeyCode.Space)){
			Debug.LogFormat("Key Released : {0} ", KeyCode.Space)	;
			_e.ReplacePosition(1.0f, 1.0f, 1.0f);
		}
	}
	
}
