using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
namespace HexMap{
	public class AStarHex{

		//private HexGrid HexGrid.instance;
		bool step;

		//List <int> frontier;
		SimplePriorityQueue <int> frontier;

		int[] came_from;
		float[] cost_so_far;


		int start;
		int destination;

		int searchcount;

		public AStarHex(){
			
		}

		public void StartPath(){
			StartPath(start);
		}

		void StartPath(int start_id){
			frontier = new SimplePriorityQueue<int>();
			frontier.Enqueue(start_id, 0);
			InitGridData();
			came_from[start] = -2;
			cost_so_far[start] = 0;
			searchcount = 0;
			Debug.Log("Starting search!");
		}

		void StartPath(Vector3 start_vec){
			HexCell start_cell = HexGrid.instance.GetCell(start_vec);
			int start_id = HexGrid.instance.GetCellId(start_cell);
			Debug.Log("Clickan cell #"+start_id+" "+start_cell.q+":"+start_cell.r+":"+start_cell.s+" @ "+start_vec);
			StartPath(start_id);
		}

		public void AdvanceFrontier(){
			if (frontier.Count != 0 && !frontier.Contains(destination)){
				int cycles = frontier.Count;
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
			int cell_id = frontier.Dequeue();
			int[] new_ids = OddSearch(cell_id);
			//Debug.Log("Enter Search Loop");

			for (int i = 0; i < 6; i++) {
				int neighbor_id = HexGrid.instance.neighbor_ids[cell_id, new_ids[i]];
				if (neighbor_id != -1){
					float new_cost = cost_so_far[cell_id] + HexGrid.instance.GetCost(neighbor_id);
					//Debug.Log("Neighbor "+new_ids[i]+" "+HexGrid.instance.all_cells[neighbor_id].q+" "+HexGrid.instance.all_cells[neighbor_id].r+" number "+neighbor_id + " Cost "+new_cost);
					//If the visited tile list doesn't contain this neighbor
					if (came_from[neighbor_id] == -1 || new_cost < cost_so_far[neighbor_id]){
						cost_so_far[neighbor_id] = new_cost;
						//Add it to frontier
						float priority = new_cost + ManhattanDist(neighbor_id, destination);
						//float priority = new_cost + Vector3.Distance(Vector3.zero, Vector3.zero);
						frontier.Enqueue(neighbor_id, priority);
						//Mark step 
						searchcount++;
						//HexGrid.instance.gui.SetText(neighbor_id, ""+priority);
						//also add it to visited

						came_from[neighbor_id] = cell_id;
						//Debug.Log("Adding "+ HexGrid.instance.all_cells[neighbor_id].q+":"+ HexGrid.instance.all_cells[neighbor_id].r );
					}
				}
			}
		}

		int ManhattanDist(int a, int b){
	//		int x_diff = Mathf.Abs(HexGrid.instance.all_cells[a].x - HexGrid.instance.all_cells[b].x);
	//		int y_diff = Mathf.Abs(HexGrid.instance.all_cells[a].y - HexGrid.instance.all_cells[b].y);
	//		return x_diff+y_diff;
			return HexCell.Distance(HexGrid.instance.all_cells[a], HexGrid.instance.all_cells[b]);
		}
		int[] OddSearch(int id){
			int[] result = new int[6];
			for (int i = 0; i < 6; i++) {
				if (id % 2 == 0){
					result[i] = i;
				}
				else {
					result[i] = 5-i;
				}
			}
			return result;
		}

		int[] DistPriority(int id){
			int[] ids = HexGrid.instance.FindNeighbors(id);
			int[] result = new int[4];
			float[] dists = new float[4];

			for (int i = 0; i < 4; i++) {
				//dists[i] = HexGrid.instance.Distance(ids[i], start);
				dists[i] = ManhattanDist(ids[i], start);
				result[i] = i;
			}

			for (int i = 0; i < 4; i++) {
				int min = -1;
				float mindist = Mathf.Infinity;
				for (int j = i; j < 4; j++) {
					if (dists[j] < mindist){
						mindist = dists[j];
						min = j;
					}
				}

				if (min != -1){
					float tempdist = dists[i];
					int temp = result[i];
					dists[i] = dists[min];
					result[i] = result[min];
					dists[min] = tempdist;
					result[min] = temp;
				}

			}

			return result;
		}

		public int[] GetFrontier(){
			return frontier.ToArray();
		}

		public int[] GetCameFrom(){
			return came_from;
		}
		public void SetDestination(int dest_id){
			destination = dest_id;
		}
		public void SetDestination(Vector3 dest_point){
			HexCell dest_cell = HexGrid.instance.GetCell(dest_point);
			int dest_id = HexGrid.instance.GetCellId(dest_cell);
			destination = dest_id;
		}
		public int GetDestination(){
			return destination;
		
		}
		public void SetStart(int start_id){
			start = start_id;
		}
		public void SetStart(Vector3 start_point){
			HexCell start_cell = HexGrid.instance.GetCell(start_point);
			int start_id = HexGrid.instance.GetCellId(start_cell);
			start = start_id;
		}
		public int GetStart(){
			return start;
		}

		public Vector3[] FindPath (Vector3 start, Vector3 end){
			SetStart(HexGrid.instance.GetCellId(start));
			SetDestination(HexGrid.instance.GetCellId(end));
			return FindPath();
		}
		public Vector3[] FindPath(HexCell start, HexCell end){
			SetStart(HexGrid.instance.GetCellId(start));
			SetDestination(HexGrid.instance.GetCellId(end));
			return FindPath();
		}
		public Vector3[] FindPath(){
			StartPath();
			int[] path_ind = FindPath(true);
			Vector3[] result = new Vector3[path_ind.Length];
			for (int i = 0; i < path_ind.Length; i++) {

				result[result.Length-i-1] = HexGrid.instance.positions[path_ind[i]];
			}
			return result;
		}
		public int[] FindPath(bool yes){
			//Debug.Log(frontier);
			int[] result = new int[0];
			for (int i = 0; i < HexGrid.instance.all_cells.Length; i++) {
				if (!frontier.Contains(destination)){
					SearchLoop();//frontier[0]);
				}
				else{
					result = RestorePath();
					break;
				}
			}
			return result;
		}

		public int[] RestorePath(){
			List<int> path = new List<int>();
			if (came_from[destination] != -1){
				//int current = destination;
				path.Add(destination);
				//int front_index = frontier.ToArray()[frontier.Count-1];
				int next = came_from[destination];
				for (int i = 0; i < HexGrid.instance.all_cells.Length; i++) {
					if (next != start){
						path.Add(next);
						//int vis_index = visited.IndexOf(next);
						next = came_from[next];
					}
					else {
						break;
					}
				}
			}
			return path.ToArray();
		}

		void InitGridData(){
			came_from = new int[HexGrid.instance.all_cells.Length];
			cost_so_far = new float[HexGrid.instance.all_cells.Length];
			for (int i = 0; i < came_from.Length; i++) {
				came_from[i] = -1;
				cost_so_far[i] = Mathf.Infinity;

			}
		}


	}
}