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
        // Init
		.Add(pool.CreateSystem<LevelSystem>())

		// Input
		.Add(pool.CreateSystem<IO_OnFirstPressSystem>()) // When direction is first pressed : boost acceleration to make char feel snappier
		.Add(pool.CreateSystem<IO_OnPressSystem>())
		.Add(pool.CreateSystem<IO_OnReleaseSystem>())

		// Update
//		.Add(pool.CreateSystem<MoveSystem>())

		// Physics
		.Add(pool.CreateSystem<AddRigidBodySystem>())
		.Add(pool.CreateSystem<rbodySystem>())
		.Add(pool.CreateSystem<OnCollisionEnterSystem>())

		// Render
		.Add(pool.CreateSystem<AddViewSystem>())
		.Add(pool.CreateSystem<RenderPositionSystem>())

		// Destroy
		.Add(pool.CreateSystem<IO_OnDestroySystem>()); //this destroys IO Entities
		
	}
	
}
