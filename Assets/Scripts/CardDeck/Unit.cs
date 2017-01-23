using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace ARTCards
{
	public class Unit {
		public string name;
        public int player;
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
        ////Overload of the constructor to place the unit in the field
        //public Unit(RaycastHit hit, string name) {
        //    this.name = name;
        //    attrs = RulesLoader.GetAttributesDict();
        //    stats = RulesLoader.GetStatsDict();
        //    //Create a game object in the scene, children of "ImageTarget_Grid"
        //    //Put the 3D model
        //    //Place it in the position of the raycast
        //}

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

		public byte[] ToBin(){
			using (MemoryStream memoryStream = new MemoryStream()){
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream)){

					foreach (var item in attrs) {
						binaryWriter.Write(item.Value.Value);
					}
					binaryWriter.Write(stats["HP"].Value);

				}
				memoryStream.Close();
				byte[] result = memoryStream.ToArray();
				return result;
			}

		}
	}
}