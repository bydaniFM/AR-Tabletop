using UnityEngine;
using System;
using System.Collections.Generic;



namespace ARTCards
{	
	public enum EffectTypes	{
		Inject,
		Boost,
		Teleport,
		Shutdown
	}
	public class PlayingCard {
		public EffectTypes type;
		public readonly int[] attributes = new int[0];
		private Action cardAct;
		public readonly string id;

		public PlayingCard(){
			
		}
		public PlayingCard(int[] attrs, string id){
			attributes = new int[attrs.Length];
			for (int i = 0; i < attrs.Length; i++) {
				attributes[i] = attrs[i];
			}
			this.id = id;
		}

		/// <summary>
		/// Returns a card with zero sum of attributes.
		/// </summary>
		/// <param name="argsNo">Number of attributes</param>
		/// <param name="maxAttr">Max attribute value</param>
		public static PlayingCard GetBalancedCard(int argsNo, int maxAttr){
			int[] args = new int[argsNo];
			args[0] = UnityEngine.Random.Range(-maxAttr, maxAttr);
			int sumSoFar = args[0];
			string debugStr = String.Empty + args[0];
			//Debug.Log(args[0]);
			for (int i = 1; i < argsNo-1; i++) {
				int range = maxAttr-Mathf.Abs(sumSoFar);
				args[i] = UnityEngine.Random.Range(-range, range);
				sumSoFar += args[i];
				debugStr += ";" + args[i];
				//Debug.Log(args[i]);
			}
			args[argsNo-1] = 0-sumSoFar;
			debugStr += ";" + args[argsNo-1];
			//Debug.Log(args[argsNo-1]);
			//int val1 = UnityEngine.Random.Range(-maxAttr, maxAttr);
			//int range2 = maxAttr-Mathf.Abs(val1);
			//int val2 = UnityEngine.Random.Range(-range2, range2);
			string myid = "balanced"+args[0]+args[1]+args[2];
			Debug.Log("Card "+debugStr);
			return new PlayingCard(args, myid);
				//cards.Push(card);
			
		}

		public override string ToString(){
			//string str = String.Format("Card {0} {1} {2} {3}", attributes);
			string form = String.Empty+"*";
			for (int i = 0; i < attributes.Length; i++) {
				form += " "+attributes[i];
			}
			//Debug.Log (attributes.Length);
			return form;
		}

		public int[] ScaleDown(int margin){
			int[] result = new int[attributes.Length];
			int max = 0;
			for (int i = 0; i < attributes.Length; i++) {
				if (attributes[i] > max){
					max = attributes[i];
				}
			}
			double scaleFactor = (max-margin)/max;
			for (int i = 0; i < attributes.Length; i++) {
				result[i] = (int)(attributes[i] * scaleFactor);
			}

			return result;

		}

		public bool isNotOverflowing(Attribute[] attrs){
			for (int i = 0; i < attrs.Length; i++) {
				if (attributes[i] + attrs[i] < attrs[i].minValue || attributes[i] + attrs[i] > attrs[i].maxValue){
					return false;
				}
			}
			return true;
		}
	}
}