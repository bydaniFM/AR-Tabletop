using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InitiativeManager : MonoBehaviour {
	List<UnitController> order;
	int index;
	PlayerControl pc;
	EnemyControl ec;
	static InitiativeManager instance;
	void Awake(){
		if (instance == null){
			instance = this;
		}
		else if (instance != this){
			Debug.LogError("An instance of InitiativeManager already exists!");

		}
		order = new List<UnitController>();
	}
	// Use this for initialization
	void Start () {
		pc = FindObjectOfType<PlayerControl>();
		ec = FindObjectOfType<EnemyControl>();
	}
	

	public static void SortOrder(){
		instance.order.Sort((x,y) => x.init.CompareTo(y.init));
	}
	public static void StartCombat(){
		instance.index = -1;
		instance.NextCombatant();
	}
	public void NextCombatant(){
		index++;
		if (index >= order.Count){

			RoundController.SwitchState(GameState.Cards);
			return;
		}
		Debug.Log(index+". "+order[index].name + " Starts turn.");
		List<UnitController> targets = new List<UnitController>();
		IPlayerControl owner;
		if (pc.isOwner(order[index])){
			owner = ec;
		}
		else{
			owner = pc;
		}
		targets.AddRange(owner.GetUnits());
		//for (int i = 0; i < targets.Count; i++) {
		foreach (var item in targets.ToArray()) {
			float distance = Vector3.Distance(order[index].transform.position, item.transform.position);
			if (distance > order[index].range){
				targets.Remove(item);
			}
		}
		Debug.Log("Proceed to attack "+ targets.Count +" targets");
		Attack(targets.ToArray(), owner);
	}

	void Attack(UnitController[] targets, IPlayerControl owner){
		List<Transform> trn = new List<Transform>();
		for (int i = 0; i < targets.Length; i++) {
			owner.Damage(targets[i], order[index].damage/targets.Length);
			trn.Add(targets[i].transform);
			Debug.Log(order[index].name+" aims at "+targets[i].name);
		}
		if (trn.Count>0){
			FireWeapon fire = order[index].GetComponent<FireWeapon>();
			//fire.onDone = ()=>NextCombatant();
			fire.Shoot(trn.ToArray(), 3);

			LeanTween.delayedCall(gameObject, 3, ()=>NextCombatant());
		}
		else{
			NextCombatant();
			Debug.Log(order[index].name+" has no targets in range");
		}
	}
	public static void Initialize(){
		instance.order.AddRange(instance.pc.GetUnits());
		instance.order.AddRange(instance.ec.GetUnits());

	}
	public static void Exclude(UnitController uc){
		instance.order.Remove(uc);
	}
}
