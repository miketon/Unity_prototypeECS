using Entitas ;
using System  ;
using MTON    ;

public class eventButtonComponent : IComponent {

  public int          ID = -1 ; // -1 == uninit
  public _enum.Button bMode   ; // button press state
  public _enum.Type   bType   ; // button press type 

}
