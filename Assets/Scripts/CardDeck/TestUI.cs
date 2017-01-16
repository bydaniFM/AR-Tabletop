using UnityEngine;
using System.Collections.Generic;
using ARTCards;
public class TestUI : MonoBehaviour {

	Player[] players;
	Unit cur_unit;
	bool holdingCard;

	// Use this for initialization
	void Start () {
		players = new Player[2];
		players[0] = new Player();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}

	void OnGUI(){
		for (int i = 0; i < players.Length; i++) {
			if (players[i] != null){

				//units
				for (int j = 0; j < players[i].units.Length; j++) {
					string buttonLabel = players[i].units[j].name + ButtonLabel(players[i].units[j].attrs);
					if (!(holdingCard && players[i].activeCard == null) && players[i].units[j].stats["HP"].Value > 0){
						if(GUI.Button(new Rect(10+65*j,10+170*i, 60, 80), buttonLabel)){
							if (cur_unit != players[i].units[j]){
								cur_unit = players[i].units[j];
							}
							else if (holdingCard){
								Attribute[] arr = new Attribute[players[i].units[j].attrs.Count];
								players[i].units[j].attrs.Values.CopyTo(arr, 0);
								if (players[i].activeCard.isNotOverflowing(arr)){
									players[i].units[j].Buff(players[i].activeCard.attributes);
									players[i].deck.Bury(players[i].activeCard);
									players[i].activeCard = null;
									holdingCard = false;
								}
							
							}
						}
					}
					else{
						GUI.Box(new Rect(10+65*j,10+170*i, 60, 80), buttonLabel);
					}
				}
				//cards
				for (int j = 0; j < 3; j++) {
					string buttonLabel = string.Empty;
					if (players[i].hand[j] != null){
						buttonLabel += ButtonLabel(players[i].hand[j].attributes);
					}
					if (!(holdingCard && players[i].activeCard == null)){
						if(GUI.Button(new Rect(10+65*j,90+170*i, 60, 60), buttonLabel)){
							ReplaceCards(ref players[i].hand[j], ref players[i].activeCard);
						}
					}
					else{
						GUI.Box(new Rect(10+65*j,90+170*i, 60, 60), buttonLabel);
					}
					
				}

				//Deck
				if (!holdingCard){
					if(GUI.Button(new Rect(300, 10+170*i, 40,40), "draw\n"+players[i].deck.deckSize)){
						PlayingCard card = players[i].deck.Draw();
						if (card != null){
							players[i].activeCard = card;
							holdingCard = true;
						}
					}
				}
				else{
					GUI.Box(new Rect(300, 10+170*i, 40,40), "draw");
				}
				//Discard
				if (holdingCard && players[i].activeCard != null){
					if(GUI.Button(new Rect(300, 50+170*i, 40,40), "drop")){
						players[i].deck.Bury(players[i].activeCard);
						players[i].activeCard = null;
						holdingCard = false;
					}
				}
				else{
					GUI.Box(new Rect(300, 50+170*i, 40,40), "drop\n"+players[i].deck.graveSize);
				}

				//holding card
				if (holdingCard && players[i].activeCard != null){
				
					if (GUI.Button(new Rect(240, 50+170*i, 30,20), "st-")){
						players[i].activeCard.attributes[0]--;
					}

					if (GUI.Button(new Rect(240, 70+170*i, 30,20), "ag-")){
						players[i].activeCard.attributes[1]--;
					}				

					if (GUI.Button(new Rect(240, 90+170*i, 30,20), "rn-")){
						players[i].activeCard.attributes[2]--;
					}
					if (GUI.Button(new Rect(270, 50+170*i, 30,20), "st+")){
						players[i].activeCard.attributes[0]++;
					}

					if (GUI.Button(new Rect(270, 70+170*i, 30,20), "ag+")){
						players[i].activeCard.attributes[1]++;
					}				

					if (GUI.Button(new Rect(270, 90+170*i, 30,20), "rn+")){
						players[i].activeCard.attributes[2]++;
					}

					string buttonLabel = ButtonLabel(players[i].activeCard.attributes);
					GUI.Box(new Rect(Input.mousePosition.x, Screen.height-Input.mousePosition.y, 60, 60), buttonLabel);


				}
			}
		}
		//Unit params
		if (cur_unit != null){
			GUI.Label(new Rect(Screen.width-200, 10, 200, 40), cur_unit.name);
			int i = 0;
			foreach(KeyValuePair<string, Attribute> x in cur_unit.attrs){
			//for (int i = 0; i < cur_unit.attrs.Count; i++) {
				GUI.Label(new Rect(Screen.width-200, 10+20*(1+i), 200, 40), x.Key);
				GUI.Label(new Rect(Screen.width-140, 10+20*(1+i), 200, 40), ""+(int)x.Value);
			//}
				i++;
			}
			GUI.Label(new Rect(Screen.width-200, 10+20*(1+cur_unit.attrs.Count), 200, 40), "dmg: "+cur_unit.damage);
			i = 0;
			foreach(KeyValuePair<string, SecondaryAttribute> x in cur_unit.stats){
			//for (int i = 0; i < cur_unit.attrs.Count; i++) {
				GUI.Label(new Rect(Screen.width-120, 10+20*(1+i), 200, 40), x.Key);
				GUI.Label(new Rect(Screen.width-50, 10+20*(1+i), 200, 40), ""+x.Value.Value);
				if (x.Key == "HP"){
					if (GUI.Button(new Rect(Screen.width-30, 5+20*(1+i), 15, 15), "-")){
						cur_unit.stats["HP"].Value -= 1;
					}
					else if (GUI.Button(new Rect(Screen.width-15, 5+20*(1+i), 15, 15), "+")){
						cur_unit.stats["HP"].Value += 1;
					}
					else if (GUI.Button(new Rect(Screen.width-30, 20+20*(1+i), 15, 15), "-")){
						cur_unit.stats["HP"].Value -= 10;

					}
					else if (GUI.Button(new Rect(Screen.width-15, 20+20*(1+i), 15, 15), "+")){
						cur_unit.stats["HP"].Value += 10;
					}
					if(cur_unit.stats["HP"].Value <=0){
						cur_unit = null;
					}
				}

			//}
				i++;
			}
		}

		/*if (GUI.Button(new Rect (Screen.height-50, Screen.width-30, 40, 20), "Restart")){
			Application.LoadLevel(0);
		}*/
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

//	if (players[i].hand[j] == null && players[i].activeCard != null){
//						players[i].hand[j] = players[i].activeCard;
//						players[i].activeCard = null;
//						holdingCard = false;
//					}
//					else if (players[i].hand[j] != null && players[i].activeCard == null){
//						players[i].activeCard = players[i].hand[j];
//						players[i].hand[j] = null;
//						holdingCard = true;
//					}
//					else if (players[i].hand[j] != null && players[i].activeCard != null){
//						PlayingCard temp = players[i].activeCard;
//						players[i].activeCard = players[i].hand[j];
//						players[i].hand[j] = temp;
//						holdingCard = true;
//					}
}
