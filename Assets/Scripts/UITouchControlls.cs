using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITouchControlls : MonoBehaviour {

	public Animator actionButton;
	public Animator sidebar;
	public Animator actionPlus;
	public GameObject movebutton;

	private bool animationState = false;
	// Use this for initialization
	void Start () {
		movebutton.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
			
	}



	public void SwitchSideBar (){
		animationState = actionButton.GetBool ("actionbar_open");
		if (animationState == false) {
			movebutton.SetActive (true);
			actionButton.SetBool ("actionbar_open", true);
			actionPlus.SetBool ("action_closed", true);
			//Debug.Log ("open");
		} else {
			actionButton.SetBool ("actionbar_open", false);
			actionPlus.SetBool ("action_closed", false);
			movebutton.SetActive (false);

			//Debug.Log ("close");

		}
	}

	public void SwitchActionButton (){
		animationState = actionButton.GetBool("actionbar_open");
		if (animationState == false) {
			movebutton.SetActive(true);
			actionButton.SetBool ("actionbar_open", true);
			actionPlus.SetBool ("action_closed", true);
			//Debug.Log ("open");
		}
		else {
		actionButton.SetBool ("actionbar_open", false);
			actionPlus.SetBool ("action_closed", false);
			movebutton.SetActive(false);

			//Debug.Log ("close");
		
		}
	
	
	}
}
