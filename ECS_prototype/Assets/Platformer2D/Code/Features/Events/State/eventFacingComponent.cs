using Entitas;

public class eventFacingComponent : IComponent{

  public int         ID = -1 ; // -1 == uninit
  public MTON._enum.FState fstate = MTON._enum.FState.Fwrd;

}
