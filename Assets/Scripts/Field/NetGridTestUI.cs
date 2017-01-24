using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPNet;
using ARTCards;

public class NetGridTestUI : MonoBehaviour {

	PlayerControl pc;
	EnemyControl ec;
	ConnectionManager cm;
	bool holdingCard;
	Unit activeUnit;
	bool unitPanel;
	bool pulled;
	Texture2D hpt;
	// Use this for initialization
	void Start () {
		pc = FindObjectOfType<PlayerControl>();	
		ec = FindObjectOfType<EnemyControl>();	
		cm = FindObjectOfType<ConnectionManager>();	
		hpt = new Texture2D(2,2);
	}
	
	// Update is called once per frame
	void Update () {
		if(pc.curUnit != -1 && activeUnit != pc.player.units[pc.curUnit]){
			activeUnit = pc.player.units[pc.curUnit];
		}
	}

	void OnGUI(){
		if (RoundController.curState == GameState.WaitingForOther){
			GUI.Box(new Rect(Screen.width/2-100, 0, 200, 20), "Please Wait");
		}
		else if (RoundController.curState == GameState.Deployment){
			if (!pc.unitsDeployed){
				GUI.Box(new Rect(Screen.width/2-100, 0, 200, 20), "Deploy Units");
			}
			else{
				GUI.Box(new Rect(Screen.width/2-100, 0, 200, 20), "Enemy Deploying");
			}
		}
		else if (RoundController.curState == GameState.Cards){
			if (!pulled){
				GUI.Box(new Rect(Screen.width/2-100, 0, 200, 20), "Pull a card!");
			}
			else{
				GUI.Box(new Rect(Screen.width/2-100, 0, 200, 20), "Play a card!");
			}
			DeckButton();
			GraveButton();
			UnitsBlock();
			if(pulled){
				for (int i = 0; i < 3; i++) {
					HandSlot(i);
				}
				if (!holdingCard){
					if(GUI.Button(new Rect(255, Screen.height-50, 60,40), "Pass")){
						pulled = false;
						RoundController.SwitchState(GameState.Planning);
					}
				}
			}
			if (holdingCard && pc.player.activeCard != null){
				string buttonLabel = ButtonLabel(pc.player.activeCard.attributes);
				GUI.Box(new Rect(Input.mousePosition.x, Screen.height-Input.mousePosition.y, 60, 60), buttonLabel);
			}
		}

		else if (RoundController.curState == GameState.Planning){
			GUI.Box(new Rect(Screen.width/2-100, 0, 200, 20), "Plan Movement");
			//unit buttons
			UnitsBlock();

		}
		else if (RoundController.curState == GameState.Ready){
			GUI.Box(new Rect(0,0,Screen.width, Screen.height), "Waiting for Enemy");
		}
		else if (RoundController.curState == GameState.Movement){
			GUI.Box(new Rect(Screen.width/2-100, 0, 200, 20), "Moving Units");
		}
		else if (RoundController.curState == GameState.Engagement){
			GUI.Box(new Rect(Screen.width/2-100, 0, 200, 20), "Resolving Combat!");
		}
		else if (RoundController.curState == GameState.Disconnecting){
			GUI.Box(new Rect(0,0,Screen.width, Screen.height), "Enemy Disconnected!\nExit in "+(int)cm.timer);
		}

		if (ec.ready){
			GUI.Box(new Rect (Screen.width-210, 10, 200, 20), "Enemy Ready");
		}
	}

	void UnitsBlock(){
		for (int i = 0; i < pc.player.units.Length; i++) {
			UnitButton(i);
		}

		if (activeUnit != null){
			if (!unitPanel) {
				UnitStats(200);	
			}
			else{
				UnitStats(0);
			}
		}
	}
	void DeckButton(){
		if (!pulled){
			if(GUI.Button(new Rect(300, Screen.height-90, 40,40), "draw\n"+pc.player.deck.deckSize)){
				PlayingCard card = pc.player.deck.Draw();
				if (card != null){
					pc.player.activeCard = card;
					holdingCard = true;
					pulled = true;
				}
			}
		}
		else{
			GUI.Box(new Rect(300, Screen.height-90, 40,40), "draw");
		}
	}

	void GraveButton(){
		if (holdingCard && pc.player.activeCard != null){
			if(GUI.Button(new Rect(300, Screen.height-50, 40,40), "drop")){
				pc.player.deck.Bury(pc.player.activeCard);
				pc.player.activeCard = null;
				holdingCard = false;
				pulled = false;
				RoundController.SwitchState(GameState.Planning);
			}
		}
		else{
			GUI.Box(new Rect(300, Screen.height-50, 40,40), "drop\n"+pc.player.deck.graveSize);
		}
	}

	void HandSlot(int id){
		string buttonLabel = string.Empty;
		if (pc.player.hand[id] != null){
			buttonLabel += ButtonLabel(pc.player.hand[id].attributes);
		}
		if (!(holdingCard && pc.player.activeCard == null)){
			if(GUI.Button(new Rect(10+65*id, Screen.height-60, 60, 60), buttonLabel)){
				ReplaceCards(ref pc.player.hand[id], ref pc.player.activeCard);
			}
		}
		else{
			GUI.Box(new Rect(10+65*id, Screen.height-60, 60, 60), buttonLabel);
		}
	}

	void ReplaceCards(ref PlayingCard c1, ref PlayingCard c2){
		if (c1 == null && c2 != null){
			c1 = c2;
			c2 = null;
			holdingCard = false;
		}
		else if (c1 != null && c2 == null){
			c2 = c1;
			c1 = null;
			holdingCard = true;
		}
		else if (c1 != null && c2 != null){
			PlayingCard temp = c2;
			c2 = c1;
			c1 = temp;
			holdingCard = true;
		}
	}

