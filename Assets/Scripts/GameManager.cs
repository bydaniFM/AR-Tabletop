using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private int Map = 1;

    public PlayerScript Player1;
    public PlayerScript Player2;

    public int nTurn;
    public int nPhase;
    public int nPlayers;
    public int tTimer;
    public bool bNextPhase;
    public bool bWinCondition;

    //Awake is called before Start function

    // Use this for initialization
    void Start () {
        nTurn = 1;
        nPhase = 1;
        nPlayers = 2;
        tTimer = 0;
        bNextPhase = false;
        bWinCondition = false;

        StartCoroutine(Timer());

        Player1 = new PlayerScript();
        Player2 = new PlayerScript();

        do {
            for (int i = 0; i < nPlayers; i++) {
                do {
                    DrawPhase();
                }while(!bNextPhase);
                do {
                    PlanningPhase();
                } while (!bNextPhase);
                do {
                    ExecutePhase();
                } while (!bNextPhase);
            }
        } while (!bWinCondition);
    }
	
	// Update is called once per frame
	void Update () {

	}



    /*_________________________________________________________________________________________________________*/

    public void DrawPhase() {
        do {
        } while (tTimer <= 30);
        tTimer = 0;
        nPhase++;
    }

    public void PlanningPhase() {
        do {
        } while (tTimer <= 30);
        tTimer = 0;
        nPhase++;
    }

    public void ExecutePhase() {
        do {
        } while (tTimer <= 30);
        tTimer = 0;
        nPhase++;
    }

    IEnumerator Timer() {
        do {
            yield return new WaitForSeconds(1);
            tTimer++;
        } while (true);
    }
}
