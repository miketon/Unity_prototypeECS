using UnityEngine;
using Entitas;
using MTON.Interface;

public class _CharacterControllerComponent : IComponent, IRbody, IForce {

  public CharacterController body;
  public Quaternion initRot { get; set;}

  public _CharacterControllerComponent() {
    this.vMove = Vector3.zero        ;
    this.vGrav = Vector3.down * 5.5f ;
    Debug.LogFormat("CONSTRUCTOR : _CharacterControllerComponent : {0} {1}", this.vMove, this.vGrav);
//    Init(body); // Can't do body because this is triggered while body is null
  }

  public void Init(CharacterController body){
    if(body){
      this.center  = body.center;
      this.height  = (body.height * body.transform.localScale.y * 0.5f) + body.skinWidth ; 
      this.radius  = body.radius * body.transform.localScale.x ;
      this.initRot = body.transform.rotation;
    }
  }

  #region IRbody implementation
  public Vector3 center { get; set;}
  public float   height { get; set;}
  public float   radius { get; set;}
  #endregion

  #region IForce implementation
  public Vector3 vMove { get; set;}
  public Vector3 vGrav { get; set;}
  #endregion

}
