using Entitas;
using MTON;

public class VstateEventComponent : IComponent {

  public int         ID = -1 ; // -1 == uninit
  public _enum.VState vstate = _enum.VState.Ground ;

}
