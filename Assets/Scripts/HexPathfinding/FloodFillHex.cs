using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexMap{
	public class FloodFillHex {
		Queue<int> frontier;
		List <int> visited;
		//int start;

		public FloodFillHex(){

		}

		public Vector3[] FloodFill(Vector3 start, int radius){
			
			int start_id = HexGrid.instance.GetCellId(start);
			int[] ids = FloodFill(start_id, radius);
			Vector3[] result = new Vector3[ids.Length];
			//string res_out = "filled hexes :";
			for (int i = 0; i < result.Length; i++) {
				result[i] = HexGrid.instance.positions[ids[i]];
				//res_out += ids[i]+", ";
			}
			//Debug.Log(res_out);
			return result;
		}

		int[] FloodFill(int start, int radius){
			//SetStart(start);
			//List<int> result = new List<int>();
			frontier = new Queue<int>();
			visited = new List<int>();
			frontier.Enqueue(start);
			for (int i = 0; i < radius; i++) {
				AdvanceFrontier();
			}
			visited.AddRange(frontier.ToArray());
			//result.AddRange(visited.ToArray());
			return visited.ToArray();//result.ToArray();
		}

//		public void SetStart(int start_id){
//			start = start_id;
//		}


		//This is part of Flood Fill
		public void AdvanceFrontier(){
			if (frontier.Count != 0){
				int cycles = frontier.Count;
				//For each cell in current frontier
				for (int i = 0; i < cycles; i++) {
					//check each neighbor
					SearchLoop();
				}
			}
			else{
				Debug.Log("Search End");
			}
		}

		public void SearchLoop(){
			//Remove tile from Frontier
			//frontier.Remove(cell_id);
			int cell_id = frontier.Dequeue();
			visited.Add(cell_id);
			//Debug.Log("Visiting "+cell_id);

			for (int i = 0; i < 6; i++) {
				//Debug.Log(i + " Looking at neighbr " + dist_ids[i]);
				int neighbor_id = HexGrid.instance.neighbor_ids[cell_id, i];
				if (neighbor_id != -1){
					//If the visited tile list doesn't contain this neighbor
					if (!visited.Contains(neighbor_id) && !frontier.Contains(neighbor_id)){
						//Add it to frontier
						//Debug.Log("Visited does not contain "+ neighbor_id+" adding to queue");
						frontier.Enqueue(neighbor_id);

					}
				}
			}
		}
	}
}