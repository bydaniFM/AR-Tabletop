using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPNet{
	public class PlayerNameInput : MonoBehaviour {

		// Store the PlayerPref Key to avoid typos
	    static string playerNamePrefKey = "PlayerName";

		// Use this for initialization
		void Start () {
			string playerName = "";

	        InputField _inputField = this.GetComponent<InputField>();
	        if (_inputField!=null)
	        {
	            if (PlayerPrefs.HasKey(playerNamePrefKey))
	            {
	                playerName = PlayerPrefs.GetString(playerNamePrefKey);
	                _inputField.text = playerName;
	            }
	        }


	        PhotonNetwork.playerName = playerName;
	    
		}
		
		public void SetPlayerName(string value){
	        // #Important
	        PhotonNetwork.playerName = value + " "; // force a trailing space string in case value is an empty string, else playerName would not be updated.


	        PlayerPrefs.SetString(playerNamePrefKey,value);
	    }
	}
}