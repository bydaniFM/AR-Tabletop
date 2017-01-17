using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARTCards;

public class GameManager : MonoBehaviour {

    private GameObject startMenuUI;
    private GameObject mainUI;
    private GameObject mainCamera;
    private GameObject ARCamera;
    private GameObject Board;

    private Player player1;
    private Player player2;
    private Unit[] unclaimedUnits;

    private TurnManager turnManager;

    private IEnumerator timer;

    private bool nextPhase;
    private bool inSetup;
    private bool inPlacing;
    private bool inClaiming;
    private int tTimer;
    private int nTurn;
    private int playerTurn;
    private int index1;
    private int index2;
    private int selectedUnit;
    
    public bool wantToInjectCard;
    public Canvas askToUseCard;

    void Start() {
        startMenuUI = GameObject.Find("canvas_start_menu");
        mainUI = GameObject.Find("canvas_main_UI");
        mainCamera = GameObject.Find("Main Camera");
        ARCamera = GameObject.Find("ARCamera");
        Board = GameObject.Find("Board");
        
        mainUI.SetActive(false);
        ARCamera.SetActive(false);
        Board.SetActive(false);

        player1 = new Player();
        player2 = new Player();
        unclaimedUnits = new Unit[6];

        timer = Timer();

        nextPhase = false;
        inSetup = false;
        inPlacing = true;
        inClaiming = false;
        tTimer = 0;
        playerTurn = 1;
        index1 = 0;
        index2 = 0;
        nTurn = 1;
        selectedUnit = 0;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && Input.touchCount < 2) {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("hit " + hit.point);
                //1.- Setup
                if (inSetup) {
                    Debug.Log("In Setup");
                    if (inPlacing) {
                        Debug.Log("In placing");
                        if (playerTurn == 1) {
                            Debug.Log("Player1 has placed an unit");
                            unclaimedUnits[index1 + index2] = new Unit(); //This unit has to be placed in "hit"
                            index1++;
                        }
                        if (playerTurn == 2) {
                            Debug.Log("Player2 has placed an unit");
                            unclaimedUnits[index1 + index2] = new Unit(); //This unit has to be placed in "hit"
                            index2++;
                        }
                        ChangeTurn();
                    } else if (inClaiming) {
                        Debug.Log("In claiming");
                        if (playerTurn == 1) {
                            if (hit.collider.gameObject.tag == "Unit") {
                                if (unclaimedUnits[index1 + index2].player == 0) {
                                    player1.units[index1] = unclaimedUnits[index1 + index2];
                                    player1.units[index1].player = 1;
                                }
                            }
                            index1++;
                        }
                        if (playerTurn == 2) {
                            if (hit.collider.gameObject.tag == "Unit") {
                                if (unclaimedUnits[index1 + index2].player == 0) {
                                    player2.units[index2] = unclaimedUnits[index1 + index2];
                                    player2.units[index2].player = 2;
                                }
                            }
                            index2++;
                        }
                        ChangeTurn();
                    }
                    if (index1 >= 3 && index2 >= 3) {
                        index1 = index2 = 0;
                        if (inPlacing) {
                            inPlacing = false;
                            inClaiming = true;
                        } else {
                            inSetup = false;
                        }
                    }
                } else {    //Normal turn
                    Debug.Log("In draw/activate card");
                    //2- Turns start: physical attribute card draw
                    //Ask the player if wants to inject. We activate the AskUseCard Canvas
                    Debug.Log("Do you want to inject an attribute card?");
                    askToUseCard.enabled = true;

                    if (playerTurn == 1) {
                        //scanning of the card: the Scanned Card Activator calls
                        if (wantToInjectCard) {
                            //activate injection process: put the car in front
                            //of the camera to begin the process
                        }
                        //3- Planning phase
                        if (selectedUnit != 0) {
                            //player1.units[selectedUnit - 1]."MoveUnit(hit.point)";
                        }
                    }
                    if (playerTurn == 2) {
                        //scanning of the card: the Scanned Card Activator calls
                        if (wantToInjectCard) {
                            //activate injection process: put the car in front
                            //of the camera to begin the process
                        }
                        //
                        if (selectedUnit != 0) {
                            //player2.units[selectedUnit - 1]."MoveUnit(hit.point)";
                        }
                    }
                    // Execute phase
                    //Apply attribute cards
                    for (int i = 0; i < player1.units.Length; i++) {
                        //Check here if unit is in range with other enemy units
                        //if (player1.units[i].attrs.TryGetValue("Range"))
                    }
                    for (int i = 0; i < player2.units.Length; i++) {
                        //Check here if unit is in range with other enemy units
                        //if (player2.units[i].attrs.TryGetValue("Range"))
                    }
                    //Attack sub-phase. Check the initiative!!

                    //Check winning condition
                    if (!inSetup && player1.units.Length <= 0) {
                        Debug.Log("Player 1 wins");
                    } else if (!inSetup && player1.units.Length <= 0) {
                        Debug.Log("Player 2 wins");
                    }
                    askToUseCard.enabled = false;
                    nTurn++;
                    ChangeTurn();
                }//End setup-normalTurn
            }
        }
    }

    public void FindGame() {
        Debug.Log("Finding Game");
        startMenuUI.SetActive(false);
        StartGame();
    }

    public void StartGame() {
        Debug.Log("Starting Game");
        mainUI.SetActive(true);
        mainCamera.SetActive(false);
        ARCamera.SetActive(true);
        Board.SetActive(true);
        inSetup = true;
    }

    public void ReturnToMenu() {
        mainUI.SetActive(false);
        mainCamera.SetActive(true);
        ARCamera.SetActive(false);
        Board.SetActive(false);
    }

    private void ChangeTurn() {
        if (playerTurn == 1)
            playerTurn = 2;
        else
            playerTurn = 1;

        Debug.Log("Turn of player " + playerTurn);
    }

    public void SelectUnit(int nUnit) {
        Debug.Log("You have selected your unit number " + nUnit);
        selectedUnit = nUnit;
    }

    /// <summary>
    /// Called when the button "accept, i want to inject a card"
    /// is pressed. Starts the injection process, first with scanning
    /// </summary>
    public void WantToInjectCard() {
        Debug.Log("Player want to use card");
        wantToInjectCard = true;
    }

    /// <summary>
    /// Called when the player press "No, do not inject any att card"
    /// </summary>
    public void DoNotWantToInjectCard() {
        Debug.Log("Player does not want to use card");
        wantToInjectCard = false;
    }

    public void StartTimer() {
        if (tTimer != 0)
            StopCoroutine(timer);
        tTimer = 0;
        StartCoroutine(timer);
    }
    IEnumerator Timer() {
        do {
            yield return new WaitForSeconds(1);
            tTimer++;
        } while (true);
    }
}