using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace ARTCards
{
	public class Attribute{
		
		private string name;
		private int curValue;
		public readonly int minValue;
		public readonly int maxValue;
		private int bonus;

		public Attribute(){
			
		}
		public Attribute(string n, int v){
			name = n;
			curValue = v-1;
			//minValue = Rules.attributes[n].minValue;
			minValue = 1;
			//maxValue = Rules.attributes[n].maxValue;
			maxValue = 14;
		}

		public Attribute(string n, int v, int min, int max){
			name = n;
			curValue = v-min;
			minValue = min;
			maxValue = max;
		}

		public int Value{
			get{return curValue + minValue + bonus;}
			set{
				if (curValue+minValue <= maxValue && curValue >= 0){
					curValue = value-minValue;
				}
				else if (curValue+minValue > maxValue){
					//curValue = maxValue;
					Debug.LogWarning("Stat "+name+" can't go over "+maxValue);
				}
				else if (curValue<0){
					Debug.LogWarning("Stat "+name+" can't go under "+minValue);
				}
			}
		}

		public static Attribute operator + (Attribute left, int right) {
			int newValue = left.curValue + right;
			if (newValue + left.minValue > left.maxValue){
				newValue = left.maxValue - left.minValue;
			}
			return new Attribute(left.name, newValue, left.minValue, left.maxValue);
		}

		public static implicit operator int(Attribute a){
			return a.curValue + a.minValue;
		}
		public void SetBonus(int n){
			bonus = n;
		}
	}

}