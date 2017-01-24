using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : Photon.PunBehaviour, IPunObservable {
	//public bool enemyReady;
	//public bool allUnitsPlaced;

	//PlayerControl cc;
	EnemyControl ec;

	public static NetworkController instance;

	void Awake(){
		if (instance == null){
			instance = this;
		}
		else if (instance != this){
			Debug.LogError("An instance of NetworkController already exists!");

		}
		//cc = FindObjectOfType<PlayerControl>();
		ec = FindObjectOfType<EnemyControl>();
	}

	void IPunObservable.OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting){
			//Debug.Log("Ima observed");
		   // stream.SendNext(enemyReady);
		}
		else{
		  // enemyReady = (bool)stream.ReceiveNext();
			
	    }

	}

	[PunRPC]
	public void Ready(){
		ec.ready = true;
	}

//	[PunRPC]
//	public void AllUnitsPlaced(){
//		cc.AllUnitsPlaced();
//	}

	[PunRPC]
	void SetEnemyMove(int unitID, int unitDest){
		//ec.SetEnemyMove(unitID, unitDest);
		ec.SelectUnit(unitID);
		ec.BuildPath(HexMap.HexGrid.instance.positions[unitDest]);
		Debug.Log("Setting enemy "+ unitID + " move to "+unitDest);
	}
	/*void SetEnemyMove(byte[] bytes){
		using (MemoryStream memoryStream = new MemoryStream(bytes)){
			using (BinaryReader binaryReader = new BinaryReader(memoryStream)){
				int unitId = binaryReader.ReadInt32();
				int unitDest = binaryReader.ReadInt32();
			}
		}

	}*/

	[PunRPC]
		void ForceEnemyInfoUpdate(byte[] bytes){
			Debug.Log("Loaded changed enemy stats!!");
			ec.enemy.Load(bytes);
			ec.UpdateControllers();
		}
}
