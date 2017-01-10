using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOrientationAssistant : MonoBehaviour {

	//Transform fieldTransform;
	Vector3 storedPosition;
	Quaternion storedRotation;

	// Use this for initialization
	void Start () {
		storedPosition = transform.position;
		storedRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public Vector3 WorldToGrid(Vector3 vec){
		Vector3 pivotDir = vec-transform.position;
		Quaternion relative = Quaternion.Inverse(transform.rotation) * storedRotation;
		pivotDir =  relative * pivotDir;
		//pivotDir = pivotDir;
		return  pivotDir+storedPosition;
	}

	public Vector3 GridToWorld(Vector3 vec){
		Vector3 pivotDir = vec - storedPosition;
		Quaternion inverse = storedRotation*transform.rotation;
		pivotDir = inverse * pivotDir;

		return pivotDir+transform.position;
	}
}
