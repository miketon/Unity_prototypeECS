using UnityEngine;
using System;
using System.Collections;

public class CharUpdateController : MonoBehaviour {

  [Flags] // Powers of two
  public enum floorCheck {
    // Decimal                   // Binary
    Neutral  = 0,                // 000000
    MIDL     = 1,                // 000001
    LEFT     = 2,                // 000010
    RGHT     = 3,                // 000100

    MLFT     = MIDL|LEFT,        // 000011 // at ledge right
    MLRT     = MIDL|RGHT,        // 000101 // at ledge left
    MNUL     = LEFT|RGHT,        // 000110 // falling down tube?
    FULL     = MIDL|LEFT|RGHT,   // 000111 // fully planted
  }

  public floorCheck _fCheck;

}
