using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMap;

public class FakeEnemy : MonoBehaviour {

	public Vector3[] destinations;
	public bool proximityKill;
	int curdest;
	UnitController unit;

	FireWeapon weapon;
	SelfDestruct player;

	bool movin;
	// Use this for initialization
	void Start () {
		unit = GetComponent<UnitController>();
		weapon = GetComponent<FireWeapon>();
		player = GameObject.Find("Player").GetComponent<SelfDestruct>();
		//Debug.Log(player);
	}

	void Update(){
		if (movin && !unit.isMoving && proximityKill){
			ProximityCheck();
		}

		movin = unit.isMoving;
	}


	public void PlotNext(){
		unit.PreparePath(destinations[curdest]);
	}

	public void GotoNext(){
		unit.StartPath();
		curdest++;
		if (curdest >= destinations.Length){
			curdest = 0;
		}

	}

	void ProximityCheck(){
		Debug.Log(player);
		player = GameObject.Find("Player").GetComponent<SelfDestruct>();
		if (Vector3.Distance(player.transform.position, transform.position) < 4){
			transform.LookAt(player.transform);
			weapon.Shoot();
			player.Execute(2.5f);
		}
	}

}
