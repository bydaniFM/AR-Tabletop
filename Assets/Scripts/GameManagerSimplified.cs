using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSimplified : MonoBehaviour {

    public int nTurn;
    public int nPlayers;
    public int tTimer;
    public bool bNextPhase;
    public int nWinner;
    //      -1 -> No winner
    //       0 -> Tie
    // nPlayer -> Player "n" wins
    public bool exit;

    //private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.

    //Awake is called before Start function
    void Start () {
        Debug.Log("Starting Game");
        //do {
            nTurn = 1;
            nPlayers = 2;
            tTimer = 0;
            bNextPhase = false;
            nWinner = -1;

            StartCoroutine(Timer());

            Setup();

            //do {
            //    for (int i = 0; i < nPlayers; i++) {
            //        PlanningPhase(i);
            //        ExecutePhase(i);
            //    }
            //} while (nWinner == -1);
        //} while (!exit);
    }
	
	//// Touch Controls
	//void Update () {
 //       int horizontal = 0;     //Used to store the horizontal move direction.
 //       int vertical = 0;       //Used to store the vertical move direction.
 //       //Check if Input has registered more than zero touches
 //       if (Input.touchCount > 0) {
 //           //Store the first touch detected.
 //           Touch myTouch = Input.touches[0];

 //           //Check if the phase of that touch equals Began
 //           if (myTouch.phase == TouchPhase.Began) {
 //               //If so, set touchOrigin to the position of that touch
 //               touchOrigin = myTouch.position;
 //           }

 //           //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
 //           else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
 //               //Set touchEnd to equal the position of this touch
 //               Vector2 touchEnd = myTouch.position;

 //               //Calculate the difference between the beginning and end of the touch on the x axis.
 //               float x = touchEnd.x - touchOrigin.x;

 //               //Calculate the difference between the beginning and end of the touch on the y axis.
 //               float y = touchEnd.y - touchOrigin.y;

 //               //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
 //               touchOrigin.x = -1;

 //               //Check if the difference along the x axis is greater than the difference along the y axis.
 //               if (Mathf.Abs(x) > Mathf.Abs(y))
 //                   //If x is greater than zero, set horizontal to 1, otherwise set it to -1
 //                   horizontal = x > 0 ? 1 : -1;
 //               else
 //                   //If y is greater than zero, set horizontal to 1, otherwise set it to -1
 //                   vertical = y > 0 ? 1 : -1;
 //           }
 //       }
 //   }



    /*_________________________________________________________________________________________________________*/


    // Just choose unit
    public void Setup() {
        Debug.Log("Starting Game");
    }

    public void PlanningPhase(int nUnit) {
        Debug.Log("Planing Phase");
        GameObject.Find("UI_portraits").SetActive(false);
    }

    public void ExecutePhase(int nPlayer) {
        Debug.Log("Execute!");
    }

    IEnumerator Timer() {
        do {
            yield return new WaitForSeconds(1);
            tTimer++;
        } while (true);
    }
}
