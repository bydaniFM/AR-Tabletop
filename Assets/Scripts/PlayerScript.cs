using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public int nPlayer;
    public int nUnits;
    public CardScript card;
    public UnitScript[] Units;
    public UnitScript unitToMove;

    public PlayerScript() {
        for (int i = 0; i < nUnits; i++) {
            Units[i] = new UnitScript();
        }
    }

    //Scann card in front of the player
    //Ask if he wants to use it
    public void DrawPhase() {
        card = new CardScript();    //card is scanned
        UseCard(card);
    }

    //The player can scan one of his cards to use it, and select an unit and a hex to move it
    public void PlanningPhase() {
        card = new CardScript();    //card is scanned
        UseCard(card);
        unitToMove = new UnitScript();  //unit to move is selected
        do {} while (PlanMovement(unitToMove) != null) ;
    }

    //Apply the card and move to the selected unit, respectively
    public void ExecutePhase() {

    }

    public void UseCard(CardScript card) {

    }

    //Player selects the hex where to move the unit.
    //Check if position is valid
    public HexScript PlanMovement(UnitScript unit) {
        HexScript target = new HexScript();
        int newPosX = 0;
        int newPosY = 0;

        //Here the apply the pos of the selected hex to the variables

        if((newPosX-unit.posX)+(newPosY-unit.posY) < unit.movement) {
            target.posX = newPosX;
            target.posY = newPosY;
        } else {
            target = null;
        }

        return target;
    }

    public void ApplyMovement(UnitScript unit, HexScript hex) {

    }

}