﻿using UnityEngine;
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
			.AddResource(_players[0])
			.AddPosition(pos.x, pos.y, pos.z)
			.AddVelocity(0.0f, 0.0f, 0.0f)
			.AddForce(0.1f, 0.0f, 1.0f, 1.0f); //accel, speed, maxspeed, mass
	}

	public static Entity CreateCollision(this Pool pool, Collision collision) { 
		return pool.CreateEntity()
			.AddIORelease(true, true, true);
	}

}
