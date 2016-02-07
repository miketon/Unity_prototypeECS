using UnityEngine;
using Entitas;

public class _LevelInitSystem : IInitializeSystem, ISetPool {

  public  static Entity player ;
  private        Pool   _pool  ;

  #region IInitializeSystem implementation
  public void Initialize (){
    Debug.LogFormat("Initializing Level : {0} ", this);
    player = _pool.spawnPlayer(1, Vector3.up * 2.0f); //spawn Player One

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
