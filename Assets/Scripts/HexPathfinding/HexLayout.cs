using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexMap{
	public class HexLayout {

		private static float SQRT3 = Mathf.Sqrt(3);
		public readonly float wide_width;// = hexRad * 2;
		public readonly float narrow_width; //= wide_width * 3 / 4;
		public readonly float hex_height;// = hexRad * SQRT3;

		public readonly Vector3 origin;
		public readonly Vector3 offset;
		public static HexLayout instance;
		public HexLayout(float hexRad) : this (hexRad, Vector3.zero, Vector2.one){}

		public HexLayout(float hexRad, Vector3 origin) : this (hexRad, origin, Vector2.one){}

		public HexLayout(float hexRad, Vector3 origin, Vector2 offset){
			this.origin = origin;
			this.offset = offset;
			instance = this;

			wide_width = ((hexRad*2) / SQRT3)*2; // dist between opposing vertices. Large diameter
			narrow_width = wide_width * 3 / 4; // dist between centers of adjacent hexes
			hex_height = wide_width * 0.5f * SQRT3; //dist between opposing edges. Small diameter

		}

		public HexCell WorldToHex(Vector3 vec){
			float x = vec.x - (origin.x + offset.x);
			float z = vec.z - (origin.z + offset.z);
			int u = Mathf.RoundToInt(x / narrow_width);
			int v = Mathf.RoundToInt(z / hex_height - u * 0.5f);
			Debug.Log(x+"/"+narrow_width+ ":"+z+"/"+hex_height +"-"+u+"*0.5" );
			Debug.Log("Vec "+vec + " u "+u + " v "+v);
			return new HexCell(u, v);
		}

		Vector3 HexToWorld(){
			//int u = Mathf.CeilToInt(vec.x / narrow_width);
			//int v = Mathf.CeilToInt(vec.z / hex_height - u * 0.5f);
			return Vector3.zero;
		}

		public Vector2 OffsetCoordinates(int row, int column){
			float xpos = origin.x + offset.x + row * narrow_width ;
			float ypos = origin.z + offset.z + column * hex_height + row%2*(hex_height/2);
			return new Vector2(xpos, ypos);
		}
	}
}