using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitAnimator : MonoBehaviour {
	public string toIdle;
	public string toRun;
	public string toAttack;
	private int idleHash;
	private int runHash;
	private int attackHash;
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		//hash = new int[parameters.Length];
		List<string> paramNames = new List<string>();
		paramNames = animator.parameters.Select(x=>x.name).ToList();

		if (paramNames.Contains(toIdle)){
			idleHash = Animator.StringToHash(toIdle);
		}
		else {
			Debug.LogError("The animator on "+gameObject.name+" doesn't contain parameter " + toIdle);
		}

		if (paramNames.Contains(toRun)){
			runHash = Animator.StringToHash(toRun);
		}
		else {
			Debug.LogError("The animator on "+gameObject.name+" doesn't contain parameter " + toRun);
		}
		if (paramNames.Contains(toAttack)){
			attackHash = Animator.StringToHash(toAttack);
		}
		else {
			Debug.LogError("The animator on "+gameObject.name+" doesn't contain parameter " + toAttack);
		}
	}

	public void Run(){
		animator.SetTrigger(toRun);
	}
	public void Attack(){
		animator.SetTrigger(toAttack);
	}
	public void Idle(){
		animator.SetTrigger(toIdle);
	}
}

//public class Anim
