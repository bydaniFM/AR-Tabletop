using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARAssetsBehaviour : MonoBehaviour {

	WebCamTexture wca_asset;

	// Use this for initialization
	void Start () 
	{
		Application.RequestUserAuthorization(UserAuthorization.WebCam);
		/*
		wca_asset = new WebCamTexture ();

		renderer.material.mainTexture = wca_asset;
		wca_asset.play ();
		*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
