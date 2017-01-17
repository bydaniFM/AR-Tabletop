using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HexMap;

public class PathTester : MonoBehaviour {
	GameObject[] markers;
	//AStarHex pathfinding;
	public UnitController player;

	Vector3 temp;

	FakeEnemy[] enemies;
	int enemynum;
	public Transform field;
	//FieldOrientationAssistant assist;

	// Use this for initialization
	void Start () {
		//assist = FindObjectOfType<FieldOrientationAssistant>();
		player.name = "Player";
		if (HexGrid.instance == null){
			Debug.LogError("REEEE");
		}
		//pathfinding = new AStarHex();
		markers = new GameObject[10];

		enemies = FindObjectsOfType<FakeEnemy>();


	}



	// Update is called once per frame
	void Update () {
		if (player != null){
			if (Input.GetKeyDown(KeyCode.A) || Input.touchCount == 3){
				player.transform.LookAt(enemies[0].transform);
				player.GetComponent<FireWeapon>().Shoot();

				enemies[0].GetComponent<SelfDestruct>().Execute(2.5f);
			}
			if (Input.GetMouseButtonDown(0) && Input.touchCount < 2) {
			//	ParentStuff();
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit)){
					//Vector3 mpos = Input.mousePosition;	
					Debug.Log("hit "+hit.point);
					if(!player.isMoving){
						player.PreparePath(hit.point);
						//Pathfind(hit.point);
						//GameObject m1 = SlapMarker(hit.point);
						if (player.hasPath){
							ShowPath(player.GetWorldWaypoints());
						}

						enemynum = Random.Range(0, enemies.Length);
						enemies[enemynum].PlotNext();
					}
					//GameObject m2 = SlapMarker(assist.GridToWorld(hit.point));
					//if (m1.transform.position == m2.transform.position){
					//	Debug.Log("MATCH");
					//}
				}
			}
			if (Input.GetMouseButtonDown(1) || Input.touchCount == 2){
				if(!player.isMoving){
					player.StartPath();
					enemies[enemynum].GotoNext();
					foreach (var item in markers) {
						Destroy(item);
					}
				}
			}

		}
		if (Input.GetKeyDown(KeyCode.R) || Input.touchCount == 5){
			//Application.LoadLevel(Application.loadedLevel);
			//SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

//		Debug.DrawLine(player.transform.position, assist.transform.up);
//		Debug.DrawLine(player.transform.position, player.transform.position+temp);

//		if (LeanTween.isTweening(player)){
//			float ratio = tween.passed / tween.time;
//			player.transform.rotation = Quaternion.Lerp();
//		}
	}


	void ShowPath(Vector3[] points){
		
		//Debug.Log(Input.mousePosition);
		//HexCell target = HexGrid.instance.GetCell(assist.WorldToGrid(point));
		//HexCell start = HexGrid.instance.GetCell(player.transform.position);
		//Debug.Log("Clicked "+ target.q+":"+target.r);
		foreach (var item in markers) {
			Destroy(item);
		}
		markers = new GameObject[points.Length];
		for (int i = 0; i < points.Length; i++) {
			
			markers[i] = SlapMarker(points[i]);
			markers[i].name = "Hex mark";

		}


		//temp = waypoints[pathcount]-player.transform.position;
		//tween = 
	}

	GameObject SlapMarker(Vector3 pos){
		GameObject go = new GameObject("hex");
		MeshFilter mf = go.AddComponent<MeshFilter>();
		MeshRenderer mr = go.AddComponent<MeshRenderer>();
		mf.mesh = MeshGen.Hex(HexLayout.instance.wide_width);
		go.transform.localScale = Vector3.one*0.3f;
		go.transform.position = pos;
		go.transform.parent = field;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localPosition += Vector3.up*0.02f;
		return go;
	}


	//void ParentStuff(){
	//	GameObject.Find("TheGrid").transform.parent = GameObject.Find("Field").transform;
	//}



}
