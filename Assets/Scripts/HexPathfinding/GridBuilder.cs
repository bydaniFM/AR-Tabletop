using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexMap{
	public class GridBuilder : MonoBehaviour {

		public Material gridMaterial;
		public int gridWidth;
		public int gridHeight;
		public float gridElevation; // height of grid above surface
		//public Vector2 spacing = new Vector2(1, 1);
		public Vector3 offset;
		public float hexRad; //inner radius of hexes

		GameObject gridHolder;

		Vector3[] coords;

		HexLayout layout;
		void Awake(){
			layout = new HexLayout(hexRad, transform.position, offset);
			Vector3[] ngrd = NewGridCoords(gridWidth, gridHeight, offset);

			int layerMask = 1 << LayerMask.NameToLayer("NavMesh");
			coords = HitCoords(ngrd, layerMask);
	//		foreach (var item in coords) {
	//			GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	//			go.transform.localScale = Vector3.one*0.3f;
	//			go.transform.position = item;
	//			go.name = "Hex mark";
	//		}
			HexGrid.instance = new HexGrid(coords);
		}
		// Use this for initialization
		void Start () {

			
			CreateGridObj();

//			GridUI gui = GameObject.Find("Canvas").GetComponent<GridUI>();
//
//
//			foreach (Vector3 vec in coords) {
//				
//				//Vector2 uv = vec.x * X_QR + vec.y * Y_QR;
//				//int u = (int) uv.x;
//				//int v = (int) uv.y;
//				HexCell hex = layout.WorldToHex(vec);
//				gui.FeedbackObj(vec, hex.q+":"+hex.r);
//			}
			Destroy(gameObject);
		}

		void Update(){

            //		Debug.DrawLine(Vector3.up+Vector3.back*hexRad, Vector3.up+Vector3.back*hexRad + Vector3.left * narrow_width, Color.blue); //Narrow width
            //		Debug.DrawLine(Vector3.up+Vector3.zero, Vector3.up+Vector3.forward * hex_height, Color.red); // Hex height
            //		Debug.DrawLine(Vector3.up+Vector3.right*0.01f, Vector3.up+Vector3.right*0.01f + Vector3.forward * hexRad*2, Color.yellow); // Hex height right calc
            //		Debug.DrawLine(Vector3.up+Vector3.zero, Vector3.up+Vector3.left * wide_width, Color.cyan); // hex wide width
        }


		public Vector3[] NewGridCoords(int width, int height, Vector3 offset){
			//float hh = 1;
			//float hw = 1/(Mathf.Sqrt(3)/2f);

			Vector3[] result = new Vector3[width * height];
			//Debug.Log(width+ " * "+ height+" = "+result.Length);
			//Vector3 pos = transform.position;
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					int index = height * i + j;

					Vector2 pos = layout.OffsetCoordinates(i, j);
				

					result[index] = new Vector3(pos.x, transform.position.y + offset.y, pos.y);

				}
			}
			return result;
		}

		public Vector3[] HitCoords(Vector3[] origins){
			int all_layers = 1 << 2;
			all_layers = ~all_layers;
			Vector3[] result = HitCoords(origins, all_layers);
			return result;
		}


		///Fires Raycasts downwards to find grid positions on geometry
		public Vector3[] HitCoords(Vector3[] origins, int layer_mask){
			List<Vector3> result = new List<Vector3>();
			//List<Vector3> multihits = new List<Vector3>();
			//List<string> multilayer = new List<string>();
			float depth = 100;
			for (int i = 0; i < origins.Length; i++) {
				Ray ray = new Ray(origins[i], Vector3.down*depth);
				RaycastHit[] hits = Physics.RaycastAll(ray, depth, layer_mask);
			//	string mul_descr = i+"";

				for (int j = 0; j < hits.Length; j++) {
					//Debug.Log(hits[j].collider.name +" hit at " +hits[j].point);
					result.Add(hits[j].point + Vector3.up * gridElevation);


				}
			

			}
			//layered_coords = multihits.ToArray();
			//vertical_neighbors = multilayer.ToArray();
			Debug.Log (result.Count);
			return result.ToArray();
		}

		void CreateGridObj(){
			gridHolder = new GameObject("TheGrid");
            //gridHolder.transform.position = new Vector3(0, 0, 0);
            if (GameObject.Find("ImageTarget 1") != null)
                gridHolder.transform.parent = GameObject.Find("ImageTarget 1").transform;
            //gridHolder.transform.position = new Vector3(80, 25, -90);

			MeshFilter mf = gridHolder.AddComponent <MeshFilter>();
			MeshRenderer mr = gridHolder.AddComponent <MeshRenderer>();

			mf.mesh = MeshGen.CreateGridMesh(MeshGen.Hex(layout.wide_width/2), coords);//MeshGen.Square(0.5f);
			mf.mesh.name = "GridMesh";
			//RecalculateMeshHeights(mf.mesh);
			mf.mesh.RecalculateBounds();
			mf.mesh.RecalculateNormals();
			mr.material = gridMaterial;//tile.GetComponent<MeshRenderer>().sharedMaterial;
		}




	}
}