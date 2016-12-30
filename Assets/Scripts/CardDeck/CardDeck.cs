using UnityEngine;
using System.Collections.Generic;

namespace ARTCards
{
	public class CardDeck {

		private Stack<PlayingCard> cards;
		private Stack<PlayingCard> grave;

		public CardDeck(int number) 
		: this (new PlayingCard[0], number){}

		public CardDeck(PlayingCard[] pregen, int number){
			cards = new Stack<PlayingCard>();
			grave = new Stack<PlayingCard>();
			for (int i = 0; i < pregen.Length; i++) {
				cards.Push(pregen[i]);
			}

			for (int i = 0; i < number-pregen.Length; i++) {
				cards.Push(PlayingCard.GetBalancedCard(3, 9));
			}
		}

		public PlayingCard Draw(){
			//PlayingCard card = cards.Peek();
			if (cards.Count == 0){
				Debug.Log("The deck is drawn out!");
				cards = Shuffle(grave);
				grave.Clear();
				return null;
			}

			Debug.Log("Drawn "+cards.Peek().ToString());
			return cards.Pop();
		}
		public void Bury(PlayingCard card){
			grave.Push(card);
			Debug.Log("Card "+card.ToString()+"put into grave");
		}

		public Stack<PlayingCard> Shuffle(Stack<PlayingCard> deck){
			PlayingCard[] tempDeck = deck.ToArray();
			Stack<PlayingCard> result = new Stack<PlayingCard>();
			for (int i = 0; i < tempDeck.Length; i++) {
				int newPlace = Random.Range(0, tempDeck.Length-1);
				PlayingCard temp = tempDeck[newPlace];
				tempDeck[newPlace] = tempDeck[i];
				tempDeck[i] = temp;
			}
			for (int i = 0; i < tempDeck.Length; i++) {
				result.Push(tempDeck[i]);
			}
			return result;
		}
		public int deckSize{
			get{return cards.Count;}
		}
		public int graveSize{
			get{return grave.Count;}
		}
	}
}
