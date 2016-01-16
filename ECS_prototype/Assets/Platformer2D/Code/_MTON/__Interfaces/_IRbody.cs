using UnityEngine        ;

namespace MTON.Interface{

  #region Mton Interface - IRbody
  public interface IRbody{

    Vector3   center { get; set; } // center point
    float     height { get; set; } // height
    float     radius { get; set; } // radius

  }
  #endregion

}