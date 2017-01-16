using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whadever : Photon.PunBehaviour, IPunObservable {

	public bool clik;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}


	void IPunObservable.OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info){
		 Debug.Log("White ppl stay colonizin");
		if (stream.isWriting){
			Debug.Log("Ima observed");
		    // We own this player: send the others our data
		    stream.SendNext(clik);
			
		}
		else{
		    // Network player, receive data
		    this.clik = (bool)stream.ReceiveNext();
	    }
	}
}
