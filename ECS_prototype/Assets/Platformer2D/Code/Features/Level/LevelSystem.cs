using UnityEngine;
using Entitas;

public class LevelSystem : IInitializeSystem {
	
	#region IInitializeSystem implementation
	public void Initialize (){
		Debug.LogFormat("Initializing Level : {0} ", this);
	}
	#endregion

}
