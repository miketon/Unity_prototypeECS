﻿using UnityEngine        ;
using System             ;
using System.Collections ;

namespace MTON{

  public class _enum {

  [Flags] // Powers of two
  public enum GPAD {
    // Decimal              // Binary
    Neutral  = 0,           // 000000
    DPAD     = 1,           // 000001
    BTTN     = 2,           // 000010
    
    FULL     = DPAD|BTTN,   // 000011
  }

  [Flags] // Powers of two
  public enum Button {
    // Decimal              // Binary
    Neutral  = 0,           // 000000
    Release  = 1,           // 000001
    Down     = 2,           // 000010
    Hold     = 4,           // 000100
  }

  [Flags] // Powers of two
  public enum Dirn {
    // Decimal              // Binary
    Neutral  = 0,           // 000000
    UP       = 1,           // 000001
    DN       = 2,           // 000010
    LT       = 4,           // 000100
    RT       = 8,           // 001000

    DN_RT    = DN|RT,       // 001010
    DN_LT    = DN|LT,       // 000110
    UP_RT    = UP|RT,       // 001001
    UP_LT    = UP|LT,       // 000101

    HNONE    = RT|LT,       // 001100
    VNONE    = DN|UP,       // 000011
    ANONE    = RT|LT|DN|UP, // 001111
    LNONE    = LT|DN|UP,    // 000111
    RNONE    = RT|DN|UP,    // 001011
    UNONE    = RT|LT|UP,    // 001101
    DNONE    = RT|LT|DN,    // 001110
	}

  [Flags] // Powers of two
  public enum VState {         // Vertical State
    // Decimal                 // Binary
    Ground    =   0,           // 000000 //==stand
    OnRise    =   1,           // 000001
    OnApex    =   2,           // 000010
    OnFall    =   4,           // 000100
    OnDive    =  16,           // 001000

    Crouch    = Ground|OnDive  //
  }

  [Flags] // Powers of two
  public enum HState {       // Horizontal State
    // Decimal               // Binary
    Neutral   = 0,           // 000000
    OnWalk    = 1,           // 000001
    OnRunn    = 2,           // 000010
    OnDash    = 4,           // 000100
  }

  [Flags] // Powers of two
  public enum FState {       // Facing State
    // Decimal               // Binary
    Neutral   = 0,           // 000000
    Fwrd      = 1,           // 000001 // 2d = right
    Back      = 2,           // 000010 // 2d = left
    Rght      = 4,           // 000100
    Left      = 8,           // 001000
  }

	[Flags] // Powers of two
	public enum Rbody {
		// Decimal               // Binary
		Neutral   = 0,           // 000000
		OnGround  = 1,           // 000001
		OnCeilng  = 2,           // 000010
		OnStunnd  = 4,           // 000100
	}

	[Flags] // Powers of two
	public enum Type {
    // Decimal               // Binary
    Neutral  = 0,            // 000000
    Jump     = 1,            // 000001
    Attack   = 2,            // 000010
    Guard    = 4,            // 000100

    AirSpin  = Jump |Attack, // 000011
    Grab     = Guard|Attack, // 000110
	}

	[Flags] // Powers of two
	public enum Powr {          // << shifts for progressive power up...or power down
		// Decimal              // Binary
		Neutral  = 0,           // 000000
		Min      = 1 << 0,      // 000001
		Max      = 1 << 1,      // 000010
		Ovr      = 1 << 2,      // 000100
		End      = 1 << 3,      // 001000
	}

	public static string TokenizeAndReturnPath(string IN_STRING, string IN_LIMITER){
		string[] tokenizeString = IN_STRING.Split(char.Parse(IN_LIMITER));
		string returnPath = "";
		for (var i=0; i<tokenizeString.Length-1; i++) {
//			Debug.LogFormat(" PRINTING : {0} ", tokenizeString[i]);
			returnPath = (returnPath+tokenizeString[i]+"/");
		}
//		Debug.LogFormat("NEW PATH : {0}", returnPath);
		return returnPath;
	}

	}

}

