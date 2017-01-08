using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour {

    public int posX;
    public int posY;
    public int player;  //0 -> neutral

    //Stats
    public int movement;

    //Constructors. Lack stats inicialization
    public UnitScript() {
        this.player = 0;
        this.posX = -1;
        this.posY = -1;
    }
    public UnitScript(int player) {
        this.player = player;
        this.posX = -1;
        this.posY = -1;
    }
    public UnitScript(int player, int posX, int posY) {
        this.player = player;
        this.posX = posX;
        this.posY = posY;
    }
}
