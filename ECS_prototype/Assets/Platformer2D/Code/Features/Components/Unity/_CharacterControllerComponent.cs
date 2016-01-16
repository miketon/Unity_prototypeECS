using UnityEngine;
using Entitas;

public class _CharacterControllerComponent : IComponent {
  public CharacterController body;

  private float center = 11.25f;
  public float Center{
    get{
      return this.center;
    }
    set{
      this.center = value;
    }
  }

}
