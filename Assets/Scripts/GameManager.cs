using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private int Map = 1;

    public PlayerScript Player1;
    public PlayerScript Player2;
    public BoardScript Board;

    public int nTurn;
    public int nPlayers;
    public int tTimer;
    public bool bNextPhase;
    public int nWinner;
    //      -1 -> No winner
    //       0 -> Tie
    // nPlayer -> Player "n" wins
    public bool exit;

    private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.

    //Awake is called before Start function

    void Awake () {
        do {
            nTurn = 1;
            nPlayers = 2;
            tTimer = 0;
            bNextPhase = false;
            nWinner = -1;

            StartCoroutine(Timer());

            Player1 = new PlayerScript();
            Player2 = new PlayerScript();

            Setup();

            do {
                for (int i = 0; i < nPlayers; i++) {
                    //Changing functions into coroutines
                    DrawPhase(i);
                    PlanningPhase(i);
                    ExecutePhase(i);

                    nWinner = CheckWinCondition();
                    if (nWinner == 0) {
                        //Display Tie
                    } else if (nWinner == 1) {
                        //Display winner 1
                    } else if (nWinner == 2) {
                        //Display winner 2
                    }
                }
            } while (nWinner == -1);
            exit = CheckPlayAgain();
        } while (!exit);
    }
	
	// Touch Controls
	void Update () {
        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.
        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0) {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began) {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0) {
                //Set touchEnd to equal the position of this touch
                Vector2 touchEnd = myTouch.position;

                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;

                //Calculate the difference between the beginning and end of the touch on the y axis.
                float y = touchEnd.y - touchOrigin.y;

                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                touchOrigin.x = -1;

                //Check if the difference along the x axis is greater than the difference along the y axis.
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                    horizontal = x > 0 ? 1 : -1;
                else
                    //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                    vertical = y > 0 ? 1 : -1;
            }
        }
    }



    /*_________________________________________________________________________________________________________*/



    public void Setup() {
        UnitScript Unit;
        UnitScript[] UnitsTemp = new UnitScript[5];
        HexScript Hex;
        //Players place the units
        for (int i = 0; i < (nPlayers*3-1); i++) {
            for (int j = 1; j <= nPlayers; j++) {
                Unit = new UnitScript(0);
                if (j == 1) {
                    Hex = Player1.PlanMovement(Unit);
                    Player1.ApplyMovement(Unit, Hex);
                    UnitsTemp[i] = Unit;
                } else if (j == 2) {
                    Hex = Player2.PlanMovement(Unit);
                    Player2.ApplyMovement(Unit, Hex);
                    UnitsTemp[i] = Unit;
                }
                i++;
            }
        }
        //Players claim the placed units
        for (int i = 0; i < 5; i++) {
            for (int j = 1; j <= nPlayers; j++) {
                if(j == 1) {
                    UnitsTemp[i].player = 1;
                }else if(j == 2) {
                    UnitsTemp[i].player = 2;
                }
            }
        }
        //Last player place and claim the last unit
        Unit = new UnitScript(0);
        Hex = Player2.PlanMovement(Unit);
        Player1.ApplyMovement(Unit, Hex);
        UnitsTemp[nPlayers*3].player = 2;
    }
        
    //Shows a message saying which player is playing this turn
    public void NextPlayer(int nPlayer) {
        
    }

    public void DrawPhase(int nPlayer) {
        if (nPlayer == 1)
            Player1.DrawPhase();
        else if (nPlayer == 2)
            Player2.DrawPhase();
    }

    public void PlanningPhase(int nPlayer) {
        if (nPlayer == 1)
            Player1.DrawPhase();
        else if (nPlayer == 2)
            Player2.DrawPhase();
    }

    public void ExecutePhase(int nPlayer) {
        if (nPlayer == 1)
            Player1.DrawPhase();
        else if (nPlayer == 2)
            Player2.DrawPhase();
    }

    public int CheckWinCondition() {
        int win = 0;
        int unitsleft1 = 0;
        int unitsleft2 = 0;
        for (int i = 0; i < Player1.Units.Length; i++)
            if (Player1.Units[i] != null)
                unitsleft1++;
        for (int i = 0; i < Player2.Units.Length; i++)
            if (Player1.Units[i] != null)
                unitsleft2++;

        if (unitsleft1 == 0 && unitsleft1 == unitsleft2)
            win = -1;   //tie
        else if (unitsleft1 == 0)
            win = 2;
        else if (unitsleft2 == 0)
            win = 1;
        else
            win = 0;

        return win;
    }

    //Ask players if they want to play another game
    public bool CheckPlayAgain() {
        return false;
    }

    IEnumerator Timer() {
        do {
            yield return new WaitForSeconds(1);
            tTimer++;
        } while (true);
    }
}
