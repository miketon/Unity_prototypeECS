using Entitas;
using MTON;

public class eventDpadComponent : IComponent {

  public int         ID = -1 ; // -1 == uninit
  public _enum.Dirn  eDirn   ;
  public float       magDr   ; // magnitude of Dir

}
