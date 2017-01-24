using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRegister : Photon.PunBehaviour{

	// Use this for initialization
	void Start () {
		if (!photonView.isMine){
			SelfRegister();
		}
	}
	
	void SelfRegister(){
		UnitController uc = GetComponent<UnitController>();
		EnemyControl ec = FindObjectOfType<EnemyControl>();
		ec.AddUnit(uc);
		Destroy(photonView);
		Destroy(this);

	}
}