	void UnitButton(int id){
		string buttonLabel = pc.player.units[id].name + ButtonLabel(pc.player.units[id].attrs);
		if (!(holdingCard && pc.player.activeCard == null) && pc.player.units[id].stats["HP"].Value > 0){
			if (pc.curUnit == id){
				GUI.Box(new Rect(10+65*id,10+170*0, 60, 80),"");
			}
			if(GUI.Button(new Rect(10+65*id,10+170*0, 60, 80), buttonLabel)){
				if (pc.curUnit != id){
					pc.SelectUnit(id);
					activeUnit = pc.player.units[id];
				}
				else if (holdingCard){
					Attribute[] arr = new Attribute[pc.player.units[id].attrs.Count];
					pc.player.units[id].attrs.Values.CopyTo(arr, 0);
					if (pc.player.activeCard.isNotOverflowing(arr)){
						pc.player.units[id].Buff(pc.player.activeCard.attributes);
						pc.player.deck.Bury(pc.player.activeCard);
						pc.player.activeCard = null;
						holdingCard = false;
						pulled = false;
						pc.UpdateControllers();
						RoundController.SwitchState(GameState.Planning);
						NetworkController.instance.photonView.RPC("ForceEnemyInfoUpdate", PhotonTargets.Others, pc.player.ToBin());
					}
				
				}
			}
		}
		else{
			GUI.Box(new Rect(10+65*id,10+170*0, 60, 80), buttonLabel);
		}
		float hpPrc = (float)pc.player.units[id].stats["HP"].Value/(float)pc.player.units[id].stats["HP"].MaxValue;
		if (hpPrc < 0){
			hpPrc = 0;
		}
		GUI.Box(new Rect(10+67*id,12+80+170*0, 56, 5), "");
		GUI.color = Color.red;
		GUI.DrawTexture(new Rect(10+67*id,12+80+170*0, 56*hpPrc, 5), hpt);
		GUI.color = Color.white;
	}
	void UnitButtonOld(int id){
		if (pc.curUnit != id){
			if(GUI.Button(new Rect(10,10+25*id, 20, 20), "")){
				pc.SelectUnit(id);
			}
		}
		else{
			GUI.Box(new Rect(10,10+25*id, 20, 20), "");
		}
		
	}

	void UnitStats(int xoffset){
		GUI.Box(new Rect(Screen.width-210 +xoffset, 0, 210, 400), "");
		GUI.Label(new Rect(Screen.width-200 +xoffset, 10, 200, 40), activeUnit.name);
		int i = 0;
		foreach(KeyValuePair<string, Attribute> kvp in activeUnit.attrs){
		//for (int i = 0; i < curUnit.attrs.Count; i++) {
			GUI.Label(new Rect(Screen.width-200+xoffset, 10+20*(1+i), 200, 40), kvp.Key);
			GUI.Label(new Rect(Screen.width-140+xoffset, 10+20*(1+i), 200, 40), ""+(int)kvp.Value);
		//}
			i++;
		}
		GUI.Label(new Rect(Screen.width-200+xoffset, 10+20*(1+activeUnit.attrs.Count), 200, 40), "dmg: "+activeUnit.damage);
		i = 0;
		foreach(KeyValuePair<string, SecondaryAttribute> kvp in activeUnit.stats){
		//for (int i = 0; i < curUnit.attrs.Count; i++) {
			GUI.Label(new Rect(Screen.width-120+xoffset, 10+20*(1+i), 200, 40), kvp.Key);
			GUI.Label(new Rect(Screen.width-50+xoffset, 10+20*(1+i), 200, 40), ""+kvp.Value.Value);
		//}
			if (kvp.Key == "HP"){

			}
			i++;
		}

		if (GUI.Button(new Rect(Screen.width-230+xoffset, 20, 20, 20), "")){
			unitPanel = !unitPanel;
		}
	}

	void HPManualEdit(int id){
		
		if (GUI.Button(new Rect(Screen.width-30, 5+20*(1+id), 15, 15), "-")){
			activeUnit.stats["HP"].Value -= 1;

			NetworkController.instance.photonView.RPC("ForceEnemyInfoUpdate", PhotonTargets.Others, pc.player.ToBin());
		}
		else if (GUI.Button(new Rect(Screen.width-15, 5+20*(1+id), 15, 15), "+")){
			activeUnit.stats["HP"].Value += 1;

			NetworkController.instance.photonView.RPC("ForceEnemyInfoUpdate", PhotonTargets.Others, pc.player.ToBin());
		}
		else if (GUI.Button(new Rect(Screen.width-30, 20+20*(1+id), 15, 15), "-")){
			activeUnit.stats["HP"].Value -= 10;

			NetworkController.instance.photonView.RPC("ForceEnemyInfoUpdate", PhotonTargets.Others, pc.player.ToBin());
		}
		else if (GUI.Button(new Rect(Screen.width-15, 20+20*(1+id), 15, 15), "+")){
			activeUnit.stats["HP"].Value += 10;

			NetworkController.instance.photonView.RPC("ForceEnemyInfoUpdate", PhotonTargets.Others, pc.player.ToBin());
		}
		if(activeUnit.stats["HP"].Value <=0){
			activeUnit = null;
		}
			
	}

	string ButtonLabel(Dictionary<string, Attribute> dict){
		return '\n'+"STR:"+dict["Strength"].Value
				+'\n'+"AGI:"+dict["Agility"].Value
				+'\n'+"RNG:"+dict["Range"].Value;
	}
	string ButtonLabel(int[] arr){
		return "STR:"+arr[0]
				+'\n'+"AGI:"+arr[1]
				+'\n'+"RNG:"+arr[2];
	}

}
