using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerSimplified : MonoBehaviour {

    public int nTurn;
    public int nPhase;
    public int nPlayers;
    public int nUnit;
    public int tTimer;
    public bool timerRunning;
    public bool bNextPhase;
    public int nWinner;
    //      -1 -> No winner
    //       0 -> Tie
    // nPlayer -> Player "n" wins
    public bool exit;

    public GameObject myUnit;
    public GameObject Octo;
    public GameObject Scampi;
    public GameObject Crab;

    public Text TimerText;

    //private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.
    
    void Start () {
        Debug.Log("Starting Game");

        nTurn = 1;
        nPhase = 0;
        nPlayers = 2;
        nUnit = 0;
        tTimer = 0;
        bNextPhase = false;
        timerRunning = false;
        nWinner = -1;

        Octo = GameObject.Find("Octo_base");
        Scampi = GameObject.Find("scampi_base");
        Crab = GameObject.Find("Crab_base");
        
        StartCoroutine(Timer());

    }

    void Update () {
        TimerText.text = tTimer.ToString();
    }

    //Change into SelectUnit(int nUnit)
    public void PlanningPhase(int nUnit) {
        Debug.Log("Planing Phase");
        this.nUnit = nUnit;
        Debug.Log("Unit selected: "+nUnit);
    }

    //Change into PlaceUnit()
    public void MovePhase() {
        if (nPhase == 0) {
            nPhase = 1;
            if (nUnit != 0) {
                Debug.Log("Move Phase");
                if (nUnit == 1) {
                    myUnit = Octo;
                }
                if (nUnit == 2) {
                    myUnit = Scampi;
                }
                if (nUnit == 3) {
                    myUnit = Crab;
                }
                myUnit.transform.parent = GameObject.Find("ImageTarget 1").transform;
                myUnit.transform.localPosition = new Vector3(0, 0, 0);
                myUnit.transform.localRotation = new Quaternion(0, 0, 0, 0);
                myUnit.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
        }
    }

    public void ResetTimer() {
        tTimer = 0;
    }

    IEnumerator Timer() {
        do {
            timerRunning = true;
            yield return new WaitForSeconds(1);
            tTimer++;
        } while (true);
    }

    public int GetTimer() {
        return tTimer;
    }
}
