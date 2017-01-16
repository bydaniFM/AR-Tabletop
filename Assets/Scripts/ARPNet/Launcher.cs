using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARPNet{
	public class Launcher : Photon.PunBehaviour {


		
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
		public byte MaxPlayersPerRoom = 2;
		string gameVersion = "1";

		public GameObject playPanel;
		public GameObject loadingLabel;

		bool isConnecting;

		void Awake(){
			PhotonNetwork.autoJoinLobby = false;
			PhotonNetwork.automaticallySyncScene = true;

			PhotonNetwork.logLevel = Loglevel;
		}
		// Use this for initialization
		void Start () {
			//Connect();	
			playPanel.SetActive(true);
			loadingLabel.SetActive(false);
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void Connect(){
			isConnecting = true;
			if (PhotonNetwork.connected){
				PhotonNetwork.JoinRandomRoom();
			}
			else{
				PhotonNetwork.ConnectUsingSettings(gameVersion);
			}
			playPanel.SetActive(false);
			loadingLabel.SetActive(true);

		}

		public override void OnConnectedToMaster(){
			Debug.Log("I CONNECT");
			if (isConnecting){
				PhotonNetwork.JoinRandomRoom();
			}
		}
		public override void OnDisconnectedFromPhoton(){
			Debug.LogWarning("I DISCONNECT!");

			playPanel.SetActive(true);
			loadingLabel.SetActive(false);
		}

		public override void OnPhotonRandomJoinFailed (object[] codeAndMsg)
		{
			PhotonNetwork.CreateRoom(null, new RoomOptions() {MaxPlayers = MaxPlayersPerRoom}, null);
			Debug.Log("Creating room instead!");
		}

		public override void OnJoinedRoom(){
			Debug.Log("Reeeee joined!");
			SceneManager.LoadScene("CardFight");
		}


	}
}