using UnityEngine        ;
using System             ;
using System.Collections ;

namespace MTON{
  public class _struct {

#region Pooling Structs

    [Serializable] //MUST : add so that this custom data type can be displayed in the inspector
    public struct s_GameObjects {

      public Transform[]      e_Players ; // bullet Entities
      public Transform[]      e_Bullets ; // bullet Entities
      public Transform[]      e_Icons   ;

    }

    [Serializable] //MUST : add so that this custom data type can be displayed in the inspector
    public struct s_GameEntities {

      public Transform[]      e_GroundE ; // ground Entities
      public Transform[]      e_FlyingE ; // flying Entities
      public Transform[]      e_Pillars ; // pillar Entities : slams : thompers, groundspikes, doors..etc

    }

    [Serializable] //MUST : add so that this custom data type can be displayed in the inspector
    public struct s_FXObjects {

      public Animator[]       anmEmit ;
      public ParticleSystem[] fx_Hits ;

      public float[]          fx_Hit_OffSet  ; // To place effect at feet = particle size(radius)
      public float[]          anmEmit_duratn ; // Duration of animation clip
      public Vector3[]        anmEmit_IntScl ; // captures scale of animator objects

    }
#endregion

  }
}