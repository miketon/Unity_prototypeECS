using UnityEngine;
using System;
using System.Collections;

namespace MTON{
	
	public class _enum : MonoBehaviour {
		
	[Flags] // Powers of two
	public enum Press {
		// Decimal              // Binary
		Neutral  = 0,           // 000000
		Down     = 1,           // 000001
		Release  = 2,           // 000010
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
		
//		HNONE    = RT|LT,       // 001100
//		VNONE    = DN|UP,       // 000011
	}

	[Flags] // Powers of two
	public enum Type {
		// Decimal              // Binary
		Neutral  = 0,           // 000000
		Jump     = 1,           // 000001
		Attack   = 2,           // 000010
		Guard    = 4,           // 000100

		AirSpin  = Jump|Attack, // 000011
        Grab     = Guard|Attack,// 000110
	}

	[Flags] // Powers of two
	public enum Attk {          // << shifts for progressive power up...or power down
		// Decimal              // Binary
		Neutral  = 0,           // 000000
		Min      = 1 << 0,      // 000001
		Med      = 1 << 1,      // 000010
		Max      = 1 << 2,      // 000100
		Ovr      = 1 << 3,      // 001000
		Fin      = 1 << 4,      // 010000
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

