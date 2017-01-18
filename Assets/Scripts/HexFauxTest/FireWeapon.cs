using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour {

	public Transform hardpoint;
	public GameObject particle;
    public Animator anim;

	// Use this for initialization
	void Start () {
        anim.GetComponent<Animator>();
    }
	
	public void Shoot() {
        anim.Play("Fire");
        GameObject prt = (GameObject) Instantiate(particle, hardpoint.position, hardpoint.rotation);
		Destroy(prt, 3);
	}
}
