using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITouchControlls : MonoBehaviour {

	public Text announcer;

	//public AudioSource fightaudio;

	public Animator topinfo;
	public Animator actionButton;
	public Animator charsheet;
	public Animator actionPlus;

	public GameObject char_button_1;
	public GameObject char_button_2;
	public GameObject char_button_3;

	private bool animationState = false;
	private int rounds;
	// Use this for initialization
	void Start () {
		char_button_1.SetActive (false);
		char_button_2.SetActive (false);
		char_button_3.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {
		
			
	}



	public void SwitchCharSheet (){
		animationState = charsheet.GetBool ("isopen");
		if (animationState == false) {
			charsheet.SetBool ("isopen", true);

		} else {
			charsheet.SetBool ("isopen", false);


		}
	}

	public void SwitchActionBar (){
		animationState = actionButton.GetBool ("actionbar_open");
		if (animationState == false) {
			actionButton.SetBool ("actionbar_open", true);
			actionPlus.SetBool ("action_closed", true);
			//Debug.Log ("open");
		} else {
			actionButton.SetBool ("actionbar_open", false);
			actionPlus.SetBool ("action_closed", false);

			//Debug.Log ("close");
		
		}
	}

	public void RoundManager (){
		rounds++;
		switch (rounds){

		case 1:
			announcer.text = "Draw your card";
			topinfo.SetBool ("topaction_open", true);

			break;
		case 2:
			announcer.text = "Move your unit";
			break;

		case 3:
			announcer.text = "Fight";
		//	fightaudio.Play ();
			break;

		case 4:
			topinfo.SetBool ("topaction_open", false);
			announcer.text = "";
			rounds = 0;
			break;

		}
			
		}


	public void ModelViewer(int buttonnumber){

		switch (buttonnumber) {

		case 1:
			if (char_button_1.activeSelf == true) {
				char_button_1.SetActive (false);
				break;
			}
			char_button_1.SetActive (true);
			char_button_2.SetActive (false);
			char_button_3.SetActive (false);
			break;

		case 2:
			if (char_button_2.activeSelf == true) {
				char_button_2.SetActive (false);
				break;
			}
			char_button_1.SetActive (false);
			char_button_2.SetActive (true);
			char_button_3.SetActive (false);
			break;

		case 3:
			if (char_button_3.activeSelf == true) {
				char_button_3.SetActive (false);
				break;
			}
			char_button_1.SetActive (false);
			char_button_2.SetActive (false);
			char_button_3.SetActive (true);
			break;
		
		
		}
		}
		}
