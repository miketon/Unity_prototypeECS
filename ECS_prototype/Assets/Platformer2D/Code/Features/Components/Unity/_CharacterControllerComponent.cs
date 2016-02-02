using UnityEngine;
using Entitas;
using MTON;
using MTON.Interface;
using MTON.Controller;

public class _CharacterControllerComponent : IComponent, IRbody, IForce {

  public  int                   ID = -1  ; // -1 == uninit
  public  CharacterController   body     ;
  private CharUpdateController  cControl ;
  private OnCollisionController onColl   ; //fires off vState events, needs ID

  public _CharacterControllerComponent() {
    this.vMove = Vector3.zero        ;
    this.vGrav = Vector3.down * 5.5f ;
    Debug.LogFormat("CONSTRUCTOR : _CharacterControllerComponent : {0} {1}", this.vMove, this.vGrav);
//    Init(body); // Can't do body because this is triggered while body is null
  }

  public void Init(CharacterController body){
    if(body){
      this.center   = body.center;
      this.height   = (body.height * body.transform.localScale.y * 0.5f) + body.skinWidth ; 
      this.radius   = body.radius * body.transform.localScale.x ;
      this.initRo   = body.transform.rotation;
      this.cControl = MTON.__gUtility.AddComponent_mton<CharUpdateController>(this.body.gameObject)  ;
      this.cControl.setID(ID)                                                                        ; //fires off vState events, needs ID
      this.onColl   = MTON.__gUtility.AddComponent_mton<OnCollisionController>(this.body.gameObject) ;
    }
  }

  public void doJump(){
    this.cControl.doJump();
  }

  #region IRbody implementation
  public Vector3    center { get; set; }
  public float      height { get; set; }
  public float      radius { get; set; }
  public Quaternion initRo { get; set; } // initRotation
  #endregion

  #region IForce implementation
  public Vector3 vMove { get; set;}
  public Vector3 vGrav { get; set;}
  public float   fMass { get; set;}
  #endregion

}
