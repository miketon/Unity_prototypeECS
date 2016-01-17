using UnityEngine;
using System.Collections;
using Entitas;

public class _CharacterControllerSystem : IExecuteSystem, ISetPool, IInitializeSystem {

  private Group     _group       ;
  private LayerMask _layerGround ;
  private Transform _xform       ;

  #region IExecuteSystem implementation
  public void Execute() {
    foreach (var e in _group.GetEntities()){
//     Debug.LogFormat("EXECUTING : {0}", e);
      var cc = e._CharacterController;
      OnGround(cc);
    }
  }
  #endregion

  #region ISetPool implementation
  public void SetPool(Pool pool) {
    _group = pool.GetGroup(Matcher.AllOf(Matcher._CharacterController));
  }
  #endregion

  #region IInitializeSystem implementation
  public void Initialize() {
    this._layerGround = LayerMask.GetMask (MTON._CONSTANTComponent._FLOOR);
    _xform = new GameObject("MTON_CHARACTERCONTROLLER").transform;
//    Debug.LogFormat("INITIALIZING _CharacterControllerSystem : LAYERGROUND {0}", this._layerGround);
  }
  #endregion

  //Utilities -- Not extending xForm so reimplementing ground logic
  public virtual bool OnGround(_CharacterControllerComponent cc){                                          
    Vector3 vPos = cc.body.transform.position + cc.center                                    ;
    return this.OnGround(vPos, -Vector3.up, new Vector3(cc.radius * 0.85f, cc.height, 0.0f)) ;
  }

  public virtual bool OnGround(Vector3 vPos, Vector3 vDir, Vector3 vCol){                 // vCol: x = cRadius, y = cHeight   
      float bCentCheck = this.dirRayCheck(vPos                            , vDir, vCol.y) ; // check center
      float bRghtCheck = this.dirRayCheck(vPos + ( Vector3.right * vCol.x), vDir, vCol.y) ; // check right edge
      float bLeftCheck = this.dirRayCheck(vPos + (-Vector3.right * vCol.x), vDir, vCol.y) ; // check left edge
    int countCheck = 0;
    if(bCentCheck > 0.0f){
      countCheck=countCheck+2; //center counts more
    }
    if(bLeftCheck > 0.0f){
      countCheck++;
    }
    if(bRghtCheck > 0.0f){
      countCheck++;
    }

    if (countCheck>0){                                //either edge connects, then character is onGround
      if(countCheck<2){ //Not all rays hitting ground; reduce radius of collider
//        contrl.radius = vCol.x * 0.05f ; //reduce radius collider
      }
      else{
//        contrl.radius = vCol.x         ; //else leave at default
      }
        return true;
    }
    else{
      return false;
    }
  }

  public float dirRayCheck(Vector3 vPos, Vector3 vDir, float IN_magnitude){
    return _xform.dirRayCheck(vPos, vDir, IN_magnitude, this._layerGround);
  }


}
