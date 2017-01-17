using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexMap{
	public class GridScaler : MonoBehaviour {
		public Transform token1;
		public Transform token2;
		[Tooltip("How far removed are tokens from each other.")]
		public int hexDist;
		private GridBuilder builder;

		// Use this for initialization
		void Awake () {
			builder = FindObjectOfType<GridBuilder>();

			Vector3 newpos = new Vector3(token1.position.x, builder.transform.position.y, token1.position.z);
			builder.transform.position = newpos;
			if(hexDist != 0){
				builder.hexRad = Vector3.Distance(token1.position, token2.position)/(hexDist*2f);	
			}
			else{
				Debug.LogError("Set Hex Distance on a Grid Scaler!");
			}
			Destroy(token1.gameObject);
			Destroy(token2.gameObject);
		}
	}
}