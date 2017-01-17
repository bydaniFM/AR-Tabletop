using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMap;

public class UnitController : MonoBehaviour {

	private bool _isMoving;
	private bool _hasPath;
	private Vector3[] waypoints;
	private FieldOrientationAssistant assist;
	private AStarHex pathfinding;
	private int pathcount;
    public float speed;

    public Animator anim;

    // Use this for initialization
    void Start () {
		assist = FindObjectOfType<FieldOrientationAssistant>();
		pathfinding = new AStarHex();
        anim = GetComponent<Animator>();

        speed = 0;
	}
	
	// Update is called once per frame
	void Update () {

        
    }

	public void PreparePath(Vector3 dest){
		if (!_isMoving){
			if (HexGrid.instance.GetCellId(assist.WorldToGrid(transform.position)) == -1){
				Debug.LogError("Unit "+gameObject.name+ " position out of bounds!");
				_hasPath = false;
				return;
			}
			if ( HexGrid.instance.GetCellId(assist.WorldToGrid(dest)) == -1){
				Debug.LogError("Unit "+gameObject.name+ " destination out of bounds!");
				_hasPath = false;
				return;
			}
			//waypoints = assist.ArrayToWorld(pathfinding.FindPath(assist.WorldToGrid(transform.position), assist.WorldToGrid(dest)));
			//Waypoints are stored in GRID coords
			waypoints = pathfinding.FindPath(assist.WorldToGrid(transform.position), assist.WorldToGrid(dest));

			_hasPath = true;
		}

	}

	public void StartPath(){
		if (!_isMoving && _hasPath){
			_isMoving = true;
			if (LeanTween.isTweening(gameObject)){
				Debug.Log(gameObject.name+" is still tweening!");
			}
			pathcount = 0;
			LeanTween.moveLocal(gameObject, waypoints[pathcount], 0.2f).setOnComplete(() => FollowPath());
			Vector3 myRotation = new Vector3(0, Quaternion.LookRotation(waypoints[pathcount]-transform.position, assist.transform.up).eulerAngles.y, 0);

			LeanTween.rotateLocal(gameObject, myRotation, 0.2f).setEase(LeanTweenType.easeSpring);
	
			//Handle animation
			if (anim!=null){
	            anim.Play("Move");
	            anim.SetFloat("AnimSpeed", speed);
	            if (speed < 1){
	                speed += 1f*Time.deltaTime;
				}
			}
		}
		else {
            speed = 0;
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
			_hasPath = false;
		}
	}

	public Vector3[] GetWaypoints(){
		return waypoints;
	}
	public Vector3[] GetWorldWaypoints(){
		return assist.ArrayToWorld(waypoints);
	}
	public bool isMoving{
		get{ return _isMoving; }
	}
		public bool hasPath{
		get{ return _hasPath; }
	}
}
