using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARPNet{
	public class GameManager : Photon.PunBehaviour {

		// Use this for initialization
		void Start () {
			if (!PhotonNetwork.connected){
				SceneManager.LoadScene(0);
			}
			else{
				//if (photonView.isMine){
				Debug.Log("I'mo not instantiatin shit");


				//}
			}
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public override void OnLeftRoom(){
	        SceneManager.LoadScene(0);
	    }

	    public void LeaveRoom(){
	    	PhotonNetwork.LeaveRoom();
	    }

		public override void OnPhotonPlayerConnected( PhotonPlayer other  ){
			Debug.Log(other.NickName + " joined!");
		//	PhotonNetwork.Instantiate("Test", Vector3.zero, Quaternion.identity, 1);
			PhotonNetwork.Instantiate("Test", Vector3.zero, Quaternion.identity, 1);
			PhotonNetwork.Instantiate("LeCardsObj", Vector3.zero, Quaternion.identity, 1);
		}

		public override void OnPhotonPlayerDisconnected( PhotonPlayer other ){
			Debug.Log(other.NickName + " left!");
		}
	}
}