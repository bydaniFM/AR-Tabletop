using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexMap{
	public class HexGrid {

		Dictionary<HexCell, int> celldict;
		//Hashtable<Hash128, HexCell> cellhash;
		public readonly HexCell[] all_cells;
		float[] moveCosts;
		//int[] cell_status;
		public readonly int[,] neighbor_ids; // stores 6 id indices for each cell
		public readonly Vector3[] positions;
		public static HexGrid instance;

		public HexGrid(Vector3[] points){
			positions = points;
			all_cells = Coords2Cells(points);
			celldict = IdDictionary();
			neighbor_ids = new int[all_cells.Length, 6];
			for (int i = 0; i < all_cells.Length; i++) {
				int[] cell_nghbrs = FindNeighbors(i); 
				for (int j = 0; j < cell_nghbrs.Length; j++) {
					neighbor_ids[i,j] = cell_nghbrs[j];
				}
			}
			if (instance == null){
				instance = this;
			}
			if (instance != this){
				Debug.LogError("HexGrid object already exists");
			}

		}

		private HexCell[] Coords2Cells(Vector3[] points){
			HexCell[] result = new HexCell[points.Length];
			for (int i = 0; i < points.Length; i++) {
				result[i] = GetCell(points[i]);
				//Debug.Log("Added "+points[i]+" "+result[i].q+":"+result[i].r);
			}
			return result;
		}
		private Dictionary<HexCell, int> IdDictionary(){
			Dictionary<HexCell, int> result = new Dictionary<HexCell, int>();
			for (int i = 0; i < all_cells.Length; i++) {
				if (!result.ContainsKey(all_cells[i])){
					result.Add(all_cells[i], i);
				}
				else {
					Debug.LogError(all_cells[i].q+":"+all_cells[i].r);
				}
			}
			return result;
		}

		public HexCell GetCell(Vector3 pos){
			//int new_x = Mathf.FloorToInt(pos.x);
			//int new_y = Mathf.FloorToInt(pos.z);
			//FractionalHex h = new FractionalHex(pos.x, pos.z, -pos.x-pos.z);
			HexCell result = HexLayout.instance.WorldToHex(pos);

			return result;
		}

		public int[] FindNeighbors(int id){
			int[] results = new int[6];
			for (int i = 0; i < results.Length; i++) {
				results[i] = -1;
				HexCell n_cell = all_cells[id].Neighbor(i);
				//results[i] = celldict[n_cell]; 
				if (celldict.ContainsKey(n_cell)){
					results[i] = celldict[n_cell];
				}
				//Debug.Log("Cell "+all_cells[id].q+":"+all_cells[id].r+" Neighbor "+i+"="+results[i]);

				//int cell_id = GetCellId(n_cell);
				//invalidates neighbors with too much Z diff
				/*if (cell_id != -1){
					float z_diff = Mathf.Abs(all_cells[id].h - all_cells[cell_id].h);
					if (z_diff <= 1){
						results[i] = cell_id;
					}
				}*/
			}
			return results;
		}



		/// Gets the cell identifier by iterating through all cells and choses one that is closer on alt axis.

		//Redundant
		public int GetCellId(HexCell cell){
			int result = -1;
	//		float best_z_diff = Mathf.Infinity;
	//		for (int i = 0; i < all_cells.Length; i++) {
	//
	//			if (all_cells[i] == cell){
	//				float z_diff = Mathf.Abs(cell.h - all_cells[i].h);
	//				if (z_diff < best_z_diff){
	//					//Debug.Log(z_diff);
	//					best_z_diff = z_diff;
	//					result = i;
	//				}
	//				//
	//			}
	//		}
			//Debug.Log(cell.x+":"+cell.y+" id is "+result);
			//Hack
			if (celldict.ContainsKey(cell)){
				result = celldict[cell];
			}
			return result;
		}

		public int GetCellId(Vector3 point){
			HexCell cell = GetCell(point);
			return GetCellId(cell);
		}

		public float GetCost(int id){
			/*if (cell_status[id] <=2 && cell_status[id] != -1){
				return moveCosts[cell_status[id]];
			}
			else {
				return Mathf.Infinity;
			}*/
			return 1;
		}
	}
}