using UnityEngine;
using System.Collections;
using AK;
namespace ARTCards
{
	public class SecondaryAttribute {
			public readonly string name;
			public readonly string attr;

			private int maxValue;
			private int curValue;

			private int minAttr;//needed to get right enumeration of calc table
			private string[] calc;

			public SecondaryAttribute(string n, string a, string[] c)
			: this(n, a, c, 0, 0) {
				maxValue = Calculate(7);
				curValue = maxValue;
			}

			public SecondaryAttribute (string n, string a, string[] c, int mv, int cv){
				name = n;
				attr = a;
				calc = c;
				maxValue = mv;
				curValue = cv;
				minAttr = RulesLoader.GetMinAttribute(attr);
				//Debug.Log("Min Attr of "+name+" is "+ minAttr);
			}

			public void Recalculate(int attr){
				int newMax = Calculate(attr);
				if (maxValue != 0){
				float tempvalue = ((float)curValue / (float)maxValue) * newMax;
					curValue = (int)tempvalue;
				}
				else {
					curValue = newMax;
				}
				maxValue = newMax;
				if (name == "HP"){
					Debug.Log("New HP is "+curValue+"/"+maxValue);
				}
			}
			int Calculate(int attr){
				ExpressionSolver solver = new ExpressionSolver();
				return (int)solver.EvaluateExpression(calc[attr-minAttr]);
			}

			public int Value{
				get{ return curValue; }
				set{
					curValue = value; 
					if (curValue > maxValue){
						curValue = maxValue; 
					}
				}
			}

			public static SecondaryAttribute operator - (SecondaryAttribute left, int right) {
				int newValue = left.curValue - right;

				return new SecondaryAttribute(left.name, left.attr, left.calc, left.maxValue, newValue);
			}
			public static SecondaryAttribute operator + (SecondaryAttribute left, int right) {
				int newValue = left.curValue + right;
				if (newValue > left.maxValue){
					newValue = left.maxValue;
				}
				return new SecondaryAttribute(left.name, left.attr, left.calc, left.maxValue, newValue);
			}
			public static implicit operator int(SecondaryAttribute a){
				return a.curValue;
			}
			public static implicit operator double(SecondaryAttribute a){
				return (double) a.curValue;
			}
	}
}