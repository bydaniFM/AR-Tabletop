using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetDebugGui : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}
	void OnGUI(){
		if (PhotonNetwork.connected){
			GUI.Label(new Rect(Screen.width - 200, Screen.height - 50, 200, 20), "Connected!");
			if(PhotonNetwork.room != null){
				GUI.Label(new Rect(Screen.width - 200, Screen.height -30, 200, 20), "Players: "+PhotonNetwork.room.PlayerCount);
			}
		}
	}
}
