using UnityEngine;
using Entitas;

public class _OnLevelInitSystem : IInitializeSystem, ISetPool {

  public static Entity player;
  private Pool _pool;

	#region IInitializeSystem implementation
	public void Initialize (){
		Debug.LogFormat("Initializing Level : {0} ", this);
		player = _pool.spawnPlayer(Vector3.zero);
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool = pool;
	}
	#endregion

}
