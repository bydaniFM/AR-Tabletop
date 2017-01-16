using UnityEngine;
//using System.Collections.Generic;
using System.IO;
using AK;
using ARTCards;

public class Player {
	public CardDeck deck;
	public PlayingCard[] hand;
	public PlayingCard activeCard;
	public Unit[] units;
	// Use this for initialization
	public Player () {
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
	



	void PlayCard(Unit target, PlayingCard card){
		Attribute[] arr = new Attribute[target.attrs.Count];
		target.attrs.Values.CopyTo(arr, 0);
		if (card.isNotOverflowing(arr)){
			target.Buff(card.attributes);
		}
		//else{
			
		//}
	}

	public byte[] ToBin(){
		System.Collections.Generic.List<byte> lst = new System.Collections.Generic.List<byte>();
		for (int i = 0; i < units.Length; i++) {
			lst.AddRange(units[i].ToBin());
		}
		return lst.ToArray();

	}
	public void Load (byte[] bytes){
		using (MemoryStream memoryStream = new MemoryStream(bytes)){
			using (BinaryReader binaryReader = new BinaryReader(memoryStream)){
				for (int i = 0; i < units.Length; i++) {
					string[] keys = new string[units[i].attrs.Count];
					units[i].attrs.Keys.CopyTo(keys, 0);

					for (int j = 0; j < keys.Length; j++) {
						units[i].attrs[keys[j]].Value = binaryReader.ReadInt32();	
					}

					units[i].stats["HP"].Value = binaryReader.ReadInt32();
				}
			}
		}
	}
}
