using System;
using System.Collections.Generic;
using UnityEngine;
using HexMap;

public class UnitController : MonoBehaviour{

	public int move;
	public int init;
	public int range;
	public int damage;
	//public int spawnId;
	//    public string owner;
	
	private bool _isMoving;
	//private bool _hasPath;
	private Vector3[] waypoints;
	private FieldOrientationAssistant assist;
	private AStarHex pathfinding;
	private FloodFillHex fill;
	private int pathcount;

	private Action OnMoveEnd;

	// Use this for initialization
	void Start () {
		assist = FindObjectOfType<FieldOrientationAssistant>();
		pathfinding = new AStarHex();
		fill = new FloodFillHex();
		waypoints = new Vector3[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3[] GetMoveRange(){
		return fill.FloodFill(transform.position, move);

	}

	public void PreparePath(Vector3 dest){
		if (!_isMoving){
			//Debug.Log(HexGrid.instance);
			//Debug.Log(assist);
			if (HexGrid.instance.GetCellId(assist.WorldToGrid(transform.position)) == -1){
				Debug.Log("Unit "+gameObject.name+ " position out of bounds!");
				//_hasPath = false;
				waypoints = new Vector3[0];
				return;
			}
			if ( HexGrid.instance.GetCellId(assist.WorldToGrid(dest)) == -1){
				Debug.Log("Unit "+gameObject.name+ " destination out of bounds!");
				//_hasPath = false;
				waypoints = new Vector3[0];
				return;
			}
			//waypoints = assist.ArrayToWorld(pathfinding.FindPath(assist.WorldToGrid(transform.position), assist.WorldToGrid(dest)));
			//Waypoints are stored in GRID coords
			waypoints = pathfinding.FindPath(assist.WorldToGrid(transform.position), assist.WorldToGrid(dest));
			if (waypoints.Length <= move){
				//_hasPath = true;
				Debug.Log(gameObject.name+" path set!");
			}
			else {
				waypoints = new Vector3[0];
			}
		}

	}

	public void StartPath(){
		Debug.Log(gameObject.name+" Started Moving!");
		if (!_isMoving && hasPath){
			_isMoving = true;
			if (LeanTween.isTweening(gameObject)){
				Debug.Log(gameObject.name+" is still tweening!");
			}
			pathcount = 0;
			LeanTween.moveLocal(gameObject, waypoints[pathcount], 0.2f).setOnComplete(() => FollowPath());
			Vector3 myRotation = new Vector3(0, Quaternion.LookRotation(waypoints[pathcount]-transform.position, assist.transform.up).eulerAngles.y, 0);

			LeanTween.rotateLocal(gameObject, myRotation, 0.2f).setEase(LeanTweenType.easeSpring);
		}
	}
	void FollowPath(){
		pathcount++;

		if (pathcount < waypoints.Length){
			LeanTween.moveLocal(gameObject, waypoints[pathcount], 0.3f).setOnComplete(() => FollowPath());
			Vector3 myRotation = new Vector3(0, Quaternion.LookRotation(waypoints[pathcount]-waypoints[pathcount-1], assist.transform.up).eulerAngles.y, 0);
			LeanTween.rotateLocal(gameObject, myRotation, 0.2f).setEase(LeanTweenType.easeSpring);
		}
		else{
			_isMoving = false;
			//_hasPath = false;
			OnMoveEnd();
		}
	}

	public Vector3[] GetWaypoints(){
		return waypoints;
	}
	public Vector3[] GetWorldWaypoints(){
		return assist.ArrayToWorld(waypoints);
	}
	public Vector3 GetDestination(){
		return waypoints[waypoints.Length-1];
	}
	public bool isMoving{
		get{ return _isMoving; }
	}
	public bool hasPath{
		get{ return waypoints.Length > 0; }
	}
	public void SetOnMoveEnd(Action action){
		OnMoveEnd = action;
	}



}
