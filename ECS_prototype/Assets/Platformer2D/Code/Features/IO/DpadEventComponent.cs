using Entitas;
using System;

public class DpadEventComponent : IComponent {
	
	[Flags] // Powers of two
	public enum State {
		// Decimal              // Binary
		Neutral  = 0,           // 000000
		Down     = 1,           // 000001
		Release  = 2,           // 000010
		Hold     = 4,           // 000100
	}

	[Flags] // Powers of two
	public enum Dir {
		// Decimal              // Binary
		Neutral  = 0,           // 000000
		Up       = 1,           // 000001
		Down     = 2,           // 000010
		Left     = 4,           // 000100
		Right    = 8,           // 001000

		dnDiagFw = Down|Right,  // 001010
        dnDiagBk = Down|Left ,  // 000110
		upDiagFw = Up|Right  ,  // 001001
		upDiagBk = Up|Left   ,  // 000101
	}


}
