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

		_systems = createSystems(pool);
		_systems.Initialize();

	}

	void Update(){
		_systems.Execute();
	}

	Systems createSystems(Pool pool){
		#if (UNITY_EDITOR)
		return new DebugSystems()
		#else
		return new Systems()
		#endif	

		.Add(pool.CreateSystem<LevelSystem>())
		// Input
		.Add(pool.CreateSystem<IO_ForceSystem>())

		// Update
//		.Add(pool.CreateSystem<MoveSystem>())

		// Render
		.Add(pool.CreateSystem<AddViewSystem>())
		.Add(pool.CreateSystem<RenderPositionSystem>())

		// Destroy
		.Add(pool.CreateSystem<IODestrolSystem>()); //this destroys IO Entities
		
	}
	
}
