using System.Collections.Generic;
using UnityEngine;

namespace HexMap{
	public class HexMark {
		Dictionary<string, GameObject> cellGroups; // A list of all generated mesh "layers"
		Transform commonParent;
		Material hexMat;

		public static HexMark instance;

		public HexMark(){
			if (instance == null){
				instance = this;
			}
			else if (instance != this){
				Debug.LogError("HexMark object already exists!");
			}
			hexMat = Resources.Load<Material>("Hexmat");
			cellGroups = new Dictionary<string, GameObject>();
			FindRoot();

		}

		public void MarkGrid(string name, HexCell[] cells){
			Vector3[] coords = new Vector3[cells.Length];
			for (int i = 0; i < coords.Length; i++) {
				int id = HexGrid.instance.GetCellId(cells[i]);
				coords[i] = HexGrid.instance.positions[id];
			}
			MarkGrid(name, coords, hexMat, Color.white);
		}
		public void MarkGrid(string name, Vector3[] coords){
			MarkGrid(name, coords, hexMat, Color.white);
		}

		public void MarkGrid(string name, Vector3[] coords, Color color){
			MarkGrid(name, coords, hexMat, color);
		}

		public void MarkGrid(string name, Vector3[] coords, Material mat, Color color){
			Unmark(name);
			cellGroups.Add(name, new GameObject(name));
		
			MeshFilter mf = cellGroups[name].AddComponent <MeshFilter>();
			MeshRenderer mr = cellGroups[name].AddComponent <MeshRenderer>();

			mf.mesh = MeshGen.CreateGridMesh(MeshGen.Hex(HexLayout.instance.wide_width/2), coords);
			mf.mesh.name = "GridMesh"+name;
			//RecalculateMeshHeights(mf.mesh);
			mf.mesh.RecalculateBounds();
			mf.mesh.RecalculateNormals();
			mr.material = mat;//tile.GetComponent<MeshRenderer>().sharedMaterial;
			mr.material.color = color;	

			FindRoot();
			cellGroups[name].transform.position = commonParent.position + Vector3.up*0.01f*cellGroups.Count;
			cellGroups[name].transform.rotation = commonParent.rotation;
			cellGroups[name].transform.parent = commonParent;
		}

		public void Unmark(string name){
			if (! cellGroups.ContainsKey(name)){ 
				//Debug.LogError("There is no grid "+name+" in the GridMarker");
				return;
			}
			GameObject go = cellGroups[name];
			cellGroups.Remove(name);
			if (go != null){
				LeanTween.color(go, Color.clear, 0.5f).setOnComplete(() => MonoBehaviour.Destroy(go));
			}

		}
		public Transform GetRoot(){
			FindRoot();
			return commonParent;
		}

		void FindRoot(){
			if (commonParent == null){
				commonParent = GameObject.Find("Field").transform;
			}
		}

	}
}