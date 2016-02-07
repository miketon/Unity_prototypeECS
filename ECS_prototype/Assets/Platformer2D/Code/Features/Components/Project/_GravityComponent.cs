using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

[SingleEntity]
public class _GravityComponent : IComponent {

  public static Vector3 dir       ;
  public static float   magnitude ;

  public static float   terminalVelocity ; // Terminal velocity : 54 = a skydiver free-fall to earth according to wikipedia
  public static float   accleration      ; // suggested default == 0.035f

}
