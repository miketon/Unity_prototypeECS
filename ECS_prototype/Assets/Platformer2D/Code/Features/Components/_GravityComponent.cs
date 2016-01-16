using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

[SingleEntity]
public class _GravityComponent : IComponent {
	public static Vector3 dir       ;
	public static float   magnitude ;
}
