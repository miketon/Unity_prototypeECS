﻿using Entitas;
using Entitas.CodeGenerator;
using UnityEngine;

[SingleEntity]
public class GravityComponent : IComponent {
	public static Vector3 dir       ;
	public static float   magnitude ;
}
