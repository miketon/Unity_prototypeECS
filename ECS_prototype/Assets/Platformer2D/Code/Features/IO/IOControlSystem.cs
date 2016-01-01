﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class IOControlSystem : IReactiveSystem, ISetPool {
	
	private Pool _pool;
	private bool _pressed = false;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		
		var inputEntity = entities.SingleEntity() ;
		if(inputEntity.iOGamePad.bNeutral){
			_pressed = true;
			_pool.isGravity=true;
			_pool.isIONeutral = true;
			IONeutralComponent.bBUTTON = true;
//			Debug.LogFormat(this + " : Input Pressed : {0} ", entities.SingleEntity())	;
		}
		else{
			_pool.isGravity=false;
			_pool.isIONeutral = false;
			_pressed = false;
			IONeutralComponent.bBUTTON = false;
			Debug.LogFormat(this + " : Input Released : {0} ", entities.SingleEntity())	;
		}
		_pool.DestroyEntity(inputEntity)          ; // destroy  input object
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.IOGamePad.OnEntityAdded();
		}
	}
	#endregion

	
	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool = pool;
	}
	#endregion
}
