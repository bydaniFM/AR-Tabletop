using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARTCards;
using HexMap;

public class GameManager : MonoBehaviour {

    private GameObject startMenuUI;
    private GameObject mainUI;
    private GameObject atributeCardInjectorUI;
    private GameObject askToUseCardUI;
    private GameObject finishPlanningUI;
    private GameObject mainCamera;
    private GameObject ARCamera;
    private GameObject Board;

    private Player player1;
    private Player player2;
    private GameObject imageTarget;
    private GameObject[] unclaimedUnits;
    private GameObject tempUnit;
    private RaycastHit plannedRaycast1;
    private RaycastHit plannedRaycast2;

    private TurnManager turnManager;

    private IEnumerator timer;

    private bool inSetup;
    private bool inPlacing;
    private bool inClaiming;
    private bool allowCardScanning;
    private bool wantToInjectCard;
    private bool unitSelected;
    private bool inExecute;
    private int tTimer;
    private int nTurn;
    private int playerTurn;
    private int index1;
    private int index2;
    private int selectedUnit;

    private FieldOrientationAssistant assist;

    void Start() {
        startMenuUI = GameObject.Find("canvas_start_menu");
        mainUI = GameObject.Find("canvas_main_UI");
        atributeCardInjectorUI = GameObject.Find("canvas_CardScanning");
        askToUseCardUI = GameObject.Find("canvas_AskToUseCard");
        finishPlanningUI = GameObject.Find("canvas_FinishPlanning");
        mainCamera = GameObject.Find("Main Camera");
        ARCamera = GameObject.Find("ARCamera");
        Board = GameObject.Find("Board");
        imageTarget = GameObject.Find("ImageTarget_Grid");
        assist = FindObjectOfType<FieldOrientationAssistant>();
        
        mainUI.SetActive(false);
        atributeCardInjectorUI.SetActive(false);
        askToUseCardUI.SetActive(false);
        finishPlanningUI.SetActive(false);
        ARCamera.SetActive(false);
        Board.SetActive(false);

        player1 = new Player();
        player2 = new Player();
        unclaimedUnits = new GameObject[6];
        tempUnit = new GameObject();

        timer = Timer();

        inExecute = false;
        inSetup = false;
        inPlacing = true;
        inClaiming = false;
        allowCardScanning = false;
        unitSelected = false;
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
                Debug.Log("hit " + hit.point + ". In object: " + hit.collider.gameObject);
                //1.- Setup
                if (inSetup) {
                    Debug.Log("In Setup");
                    if (inPlacing) {
                        Debug.Log("In placing");
                        if (playerTurn == 1) {
                            Debug.Log("Player1 has placed an unit");
                            unclaimedUnits[index1 + index2] = (GameObject)Instantiate(Resources.Load("Scampi"));
                            int hexid = HexGrid.instance.GetCellId(assist.WorldToGrid(hit.point));
                            Debug.Log("There are "+HexGrid.instance.positions.Length+" coordinates and id is "+hexid);
                            Vector3 pos = assist.GridToWorld(HexGrid.instance.positions[hexid]);
                            unclaimedUnits[index1 + index2].gameObject.transform.position = pos;
                            unclaimedUnits[index1 + index2].transform.parent = imageTarget.transform;
                            unclaimedUnits[index1 + index2].gameObject.transform.rotation = imageTarget.transform.rotation;
                            unclaimedUnits[index1 + index2].gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                            index1++;
                        }
                        if (playerTurn == 2) {
                            Debug.Log("Player2 has placed an unit");
                            unclaimedUnits[index1 + index2] = (GameObject)Instantiate(Resources.Load("Scampi"));
                            int hexid = HexGrid.instance.GetCellId(assist.WorldToGrid(hit.point));
                            Debug.Log("There are " + HexGrid.instance.positions.Length + " coordinates and id is " + hexid);
                            Vector3 pos = assist.GridToWorld(HexGrid.instance.positions[hexid]);
                            unclaimedUnits[index1 + index2].gameObject.transform.position = pos;
                            unclaimedUnits[index1 + index2].transform.parent = imageTarget.transform;
                            unclaimedUnits[index1 + index2].gameObject.transform.rotation = imageTarget.transform.rotation;
                            unclaimedUnits[index1 + index2].gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                            index2++;
                        }
                        ChangeTurn();
                    } else if (inClaiming) {
                        Debug.Log("In claiming");
                        if (hit.collider.gameObject.tag == "Unit") {
                            Debug.Log("Unit Claimed");
                            if (playerTurn == 1) {
                                //Check if unit selected has owner already, if not, claim it
                                if (unclaimedUnits[index1 + index2].GetComponent<UnitController>().unit.player == 0) {
                                    player1.units[index1].player = 1;
                                    player1.units[index1] = unclaimedUnits[index1 + index2].GetComponent<UnitController>().unit;
                                }
                                index1++;
                            }
                            if (playerTurn == 2) {
                                    //Check if unit selected has owner already, if not, claim it
                                if (unclaimedUnits[index1 + index2].GetComponent<UnitController>().unit.player == 0) {
                                    player2.units[index2].player = 2;
                                    player1.units[index1] = unclaimedUnits[index1 + index2].GetComponent<UnitController>().unit;
                                }
                                index2++;
                            }
                            ChangeTurn();
                        }
                    }
                    if (index1 >= 3 && index2 >= 3) {
                        index1 = index2 = 0;
                        if (inPlacing) {
                            inPlacing = false;
                            inClaiming = true;
                        } else {
                            inSetup = false;
                            finishPlanningUI.SetActive(true);
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
                        if (!unitSelected) {
                            if (hit.collider.gameObject.tag == "Unit" && hit.collider.gameObject.GetComponent<UnitController>().unit.player == 1) {
                                Debug.Log("Unit Selected");
                                tempUnit = hit.collider.gameObject;
                                unitSelected = true;
                            }
                        } else {
                            Debug.Log("Path created");
                            tempUnit.GetComponent<UnitController>().PreparePath(hit.point);
                            unitSelected = false;
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
                        if (!unitSelected) {
                            if (hit.collider.gameObject.tag == "Unit" && hit.collider.gameObject.GetComponent<UnitController>().unit.player == 2) {
                                Debug.Log("Unit Selected");
                                tempUnit = hit.collider.gameObject;
                                unitSelected = true;
                            }
                        } else {
                            Debug.Log("Path created");
                            tempUnit.GetComponent<UnitController>().PreparePath(hit.point);
                            unitSelected = false;
                        }
                    }
                    // Execute phase
                    if (inExecute) {
                        Debug.Log("EXECUTE PHASE");
                        finishPlanningUI.SetActive(false);
                        //Move units
                        tempUnit.GetComponent<UnitController>().StartPath();
                        tempUnit.GetComponent<UnitController>().FollowPath();

                        //Attack sub-phase. Check the initiative!!
                        for (int i = 0; i < player1.units.Length; i++) {
                            //Check here if unit is in range with other enemy units
                            //if (player1.units[i].attrs.TryGetValue("Range"))
                        }
                        for (int i = 0; i < player2.units.Length; i++) {
                            //Check here if unit is in range with other enemy units
                            //if (player2.units[i].attrs.TryGetValue("Range"))
                        }
                        
                        finishPlanningUI.SetActive(true);

                        //Check winning condition
                        if (!inSetup && player1.units.Length <= 0) {
                            Debug.Log("Player 1 wins");
                            finishPlanningUI.SetActive(true);
                        } else if (!inSetup && player1.units.Length <= 0) {
                            Debug.Log("Player 2 wins");
                            finishPlanningUI.SetActive(true);
                        }
                        askToUseCardUI.SetActive(false);
                        nTurn++;
                        ChangeTurn();
                    }
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

    public void StartExecutePhase() {
        inExecute = true;
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