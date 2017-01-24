using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMap;

public class EnemyControl : MonoBehaviour, IPlayerControl {

	public bool ready;
	int _curUnit = -1;
	private List<UnitController> units;
	//private PlayerControl player;
	public Player enemy;
	// Use this for initialization
	void Awake () {
		units = new List<UnitController>();
		//player = FindObjectOfType<PlayerControl>();
		enemy = new Player();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void UpdateControllers(){
		for (int i = 0; i < units.Count; i++) {
			units[i].move = enemy.units[i].stats["Mov"].Value;
			units[i].range = enemy.units[i].stats["Ran"].Value;
			units[i].init = enemy.units[i].stats["Ini"].Value;
			units[i].damage = enemy.units[i].damage;
		}
	}
	public void AddUnit(UnitController uc){
		units.Add(uc);
		uc.name = "EnemyUnit"+units.Count;
		Debug.Log("Added enemy "+uc.name);
	}

	public void MovementPhase(){
		if (_curUnit != -1){
			units[_curUnit].StartPath();
			units[_curUnit].SetOnMoveEnd(() => OnMoveEnd() );
			HexMark.instance.Unmark("Enemy");
		}
		else{
			Debug.Log("Cant Move Enemy!");
		}
	}

	public void OnMoveEnd(){
		_curUnit = -1;
		Debug.Log("Ended Move on Enemy");
	}
	public UnitController[] GetUnits(){
		return units.ToArray();
	}
	public bool isOwner(UnitController uc){
		return units.Contains(uc);
	}
	public void BuildPath(Vector3 dest){
		units[_curUnit].PreparePath(dest);
		Debug.Log("EnemyPathSet!");
		HexMark.instance.MarkGrid("Enemy", units[_curUnit].GetWaypoints(), Color.red);
	}

	public void SelectUnit(UnitController uc){
		if (units.Contains(uc)){
			SelectUnit(units.IndexOf(uc));
		}
	}
	public void SelectUnit(int id){
		_curUnit = id;
	}
	public void Damage(UnitController uc, int damage){
		int ind = units.IndexOf(uc);
		enemy.units[ind].stats["HP"] -= damage;
		if (enemy.units[ind].stats["HP"] <= 0){
			SelfDestruct sd = units[ind].GetComponent<SelfDestruct>();
			sd.Execute(3f);
			InitiativeManager.Exclude(uc);
		}
	}
	public bool unitsDeployed{
		get { return units.Count == 3; }
	}
	public int curUnit{
		get{ return _curUnit; }
	}
}
