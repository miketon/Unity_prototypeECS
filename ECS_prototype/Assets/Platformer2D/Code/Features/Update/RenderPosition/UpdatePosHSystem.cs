using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class UpdatePosHSystem : IReactiveSystem, ISetPool {

	private Pool _pool;
	private Group _playerElements;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var hPos = entities.SingleEntity().iOGamePad.hAxis;
		Debug.LogFormat(" UpdatePosHSystem : {0} ", hPos);
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
		_playerElements = _pool.GetGroup(Matcher.AllOf(Matcher.IOControl, Matcher.Move));
	}
	#endregion

}
