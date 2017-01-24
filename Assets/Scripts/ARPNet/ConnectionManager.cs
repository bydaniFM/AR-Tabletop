using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARPNet{
	public class ConnectionManager : Photon.PunBehaviour {
		public float timer;
		bool disconnect;
		// Use this for initialization
		void Start () {
			if (!PhotonNetwork.connected){
				SceneManager.LoadScene(0);
			}
			else{
				ManageState();
			}

		}
		
		// Update is called once per frame
		void Update () {
			if (disconnect){
				if (timer > 0){
					timer -= Time.deltaTime;
				}
				else{
					PhotonNetwork.LeaveRoom();
				}
			}
		}

		public override void OnLeftRoom(){
			NetDebugGui ui = FindObjectOfType<NetDebugGui>();
			Destroy(ui.gameObject);
	        SceneManager.LoadScene(0);

	    }

	    public void LeaveRoom(){
	    	PhotonNetwork.LeaveRoom();
	    }

		public override void OnPhotonPlayerConnected( PhotonPlayer other  ){
			Debug.Log(other.NickName + " joined!");
		//	PhotonNetwork.Instantiate("Test", Vector3.zero, Quaternion.identity, 1);
			//PhotonNetwork.Instantiate("Test", Vector3.zero, Quaternion.identity, 1);
			//PhotonNetwork.Instantiate("LeCardsObj", Vector3.zero, Quaternion.identity, 1);
			RoundController.SwitchState( GameState.Deployment );
		}

		public override void OnPhotonPlayerDisconnected( PhotonPlayer other ){
			Debug.Log(other.NickName + " left!");
			RoundController.SwitchState( GameState.Disconnecting);
			disconnect = true;
			timer = 5;
		}

		//public override void OnJoinedRoom(){
		void ManageState(){
			if(PhotonNetwork.room.PlayerCount == 1){
				RoundController.SwitchState( GameState.WaitingForOther );
				Debug.LogWarning("Set to Waiting");
			}
			else {
				RoundController.SwitchState( GameState.Deployment );
			}
		}
	}
}