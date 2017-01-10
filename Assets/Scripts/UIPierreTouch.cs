using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPierreTouch: MonoBehaviour {

	public Text announcer;

	//public AudioSource fightaudio;

	public Animator topinfo;
	public Animator actionButton;
	public Animator charsheet;
	public Animator actionPlus;

	public GameObject char_button_1;
	public GameObject char_button_2;
	public GameObject char_button_3;

	//	public GameObject rotate_button_left;
	//	public GameObject rotate_button_right;
	public Slider slider;
	public GameObject rotate_slider;
	public GameObject freeze_button;
	public GameObject unfreeze_button;

	private bool animationState = false;
	private int rounds;
	private bool rotate;
	private bool counter_rotate;
	private float rotationspeed;
	private bool freeze;
	private Quaternion char_1_rotation;
	private Quaternion char_2_rotation;
	private Quaternion char_3_rotation;

	private GameObject toberotated;

	// Use this for initialization
	void Start () {
		char_button_1.SetActive (false);
		char_button_2.SetActive (false);
		char_button_3.SetActive (false);
		rotate_slider.SetActive(false);
		freeze_button.SetActive (false);
		unfreeze_button.SetActive (false);
		slider.value = 0f;

		char_1_rotation = char_button_1.transform.rotation;
		char_2_rotation = char_button_2.transform.rotation;
		char_3_rotation = char_button_3.transform.rotation;
	}

	// Update is called once per frame
	void Update () {

		if (freeze == false) {
			toberotated.transform.Rotate (0, rotationspeed * Time.deltaTime, 0);

		}
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
				rotate_slider.SetActive (false);
				freeze_button.SetActive (false);
				unfreeze_button.SetActive (false);
				break;
			}
			char_button_1.SetActive (true);
			char_button_2.SetActive (false);
			char_button_3.SetActive (false);
			toberotated = char_button_1;
			rotate_slider.SetActive (true);
			slider.value = 0f;
			char_button_1.transform.rotation = char_1_rotation;
			freeze_button.SetActive (true);

			break;

		case 2:
			if (char_button_2.activeSelf == true) {
				char_button_2.SetActive (false);
				rotate_slider.SetActive(false);
				freeze_button.SetActive (false);
				unfreeze_button.SetActive (false);

				break;
			}
			char_button_1.SetActive (false);
			char_button_2.SetActive (true);
			char_button_3.SetActive (false);
			toberotated = char_button_2;
			rotate_slider.SetActive(true);
			slider.value = 0f;
			char_button_2.transform.rotation = char_2_rotation;
			freeze_button.SetActive (true);
			//			rotate_button_left.SetActive (true);
			//			rotate_button_right.SetActive (true);
			break;

		case 3:
			if (char_button_3.activeSelf == true) {
				char_button_3.SetActive (false);
				rotate_slider.SetActive(false);
				freeze_button.SetActive (false);
				unfreeze_button.SetActive (false);
				break;
			}
			char_button_1.SetActive (false);
			char_button_2.SetActive (false);
			char_button_3.SetActive (true);
			toberotated = char_button_3;
			rotate_slider.SetActive(true);
			slider.value = 0f;
			char_button_3.transform.rotation = char_3_rotation;
			freeze_button.SetActive (true);

			break;


		}
	}


	//	public void RotateCharacter(int direction){
	//
	//
	//		switch (direction) {
	//		case 1:
	//			if (char_button_1.activeSelf == true) {
	//				rotate = true;
	//				counter_rotate = false;
	//
	//				test = char_button_1;
	//			} else if (char_button_2.activeSelf == true) {
	//				rotate = true;
	//				counter_rotate = false;
	//
	//				test = char_button_2;
	//			} else {
	//				rotate = true;
	//				counter_rotate = false;
	//
	//				test = char_button_3;
	//			}
	//			break;
	//
	//		case 2:
	//			if (char_button_1.activeSelf == true) {
	//				counter_rotate = true;
	//				rotate = false;
	//
	//				test = char_button_1;
	//			} else if (char_button_2.activeSelf == true) {
	//				counter_rotate = true;
	//				rotate = false;
	//				test = char_button_2;
	//			} else {
	//				counter_rotate = true;
	//				rotate = false;
	//				test = char_button_3;
	//			}
	//			break;
	//		}
	//
	//
	//	}


	public void AdjustRotation(float newrotation){
		rotationspeed = newrotation;

	}


	public void FreezeRotation(){

		freeze = true;
		freeze_button.SetActive (false);
		unfreeze_button.SetActive (true);
	}

	public void UnFreezeRotation(){

		freeze = false;
		freeze_button.SetActive (true);
		unfreeze_button.SetActive (false);
	}
}