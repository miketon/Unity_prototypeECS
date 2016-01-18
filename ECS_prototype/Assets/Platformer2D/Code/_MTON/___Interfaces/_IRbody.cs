using UnityEngine        ;

namespace MTON.Interface{

  public interface IRbody{

    Vector3    center { get; set; } // center point
    float      height { get; set; } // height
    float      radius { get; set; } // radius
    Quaternion initRo { get; set; } // initRotation

  }

}