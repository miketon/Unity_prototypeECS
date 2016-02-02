using Entitas;
using MTON;

public class DpadEventComponent : IComponent {

  public int         ID = -1 ; // -1 == uninit
  public _enum.Dirn  eDirn   ;
  public float       magDr   ; // magnitude of Dir

}
