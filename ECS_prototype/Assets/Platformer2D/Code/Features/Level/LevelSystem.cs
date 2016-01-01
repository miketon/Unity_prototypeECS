using UnityEngine;
using Entitas;

public class LevelSystem : IInitializeSystem, ISetPool, IReactiveSystem {

	private Pool _pool;
	private Group _playerElements;

	#region IReactiveExecuteSystem implementation
	public void Execute (System.Collections.Generic.List<Entity> entities){
		foreach (var e in entities) {
			Debug.LogFormat("Level Spawned : {0} ", e);
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.GameObject.OnEntityAdded();
		}
	}
	#endregion
	
	#region IInitializeSystem implementation
	public void Initialize (){
		Debug.LogFormat("Initializing Level : {0} ", this);
		_pool.spawnPlayer(Vector3.zero);
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool = pool;
		_playerElements = _pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.GameObject));
	}
	#endregion

}
