using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class InputPressSystem : IReactiveSystem, ISetPool {
	
	private Pool _pool;

	#region IReactiveExecuteSystem implementation

	public void Execute (List<Entity> entities){
		Debug.LogFormat("Key Pressed : {0} ", KeyCode.Space)	;
		var inputEntity = entities.SingleEntity() ;
		_pool.DestroyEntity(inputEntity)          ; // destroy  input object
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.Input.OnEntityAdded();
		}
	}

	#endregion

	
	#region ISetPool implementation

	public void SetPool (Pool pool){
		_pool = pool;
	}

	#endregion
}
