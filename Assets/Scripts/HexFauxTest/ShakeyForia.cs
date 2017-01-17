using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeyForia : MonoBehaviour {
	public float move_speed = 2f;
	public float rot_speed = 2f;

	public float move_dist = 3f;
	public float rot_angle = 45f;
	// Use this for initialization
	void Start () {
		LeanTween.move(gameObject, transform.position+new Vector3(1, -1, 1), move_speed).setOnComplete(() => RandomMove());
		LeanTween.rotate(gameObject, new Vector3(15, -15, 15), rot_speed).setOnComplete(() => RnadomRotation());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RandomMove(){
		float xdeviation = Random.Range(-move_dist, move_dist);
		float ydeviation = Random.Range(-move_dist, move_dist);
		float zdeviation = Random.Range(-move_dist, move_dist);
		LeanTween.move(gameObject, transform.position+new Vector3(xdeviation, ydeviation, zdeviation), move_speed).setOnComplete(() => RandomMove());
	}

	void RnadomRotation(){
		float xangle = Random.Range(-rot_angle, rot_angle);
		float yangle = Random.Range(-rot_angle, rot_angle);
		float zangle = Random.Range(-rot_angle, rot_angle);
		LeanTween.rotate(gameObject, new Vector3(xangle, yangle, zangle), rot_speed).setOnComplete(() => RnadomRotation());
	}
}
