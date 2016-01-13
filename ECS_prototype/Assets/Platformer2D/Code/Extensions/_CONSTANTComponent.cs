using UnityEngine;
using Entitas;
using Entitas.CodeGenerator;

namespace MTON {

  [SingleEntity]
  public class _CONSTANTComponent : IComponent {

    //handles Layers - Entities can move
    public const string _CAMERA = "MainCamera" ;
    public const string _PLAYER = "Player" ;
    public const string _ENEMY  = "Enemy"  ;
    public const string _BULLET = "Bullet" ;
    public const string _ITEMS  = "Item"   ;

    //handles Layers - Environments 
    public const string _FLOOR  = "Ground";
    public const string _WALLS  = "Walls" ;
    public const string _DOORS  = "Doors" ; //could be spawn, save, restore, entry, exit points
    public const string _TRGGR  = "Ignore Raycast"; //HACK :level triggers/hint should ignore ground raycast/collision check!

    //handles Layers - FX
    public const string _SPFX   = "Ignore Raycast"; //HACK :level triggers/hint should ignore ground raycast/collision check!

    //handles Tags
    //handles Paths
    public static string _char = "_Characters/" ;
    public static string _sdFX = "_SoundFX/"    ;
    public static string _txtr = "_Textures/"   ;
    public static string _matl = "_Materials/"  ;

  }

}


