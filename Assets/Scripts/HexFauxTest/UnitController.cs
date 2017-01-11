using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMap;

public class UnitController : MonoBehaviour {

	private bool _isMoving;
	private Vector3[] waypoints;
	private FieldOrientationAssistant assist;
	private AStarHex pathfinding;
	private int pathcount;
	// Use this for initialization
	void Start () {
		assist = FindObjectOfType<FieldOrientationAssistant>();
		pathfinding = new AStarHex();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PreparePath(Vector3 dest){
		if (!_isMoving){
			waypoints = assist.ArrayToWorld(pathfinding.FindPath(assist.WorldToGrid(transform.position), assist.WorldToGrid(dest)));
		}

	}

	public void StartPath(){
		if (!_isMoving){
			_isMoving = true;
			if (LeanTween.isTweening(gameObject)){
				Debug.Log(gameObject.name+" is still tweening!");
			}
			pathcount = 0;
			LeanTween.move(gameObject, waypoints[pathcount], 0.2f).setOnComplete(() => FollowPath());
			Vector3 myRotation = new Vector3(0, Quaternion.LookRotation(waypoints[pathcount]-transform.position, assist.transform.up).eulerAngles.y, 0);

			LeanTween.rotateLocal(gameObject, myRotation, 0.2f).setEase(LeanTweenType.easeSpring);
		}
	}
	void FollowPath(){
		pathcount++;

		if (pathcount < waypoints.Length){
			LeanTween.move(gameObject, waypoints[pathcount], 0.3f).setOnComplete(() => FollowPath());
			Vector3 myRotation = new Vector3(0, Quaternion.LookRotation(waypoints[pathcount]-waypoints[pathcount-1], assist.transform.up).eulerAngles.y, 0);
			LeanTween.rotateLocal(gameObject, myRotation, 0.2f).setEase(LeanTweenType.easeSpring);
		}
		else{
			_isMoving = false;
		}
	}

	public Vector3[] GetWaypoints(){
		return waypoints;
	}
	public bool isMoving{
		get{ return _isMoving; }
	}

}
