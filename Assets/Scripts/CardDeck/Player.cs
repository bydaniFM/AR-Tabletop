using UnityEngine;
//using System.Collections.Generic;
using AK;
using ARTCards;

public class Player : MonoBehaviour {
	public CardDeck deck;
	public PlayingCard[] hand;
	public PlayingCard activeCard;
	public Unit[] units;
	// Use this for initialization
	void Start () {
		//ExpressionSolver solver = new ExpressionSolver();
		//solver.SetGlobalVariable("Strength", strength);
		//string expr = "sqrt(Strength * 2)";
		//float result = (float) solver.EvaluateExpression(expr);
		//Debug.Log("Damage is "+expr+" = "+result);
		PlayingCard[] testdeck = RulesLoader.GetPregenCards().ToArray();
		deck = new CardDeck(testdeck, RulesLoader.deckSize);
		hand = new PlayingCard[RulesLoader.handSize];

		units = new Unit[RulesLoader.unitNumber];
		for (int i = 0; i < units.Length; i++) {
			units[i] = new Unit();
			units[i].name = "Unit "+i;
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetMouseButtonDown(0)){
			PlayingCard card = deck.Draw();
			PlayCard(units[0], card);
		}*/
	}


	void PlayCard(Unit target, PlayingCard card){
		Attribute[] arr = new Attribute[target.attrs.Count];
		target.attrs.Values.CopyTo(arr, 0);
		if (card.isNotOverflowing(arr)){
			target.Buff(card.attributes);
		}
		//else{
			
		//}
	}
}
