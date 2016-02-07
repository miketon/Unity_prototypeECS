using UnityEngine;
using Entitas;
using MTON.Interface;

public class _CharacterControllerComponent : IComponent, IRbody, IForce {

  public  int                   ID = -1  ; // -1 == uninit

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
