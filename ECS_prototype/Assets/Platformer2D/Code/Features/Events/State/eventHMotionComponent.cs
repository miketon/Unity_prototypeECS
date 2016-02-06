using Entitas;

public class eventHMotionComponent : IComponent {

  public int         ID = -1 ; // -1 == uninit
  public MTON._enum.HState hstate = MTON._enum.HState.Neutral;

}
