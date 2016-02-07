using UnityEngine;
using Entitas;

public class _LevelInitSystem : IInitializeSystem, ISetPool {

  public  static Entity player ;
  private        Pool   _pool  ;

  #region IInitializeSystem implementation
  public void Initialize (){
    Debug.LogFormat("Initializing Level : {0} ", this);
    player = _pool.spawnPlayer(1, Vector3.up * 2.0f, true) ; //spawn Player One
    player = _pool.spawnPlayer(2, Vector3.right * 2.0f); //spawn Player Two
    player = _pool.spawnPlayer(3, Vector3.left * 2.0f) ; //spawn Player Three
    player = _pool.spawnPlayer(4, Vector3.left * 3.0f) ; //spawn Player Three

    _GravityComponent.dir              = Physics.gravity ;
    _GravityComponent.magnitude        = 1.0f            ;
    _GravityComponent.terminalVelocity = 54.0f           ; // 54 = a skydiver free-fall to earth according to wikipedia
    _GravityComponent.accleration      = 0.035f          ;

  }
  #endregion

  #region ISetPool implementation
  public void SetPool (Pool pool){
    _pool = pool;
  }
  #endregion

}
