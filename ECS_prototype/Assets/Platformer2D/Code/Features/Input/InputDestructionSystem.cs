using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class InputDestructionSystem : IReactiveSystem {
	#region IReactiveExecuteSystem implementation

	public void Execute (List<Entity> entities){
		foreach (var e in entities) {
			e.destroy();
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.Input.OnEntityAdded();
		}
	}

	#endregion
}
