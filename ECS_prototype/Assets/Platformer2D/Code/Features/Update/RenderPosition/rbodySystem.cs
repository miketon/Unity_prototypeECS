using UnityEngine;
using System.Collections;
using Entitas;

public class rbodySystem: IExecuteSystem, ISetPool{

	private Group _group;
	
	#region IExecuteSystem implementation
	public void Execute (){
		foreach (var body in _group.GetEntities()) {
			Debug.LogFormat("Updating Body : {0}", body);
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_group = pool.GetGroup(Matcher.AllOf(Matcher.GameObject, Matcher.Position, Matcher.Velocity, Matcher.Force));
	}
	#endregion

}
