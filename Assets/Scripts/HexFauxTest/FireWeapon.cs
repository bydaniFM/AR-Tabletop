using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour {

	public Transform hardpoint;
	public GameObject particle;
	// Use this for initialization
	void Start () {
		
	}
	
	public void Shoot(){
		GameObject prt = (GameObject) Instantiate(particle, hardpoint.position, hardpoint.rotation);
		Destroy(prt, 3);
	}
}
