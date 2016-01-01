using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class UpdatePosHSystem : IReactiveSystem, ISetPool {

	private Pool _pool;
	private Group _playerElements;

	#region IReactiveExecuteSystem implementation
	public void Execute (List<Entity> entities){
		var inputEntity = entities.SingleEntity() ;
	    Debug.LogFormat(" UpdatePosHSystem : {0} {1}", inputEntity, this);
//		foreach (var player in _playerElements.GetEntities()) {
//			player.
//		}
	}

	public TriggerOnEvent trigger {
		get {
			return Matcher.InputButton.OnEntityAdded();
		}
	}
	#endregion


	#region ISetPool implementation
	public void SetPool (Pool pool){
		_pool = pool;
		_playerElements = _pool.GetGroup(Matcher.AllOf(Matcher.Player, Matcher.Position));
	}
	#endregion



}
