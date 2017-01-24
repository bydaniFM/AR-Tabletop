using System;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour {

	public Transform hardpoint;
	public GameObject particle;
	public Action onDone;

	// Use this for initialization
	void Start () {
		
	}

 	void Update(){
 		

 	}

	public void Shoot(Transform[] targets, float t){
		float time = t/targets.Length; 
		for (int i = 0; i < targets.Length; i++) {
			Transform trg = targets[i];
			LeanTween.delayedCall(gameObject, time * i, ()=>{Shoot(trg, time);});
			Debug.Log(transform.name+" will shoot "+trg.name+ " in "+(time*i)+"s" );
		}
		LeanTween.delayedCall(gameObject, t, onDone);
	}
	public void Shoot(Transform target, float t){
		
		Vector3 myRotation = new Vector3(0, Quaternion.LookRotation(target.position-transform.position, transform.up).eulerAngles.y, 0);
		LeanTween.rotate(gameObject, myRotation, 0.2f);
		GameObject prt = (GameObject) Instantiate(particle, hardpoint.position, hardpoint.rotation);
		prt.transform.parent = hardpoint;
		Destroy(prt, t);

	}
}
