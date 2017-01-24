using System.IO;
using System.Collections.Generic;
using UnityEngine;
using HexMap;

public class PlayerControl : MonoBehaviour, IPlayerControl {


	int _curUnit = -1;
	//int activeEnemy = -1;
	private List<UnitController> units;
	//private List<UnitController> enemyUnits;

	//public bool ready;

	public Player player;
	//EnemyControl enemy;
	PhotonView photonView;
	GameObject selMarker;

	//private bool mMarker;
	// Use this for initialization
	void Start () {
		units = new List<UnitController>();
		//enemyUnits = new List<UnitController>();
		//units.AddRange(FindObjectsOfType<UnitController>());
		photonView = NetworkController.instance.photonView;
		//enemy = FindObjectOfType<EnemyControl>();
		player = new Player();
	}
	
	// Update is called once per frame
	void Update () {

		/*if (_curUnit != -1 && !units[_curUnit].isMoving && !units[_curUnit].hasPath && !mMarker){
			HexMark.instance.MarkGrid("Range", units[_curUnit].GetMoveRange(), new Color(0, 0, 1, 0.2f));
			mMarker = true;

		}*/

//		if (_curUnit != -1 && ready){
//			if (units[_curUnit].isMoving){
//				ready = false;
//				_curUnit = -1;
//			}
//			else if (ready && enemy.ready){
//				MovementPhase();
//			}
//		}

	}


	/// Updates all UnitController variables from Unit stats
	public void UpdateControllers(){
		for (int i = 0; i < units.Count; i++) {
			units[i].move = player.units[i].stats["Mov"].Value;
			units[i].range = player.units[i].stats["Ran"].Value;
			units[i].init = player.units[i].stats["Ini"].Value;
			units[i].damage = player.units[i].damage;
		}
	}

	/// Commands current unit to calculate path to a point.
	public void BuildPath(Vector3 point){
		if(curUnit != -1){
			units[_curUnit].PreparePath(point);
			if (units[_curUnit].hasPath){
				//if(units[_curUnit].hasPath && units[_curUnit].GetWaypoints().Length <= units[_curUnit].move){
					HexMark.instance.MarkGrid("Path", units[_curUnit].GetWaypoints(), new Color(0, 1, 0, 0.5f));
				//}

				int unitDest = HexGrid.instance.GetCellId(units[curUnit].GetDestination());
				photonView.RPC("SetEnemyMove", PhotonTargets.Others, curUnit, unitDest);
			}
		}
	}

	/// Moves current unit along stored path
	public void MovementPhase(){
		if(_curUnit != -1){
			units[_curUnit].StartPath();
			units[_curUnit].SetOnMoveEnd(() => OnMoveEnd() );
			ClearSelection();

		}
		else{
			Debug.Log("Cant Move Unit");
		}
	}



	public void OnMoveEnd(){
		_curUnit = -1;
		Debug.Log("Ended Move on Player");
	}
	public UnitController[] GetUnits(){
		return units.ToArray();
	}
	public bool isOwner(UnitController uc){
		return units.Contains(uc);
	}
	void ClearSelection(){
		HexMark.instance.Unmark("Path");
		HexMark.instance.Unmark("Range");
	}

	public void Damage(UnitController uc, int damage){
		int ind = units.IndexOf(uc);
		player.units[ind].stats["HP"] -= damage;
		if (player.units[ind].stats["HP"] <= 0){
			SelfDestruct sd = uc.GetComponent<SelfDestruct>();
			sd.Execute(3f);
			InitiativeManager.Exclude(uc);
		}

	}
	public int curUnit{
		get{ return _curUnit; }
	}
	public bool canEndRound{
		get { return curUnit != -1 && units[curUnit].hasPath; }
	}
	public bool unitsDeployed{
		get { return units.Count == 3; }
	}

	public void SelectUnit(UnitController uc){
		if (units.Contains(uc)){
			SelectUnit(units.IndexOf(uc));
		}
	}
	public void SelectUnit(int id){
		ClearSelection();
		_curUnit = id;
		HexMark.instance.MarkGrid("Range", units[_curUnit].GetMoveRange(), new Color(0, 0, 1, 0.5f));
		if (units[_curUnit].hasPath){
			HexMark.instance.MarkGrid("Path", units[_curUnit].GetWaypoints(), new Color(0, 1, 0, 0.5f));
		}
	}

	public void PlaceUnit(){
			int cellID = Random.Range(0, HexGrid.instance.all_cells.Length-1);
			PlaceUnit(cellID);

	}
	public void PlaceUnit(int cellID){
		//if (units.Count<3){
		if (!unitsDeployed){
			GameObject go = PhotonNetwork.Instantiate("TestUnit", HexGrid.instance.positions[cellID], Quaternion.identity, 0);
			go.transform.parent = HexMark.instance.GetRoot();

			UnitController uc = go.GetComponent<UnitController>();
			//uc.spawnId = units.Count;
			go.name = "PlayerUnit"+units.Count;//+= uc.spawnId;
			//uc.owner = PhotonNetwork.playerName;
			units.Add(uc);

			/*if (units.Count == 3){
				photonView.RPC("AllUnitsPlaced", PhotonTargets.Others, null);
			}*/
		}
	}


	/*public void AllUnitsPlaced(){
		UnitController[] all_ucs = FindObjectsOfType<UnitController>();
		for (int i = 0; i < all_ucs.Length; i++) {
			if(!units.Contains(all_ucs[i])){
				enemyUnits.Add(all_ucs[i]);
				all_ucs[i].transform.parent = HexMark.instance.GetRoot();
			}
		}
		Debug.Log("Found "+enemyUnits.Count+" Enemies!");
	}*/
}
