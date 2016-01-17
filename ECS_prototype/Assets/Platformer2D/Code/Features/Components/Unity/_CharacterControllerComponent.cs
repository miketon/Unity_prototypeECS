using UnityEngine;
using Entitas;

public class _CharacterControllerComponent : IComponent, MTON.Interface.IRbody {

  public CharacterController body;

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

  public Quaternion initRot { get; set;}

}

/* Contstructor doesn't initialize correctly
  public _CharacterControllerComponent() {
    Init(body);
    Debug.LogFormat("_CharacterControllerComponent : {0} {1} {2}", this.center, this.height, this.radius);
  }
*/
