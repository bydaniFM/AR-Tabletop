using UnityEngine;

namespace HexMap{
	public class MeshGen {
		//static float SQRT3 = Mathf.Sqrt(3);

		public static Mesh Square(float extent){
			Mesh result = new Mesh();

			Vector3[] vertices = new Vector3[]
			{
				new Vector3( extent, 0,  extent),
				new Vector3( extent, 0, -extent),
				new Vector3(-extent, 0,  extent),
				new Vector3(-extent, 0, -extent)
			};

			Vector2[] uv = new Vector2[]
			{
				new Vector2(1, 1),
				new Vector2(1, 0),
				new Vector2(0, 1),
				new Vector2(0, 0)
			};

			int[] triangles = new int[]
			{
				0, 1, 2,
				2, 1, 3
			};

			result.vertices = vertices;
			result.uv = uv;
			result.triangles = triangles;

			return result;
		}

		public static Mesh Hex(float radius){
			Mesh result = new Mesh();
			//float r = extent/(SQRT3/2f);

			Vector3[] p = new Vector3[7];
			p[0] = Vector3.zero;
			for (int i = 1; i <= 6; i++) {
				p[i] = new Vector3(radius*Mathf.Cos(2*Mathf.PI*i/6), 0, radius*Mathf.Sin(2*Mathf.PI*i/6));
			}

			   /*[] // point array
			{
				new Vector3 (0,0,0),
				new Vector3 (r*Mathf.Cos(2*Mathf.PI*1/6), 0, r*Mathf.Sin(2*Mathf.PI*1/6)),
				new Vector3 (r*Mathf.Cos(2*Mathf.PI*2/6), 0, r*Mathf.Sin(2*Mathf.PI*2/6)),
				new Vector3 (r*Mathf.Cos(2*Mathf.PI*3/6), 0, r*Mathf.Sin(2*Mathf.PI*3/6)),
				new Vector3 (r*Mathf.Cos(2*Mathf.PI*4/6), 0, r*Mathf.Sin(2*Mathf.PI*4/6)),
				new Vector3 (r*Mathf.Cos(2*Mathf.PI*5/6), 0, r*Mathf.Sin(2*Mathf.PI*5/6)),
				new Vector3 (r*Mathf.Cos(2*Mathf.PI*6/6), 0, r*Mathf.Sin(2*Mathf.PI*6/6))
			};*/
			

			Vector3[] vertices = new Vector3[]{
				//p1,p2,p0,
				p[0],p[2],p[1], // 0,2,1,4,7,10,13
				p[0],p[3],p[2],
				p[0],p[4],p[3],
				p[0],p[5],p[4],
				p[0],p[6],p[5],
				p[0],p[1],p[6]
			};
			int[] triangles = new int[]
			{
			    0,1,2,
			    3,4,5,
			    6,7,8,
			    9,10,11,
				12,13,14,
				15,16,17
			};
			
			Vector2 uv0 = new Vector2(0.5f, 0f);
			Vector2 uv1 = new Vector2(0, 1f);
			Vector2 uv2 = new Vector2(1, 1);
			
			Vector2[] uv = new Vector2[]{
	    		uv0,uv1,uv2,
				uv0,uv1,uv2,
				uv0,uv1,uv2,
				uv0,uv1,uv2,
				uv0,uv1,uv2,
				uv0,uv1,uv2
				
			};

			result.vertices = vertices;
			result.uv = uv;
			result.triangles = triangles;
			
			return result;
		}

		///Creates a combined Mesh of squares centered at coords
		public static Mesh CreateGridMesh(Vector3[] coords){
			Mesh mesh = MeshGen.Square(0.5f);
			return CreateGridMesh(mesh, coords);
		}

		public static Mesh CreateGridMesh(Mesh mesh, Vector3[] coords){
			Mesh result = new Mesh();
			CombineInstance[] combineDetails = new CombineInstance[coords.Length];
			for (int i = 0; i < coords.Length; i++) {
				Matrix4x4 translation = TranslationMatrix(coords[i]);

				combineDetails[i].mesh = mesh;
				combineDetails[i].transform = translation;
			}
			result.CombineMeshes(combineDetails);
			return result;
		}

		private static Matrix4x4 TranslationMatrix(Vector3 offset) {
			Matrix4x4 matrix = Matrix4x4.identity;
			matrix.m03 = offset.x;
			matrix.m13 = offset.y;
			matrix.m23 = offset.z;
			return matrix;
		}
	}
}