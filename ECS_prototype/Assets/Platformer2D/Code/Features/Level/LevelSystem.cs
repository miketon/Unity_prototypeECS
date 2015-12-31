using UnityEngine;
using Entitas;

public class LevelSystem : IInitializeSystem, ISetPool {

	private Pool _pool;
	private Group _playerElements;
	
	#region IInitializeSystem implementation
	public void Initialize (){
		Debug.LogFormat("Initializing Level : {0} ", this);
		_pool.spawnPlayer(Vector3.zero);
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool = pool;
		_playerElements = _pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.Position));
	}
	#endregion

}
