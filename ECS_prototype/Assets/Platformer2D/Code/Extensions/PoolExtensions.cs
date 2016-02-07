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
      .AddViewResource(_players[0])    //string to prefab for instantiation
      .AddIO_Controllable(player_ID)   // systemReact will add state for dpad and button, as well as inputcontroller gets
      .Add_CharacterController(player_ID)
      .AddstateVMotion(_enum.VState.Ground)
      .AddstateHMotion(_enum.HState.Neutral)
      .AddstateFacing(_enum.FState.Fwrd)
      .AddstateCrouch(false)
			.AddPosition(pos.x, pos.y, pos.z)
      .AddRotation(Quaternion.identity)
      .AddScale(Vector3.zero)
			.AddVelocity(0.0f, 0.0f, 0.0f);
	}

  public static Entity CreateCollision(this Pool pool, Collision collision) { 
    return pool.CreateEntity()
    .AddeventOnCollision(0, collision); // HACK : Adding dummy 0 to follow convention that events must have an ID
  }

}
