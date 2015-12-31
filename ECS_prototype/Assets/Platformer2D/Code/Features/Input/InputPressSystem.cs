using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class InputPressSystem : IReactiveSystem, ISetPool {
	
	private Pool _pool;
	private bool _pressed = false;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		
		var inputEntity = entities.SingleEntity() ;
		if(inputEntity.inputPress.bRelease){
			_pressed = false;
			Debug.LogFormat(this + " : Input Released : {0} ", entities.SingleEntity())	;
		}
		else{
			_pressed = true;
//			Debug.LogFormat(this + " : Input Pressed : {0} ", entities.SingleEntity())	;
		}
		if(inputEntity!=null){
		  _pool.DestroyEntity(inputEntity)          ; // destroy  input object
		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.InputPress.OnEntityAdded();
		}
	}
	#endregion

	
	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool = pool;
	}
	#endregion
}
