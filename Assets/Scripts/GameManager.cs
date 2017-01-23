using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARTCards;

public class GameManager : MonoBehaviour {

    private GameObject startMenuUI;
    private GameObject mainUI;
    private GameObject atributeCardInjectorUI;
    private GameObject askToUseCardUI;
    private GameObject mainCamera;
    private GameObject ARCamera;
    private GameObject Board;

    private Player player1;
    private Player player2;
    private GameObject[] unclaimedUnits;
    private RaycastHit plannedRaycast1;
    private RaycastHit plannedRaycast2;

    private TurnManager turnManager;

    private IEnumerator timer;

    private bool nextPhase;
    private bool inSetup;
    private bool inPlacing;
    private bool inClaiming;
    private bool allowCardScanning;
    private bool wantToInjectCard;
    private int tTimer;
    private int nTurn;
    private int playerTurn;
    private int index1;
    private int index2;
    private int selectedUnit;

    void Start() {
        startMenuUI = GameObject.Find("canvas_start_menu");
        mainUI = GameObject.Find("canvas_main_UI");
        atributeCardInjectorUI = GameObject.Find("canvas_CardScanning");
        askToUseCardUI = GameObject.Find("canvas_AskToUseCard");
        mainCamera = GameObject.Find("Main Camera");
        ARCamera = GameObject.Find("ARCamera");
        Board = GameObject.Find("Board");
        
        mainUI.SetActive(false);
        atributeCardInjectorUI.SetActive(false);
        askToUseCardUI.SetActive(false);
        ARCamera.SetActive(false);
        Board.SetActive(false);

        player1 = new Player();
        player2 = new Player();
        unclaimedUnits = new GameObject[6];

        timer = Timer();

        nextPhase = false;
        inSetup = false;
        inPlacing = true;
        inClaiming = false;
        allowCardScanning = false;
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
                            //unclaimedUnits[index1 + index2] = new Unit(); //This unit has to be placed in "hit"
                            unclaimedUnits[index1 + index2] = (GameObject)Instantiate(Resources.Load("Scampi"));
                            unclaimedUnits[index1 + index2].transform.parent = GameObject.Find("ImageTarget_Grid").transform;
                            unclaimedUnits[index1 + index2].gameObject.transform.position = hit.transform.position;
                            unclaimedUnits[index1 + index2].gameObject.transform.rotation = Quaternion.identity;//new Quaternion(0, 0, 90, 0);
                            unclaimedUnits[index1 + index2].gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                            index1++;
                        }
                        if (playerTurn == 2) {
                            Debug.Log("Player2 has placed an unit");
                            //unclaimedUnits[index1 + index2] = new Unit(); //This unit has to be placed in "hit"
                            unclaimedUnits[index1 + index2] = (GameObject)Instantiate(Resources.Load("Scampi"));
                            unclaimedUnits[index1 + index2].gameObject.transform.position = hit.transform.position;
                            unclaimedUnits[index1 + index2].transform.parent = GameObject.Find("ImageTarget_Grid").transform;
                            index2++;
                        }
                        ChangeTurn();
                    } else if (inClaiming) {
                        Debug.Log("In claiming");
                        if (playerTurn == 1) {
                            if (hit.collider.gameObject.tag == "Unit") {
                                //Check if unit selected has owner already, if not, claim it
                                if (unclaimedUnits[index1 + index2].GetComponent<UnitController>().unit.player == 0) {
                                    player1.units[index1].player = 1;
                                    player1.units[index1] = unclaimedUnits[index1 + index2].GetComponent<UnitController>().unit;
                                }
                            }
                            index1++;
                        }
                        if (playerTurn == 2) {
                            if (hit.collider.gameObject.tag == "Unit") {
                                //Check if unit selected has owner already, if not, claim it
                                if (unclaimedUnits[index1 + index2].GetComponent<UnitController>().unit.player == 0) {
                                    player2.units[index2].player = 2;
                                    player1.units[index1] = unclaimedUnits[index1 + index2].GetComponent<UnitController>().unit;
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
                    askToUseCardUI.SetActive(true);
                    if (playerTurn == 1) {
                        //scanning of the card: the Scanned Card Activator calls
                        if (wantToInjectCard) {
                            //activate injection process: put the car in front
                            //of the camera to begin the process
                            atributeCardInjectorUI.SetActive(true);
                        }
                        //3- Planning phase
                        if (selectedUnit != 0) {
                            plannedRaycast1 = hit;
                        }
                    }
                    if (playerTurn == 2) {
                        //scanning of the card: the Scanned Card Activator calls
                        if (wantToInjectCard) {
                            //activate injection process: put the car in front
                            //of the camera to begin the process
                            atributeCardInjectorUI.SetActive(true);
                        }
                        //3- Planning phase
                        if (selectedUnit != 0) {
                            plannedRaycast2 = hit;
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
                    //Move units

                    //Attack sub-phase. Check the initiative!!

                    //Check winning condition
                    if (!inSetup && player1.units.Length <= 0) {
                        Debug.Log("Player 1 wins");
                    } else if (!inSetup && player1.units.Length <= 0) {
                        Debug.Log("Player 2 wins");
                    }
                    askToUseCardUI.SetActive(false);
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