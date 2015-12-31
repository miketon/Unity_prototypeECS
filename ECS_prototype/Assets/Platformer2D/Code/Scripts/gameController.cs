using UnityEngine;
using System.Collections;
using Entitas;
using Entitas.Unity.VisualDebugging;

public class gameController : MonoBehaviour {

	private Entity  _e;
	private Systems _systems;

	// Use this for initialization
	void Start () {
		var pool = Pools.pool;
		pool.GetGroup(Matcher.Position);
//		pool.GetGroup(Matcher.AllOf(Matcher.Position)); // what does this do???
		_e = pool.CreateEntity();
		_e.AddPosition(0.0f, 3.0f, 1.9f);

		_systems = createSystems(Pools.pool);
		_systems.Initialize();

	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			_e.ReplacePosition(0.0f, 0.0f, 0.0f);
		}
		else if(Input.GetKeyUp(KeyCode.Space)){
			_e.ReplacePosition(1.0f, 1.0f, 1.0f);
		}

		_systems.Execute();
	}

	Systems createSystems(Pool pool){
		#if (UNITY_EDITOR)
		return new DebugSystems()
		#else
		return new Systems()
		#endif	

		// Input
		.Add(pool.CreateSystem<InputPressSystem>())

		// Update
		.Add(pool.CreateSystem<LevelSystem>())

		// Render
		.Add(pool.CreateSystem<AddViewSystem>());
		
	}
	
}
