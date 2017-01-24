using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState{
	WaitingForOther,
	Deployment,
	Cards,
	EffectCard,
	Planning,
	Ready,
	Movement,
	Engagement,
	GameOver,
	Disconnecting
}
public class RoundController : MonoBehaviour {

	public static GameState curState;
	//private GameState prevState;

	PlayerControl pc;
	EnemyControl ec;

	static RoundController instance;
	void Awake(){
		if (instance == null){
			instance = this;
		}
		else if (instance != this){
			Debug.LogError("An instance of RoundController already exists!");

		}
	}
	// Use this for initialization
	void Start () {
		pc = FindObjectOfType<PlayerControl>();
		ec = FindObjectOfType<EnemyControl>();
	}
	
	// Update is called once per frame
	void Update () {

		if (curState == GameState.Deployment && pc.unitsDeployed && ec.unitsDeployed){
			InitiativeManager.Initialize();
			SwitchState(GameState.Cards);
			pc.UpdateControllers();
			ec.UpdateControllers();
		}
		if (curState == GameState.Ready && ec.ready){
			pc.MovementPhase();
			ec.MovementPhase();
			ec.ready = false;
			SwitchState(GameState.Movement);
			InitiativeManager.SortOrder();
		}
		if (curState == GameState.Movement && pc.curUnit == -1 && ec.curUnit == -1){
			SwitchState(GameState.Engagement);
			InitiativeManager.StartCombat();
		}
	}

	public static void SwitchState(GameState newState){
		//instance.prevState = curState;
		curState = newState;
	}
}
