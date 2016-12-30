﻿using UnityEngine;
using System.Collections.Generic;

namespace ARTCards
{
	public class Unit {
		public string name;
		public Dictionary<string, Attribute> attrs;
		public Dictionary<string, SecondaryAttribute> stats;
		public int damage;

		public Unit(){
			name = "Nameless Unit";
			attrs = RulesLoader.GetAttributesDict();
			stats = RulesLoader.GetStatsDict();

			//test
			damage = Damage();
		}

		public void Engage(Unit[] targets){
			int dmg = Damage()/targets.Length;
			for (int i = 0; i < targets.Length; i++) {
				targets[i].stats["hp"] -= dmg;
			}
		}

		int Damage(){
			AK.ExpressionSolver solver = new AK.ExpressionSolver();
			foreach (var x in stats) {
				solver.SetGlobalVariable(x.Key, x.Value);
			}
			int dmg = (int) solver.EvaluateExpression(RulesLoader.damageFormula);
			Debug.Log ("Damage is "+dmg);
			solver.ClearGlobalVariables();
			return dmg;
		}


		public void Die(){

		}

		public void Buff(int[] bonus){
			Debug.Assert( bonus.Length == attrs.Count, "Card or unit attributes are not in correct format!");
			int i = 0;
			foreach(Attribute x in attrs.Values){
				x.Value += bonus[i];
				i++;
			}
			foreach(SecondaryAttribute x in stats.Values){
				x.Recalculate(attrs[x.attr]);
			}

			damage = Damage();
		}
	}
}