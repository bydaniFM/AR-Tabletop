using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMap;

public class PathTester : MonoBehaviour {

	//GameObject[] markers;
	AStarHex pathfinding;
	public GameObject player;

    public bool active = false;
    public bool movementAllowed = false;

    public RaycastHit storedHit;

    FieldOrientationAssistant assist;
    // Use this for initialization
    void Start () {
        assist = FindObjectOfType<FieldOrientationAssistant>();
        if (HexGrid.instance == null){
			Debug.LogError("REEEE");
		}
		pathfinding = new AStarHex();
		//markers = new GameObject[10];
	}

	Vector3[] waypoints;
	int pathcount;
	LTDescr tween;

    // Update is called once per frame
    void Update() {

        if (active) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)) {	
                    Debug.Log("hit " + hit.point);
                }
                if(!movementAllowed)
                    storedHit = hit;

                active = false;
            }

            if (movementAllowed) {
                movementAllowed = false;
                //HexCell target = HexGrid.instance.GetCell(storedHit.point);
                HexCell target = HexGrid.instance.GetCell(assist.WorldToGrid(storedHit.point));
                Debug.Log("Clicked " + target.q + ":" + target.r);
                //waypoints = pathfinding.FindPath(player.transform.position, storedHit.point);
                waypoints = TransformWaypoints(pathfinding.FindPath(assist.WorldToGrid(player.transform.position), assist.WorldToGrid(storedHit.point)));
                //foreach (var item in markers) {
                //    Destroy(item);
                //}
                //markers = new GameObject[waypoints.Length];
                //for (int i = 0; i < waypoints.Length; i++) {
                //    markers[i] = SlapMarker(waypoints[i]);
                //    markers[i].name = "Hex mark";
                //}

                pathcount = 0;
                tween = LeanTween.move(player, waypoints[pathcount], 0.2f).setOnComplete(() => TweenNext());
                // LeanTween.rotate(player, Vector3.zero, 1f);
                Vector3 myRotation = Quaternion.LookRotation(waypoints[pathcount] - player.transform.position).eulerAngles;
                LeanTween.rotateLocal(player, myRotation, 0.2f).setEase(LeanTweenType.easeSpring);
                
            }
        }
    }

    GameObject SlapMarker(Vector3 pos) {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.localScale = Vector3.one * 0.3f;
        go.transform.position = pos;
        return go;
    }

    void TweenNext(){
		pathcount++;
		if (pathcount < waypoints.Length){
			LeanTween.move(player, waypoints[pathcount], 0.3f).setOnComplete(() => TweenNext());

				//player.transform.LookAt(waypoints[pathcount+1]);
			Vector3 myRotation =  Quaternion.LookRotation(waypoints[pathcount]-waypoints[pathcount-1]).eulerAngles;
			LeanTween.rotateLocal(player,myRotation, 0.2f).setEase(LeanTweenType.easeSpring);
			//}
			//LeanTween.rotate(player, waypoints[pathcount+1], 0.1f);
		}
	}

    Vector3[] TransformWaypoints(Vector3[] waypoints) {
        Vector3[] result = new Vector3[waypoints.Length];
        for (int i = 0; i < result.Length; i++) {
            result[i] = assist.GridToWorld(waypoints[i]);
        }
        return result;
    }

    public void AllowMovement() {
        //if(active)
        active = true;
        movementAllowed = true;
    }
}
