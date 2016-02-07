using Entitas;
using UnityEngine;

public class eventOnCollisionComponent : IComponent {
  public int ID = -1         ; // -1 == uninit
  public Collision collision ;
}
