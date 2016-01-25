using UnityEngine;

namespace MTON.Interface{

  public interface IForce {

    Vector3  vMove { get; set; } // vector move
    Vector3  vGrav { get; set; } // vector gravity
    float    fMass { get; set; }

  }

}
