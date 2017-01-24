using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOrientationAssistant : MonoBehaviour {

	//Transform fieldTransform;
	Vector3 storedPosition;
	Quaternion storedRotation;

	public static FieldOrientationAssistant instance;
	void Awake(){
		if (instance == null){
			instance = this;
		}
		else if (instance != this){
			Debug.LogError("An instance of Field Orientation Assistant already exists!");

		}
	}
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

	public Vector3[] ArrayToWorld (Vector3[] points){
		Vector3[] result = new Vector3[points.Length];
		for (int i = 0; i < result.Length; i++) {
			result[i] = GridToWorld(points[i]);
		}
		return result;
	}
}
