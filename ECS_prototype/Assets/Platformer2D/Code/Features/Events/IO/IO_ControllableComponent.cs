using Entitas     ;
using UnityEngine ;

public class IO_ControllableComponent : IComponent { // Set Flag to true for any entity that is currently eligible for IO
  public int ID = -1 ; // -1 == uninit
}
