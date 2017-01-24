using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	public GameObject gibs;

	
	public void Execute(float t){
		Destroy(gameObject, t);
		Invoke("Gibs", t-0.1f);
	}

	void Gibs(){
		GameObject go = (GameObject)Instantiate(gibs, transform.position+Vector3.up*0.1f, transform.rotation);
		Rigidbody[] rb = FindObjectsOfType<Rigidbody>();
		foreach (var item in rb) {
			item.AddExplosionForce(100, transform.position+Vector3.down*0.1f + Vector3.right*(Random.Range(-0.9f, 0.9f)), 10);	
		}

		Destroy(go, 5);
	}
}
