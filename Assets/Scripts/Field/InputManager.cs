using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexMap;
using ARTCards;

public class InputManager : MonoBehaviour {

	PlayerControl pc;
	EffectTypes curEffect;
	// Use this for initialization
	void Start () {
		pc = FindObjectOfType<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if (RoundController.curState == GameState.Deployment){
			if (Input.GetMouseButtonDown(0) && Input.touchCount < 2) {
				RayProbe();
			}


		}	
		else if ( RoundController.curState == GameState.Cards){
			if (Input.GetMouseButtonDown(0) && Input.touchCount < 2) {
				RayProbe();
			}
		}
		else if (RoundController.curState == GameState.EffectCard){
			if (Input.GetMouseButtonDown(0) && Input.touchCount < 2) {
				curEffect = pc.player.activeCard.type;
				EffectProbe();
			}

		}
		else if ( RoundController.curState == GameState.Planning){
			if (Input.GetMouseButtonDown(0) && Input.touchCount < 2) {
				RayProbe();
			}
			if (Input.GetMouseButtonDown(1) || Input.touchCount == 2){
				if(pc.canEndRound){
					RoundController.SwitchState(GameState.Ready);

					NetworkController.instance.photonView.RPC("Ready", PhotonTargets.Others, null);
				}
			}
		}

		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Backspace)){
			PhotonNetwork.LeaveRoom();
		}
	}

	void RayProbe(){
		//int nav = LayerMask.NameToLayer("NavMesh");
		RaycastHit hit = new RaycastHit();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit)){
			//Debug.Log("hit "+hit.point);
			if (RoundController.curState == GameState.Deployment){
				int cellID = HexGrid.instance.GetCellId(hit.point);
				if (cellID != -1){
					pc.PlaceUnit(cellID);
				}
			}
			else if (RoundController.curState == GameState.Planning){
				if (hit.collider.tag == "Unit"){
					UnitController uc = hit.collider.GetComponent<UnitController>();
					pc.SelectUnit(uc);
				}
				else{
					pc.BuildPath(hit.point);
				}
			}
		}

	
	}

	void EffectProbe(){
		RaycastHit hit = new RaycastHit();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)){
			if (curEffect == EffectTypes.Teleport){
				int cellID = HexGrid.instance.GetCellId(hit.point);
				pc.GetUnits()[pc.curUnit].transform.position = HexGrid.instance.positions[cellID];
			}
		}
	}
}
