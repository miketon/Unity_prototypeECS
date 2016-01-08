using Entitas;
using System;

public class ButtonEventComponent : IComponent {

	[Flags] // Powers of two
	public enum State {
		// Decimal              // Binary
		Neutral  = 0,           // 000000
		Down     = 1,           // 000001
		Release  = 2,           // 000010
		Hold     = 4,           // 000100
	}

	[Flags] // Powers of two
	public enum Type {
		// Decimal              // Binary
		Cancel   = 0,           // 000000
		Jump     = 1,           // 000001
		Attack   = 2,           // 000010
		Guard    = 4,           // 000100

		AirSpin  = Jump|Attack, // 000011
        Grab     = Guard|Attack,// 000110
	}

}
