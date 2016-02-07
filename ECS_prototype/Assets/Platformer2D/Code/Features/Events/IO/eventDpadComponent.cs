using Entitas;
using MTON;

public class eventDpadComponent : IComponent {

  public int         ID = -1 ; // -1 == uninit
  public _enum.Dirn  dpad    ;
  public float       mdir    ; // magnitude of Dir

}
