using UnityEngine;
using Entitas;

public static class PoolExtensions {

	static readonly string[] _players = {
		Res.player0,
	};

	public static Entity spawnPlayer(this Pool pool, Vector3 pos){
		return pool.CreateEntity()
			.IsGameObject(true)
			.IsPlayer(true)
			.IsIOControl(true)
//			.AddDpadEvent(MTON._enum.Dirn.Neutral, MTON._enum.Press.Neutral)
//			.AddButtonEvent(MTON._enum.Press.Down, MTON._enum.Type.Attack) //player can't have this if IO_OnDestroySystem will delete it
			.AddResource(_players[0])
			.AddPosition(pos.x, pos.y, pos.z)
			.AddVelocity(0.0f, 0.0f, 0.0f)
			.AddForce(0.1f, 0.0f, 1.0f, 1.0f) //accel, speed, maxspeed, mass
      .Add_OnStart(true);
	}

	public static Entity CreateCollision(this Pool pool, Collision collision) { 
		return pool.CreateEntity()
			.AddOnCollisionEnter(collision);
	}

}
