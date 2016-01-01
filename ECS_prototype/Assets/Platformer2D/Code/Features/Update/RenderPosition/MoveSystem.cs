using Entitas;

public class MoveSystem : IExecuteSystem, ISetPool {

	private Group _group;

	#region IExecuteSystem implementation
	public void Execute (){
		foreach (var e in _group.GetEntities()) {
			var move = e.move;
			var pos  = e.position;
			e.ReplacePosition(pos.x, pos.y, pos.z * move.speed);
		}
	}
	#endregion

	#region ISetPool implementation
	public void SetPool (Pool pool){
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Position, Matcher.Move));
	}
	#endregion
}
