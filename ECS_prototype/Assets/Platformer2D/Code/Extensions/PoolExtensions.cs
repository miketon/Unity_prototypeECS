using UnityEngine;
using Entitas;
using MTON;

public static class PoolExtensions {

	static readonly string[] _players = {
		_ResourcePaths.player0,
	};

	public static Entity spawnPlayer(this Pool pool, int player_ID, Vector3 pos){
		return pool.CreateEntity()
      .AddPlayer(player_ID)
      .AddIO_Controllable(player_ID)
      .Add_CharacterController(player_ID)
      .AddstateVMotion(_enum.VState.Ground)
      .AddstateHMotion(_enum.HState.Neutral)
			.AddViewResource(_players[0]) //string to prefab for instantiation
			.AddPosition(pos.x, pos.y, pos.z)
			.AddVelocity(0.0f, 0.0f, 0.0f)
//			.AddForce(0.1f, 0.0f, 1.0f, 1.0f) //accel, speed, maxspeed, mass
      .Add_OnStart(true); // indicates already initiated
	}

  public static Entity CreateCollision(this Pool pool, Collision collision) { 
    return pool.CreateEntity()
    .AddeventOnCollision(0, collision); // HACK : Adding dummy 0 to follow convention that events must have an ID
  }

}
