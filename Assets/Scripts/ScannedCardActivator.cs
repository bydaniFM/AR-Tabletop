﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System;
using System.Linq;
using ARTCards;



/// <summary>
/// Scanned card activator is an script activated when the ImageTarget has been 
/// tracked by the ARCamera
/// This needs to bee done like this beacuse even the card is no longer been
/// tracked by the camera, we need the injection process of the card to start
/// and start showing the UI stuff
/// His name contains the cardID that we are going to use
/// We will need to search for it with the Card scripting stuff
/// </summary>
public class ScannedCardActivator : MonoBehaviour//, ITrackableEventHandler 
{
    //public TrackableBehaviour mTrackableBehaviour;
    public bool cardTracked;
    public int cardID;//ALSO for card source image
	private AttributeCardInjector injector;
    private CardScannerSimplified cs_simplified;

	public GameObject imageTarget;

    public GameObject plane;

	//private TrackableBehaviour mTrackableBehaviour;
    


    void Start()
    {
		cardTracked = true;
        injector = FindObjectOfType<AttributeCardInjector>();
        cs_simplified = FindObjectOfType<CardScannerSimplified>();
        //mTrackableBehaviour = GetComponent<TrackableBehaviour> ();

		imageTarget = GameObject.Find(transform.name);
        //cardTracked = imageTarget.gameObject.GetComponent<DefaultTrackableEventHandler> ().isFound;

        //plane = GetComponent<GameObject>();
        
    }

	//public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	//{
	//	if (newStatus == TrackableBehaviour.Status.DETECTED ||
	//	   newStatus == TrackableBehaviour.Status.TRACKED ||
	//	   newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) 
	//	{
	//		Activate ();
	//	}
	//}

	void Update()
	{
        //if(mTrackableBehaviour.)
        //if (imageTarget.gameObject.GetComponent<DefaultTrackableEventHandler>().isFound) {
        if (plane.gameObject.GetComponent<MeshRenderer>().enabled) {
            if (cardTracked) {
                Debug.Log("Activating");
                Activate();
                cardTracked = false;
            }
        }
	}

	public void Activate()
	{
		cardID = Int32.Parse(transform.name); 
		Debug.Log("CardID tracked: " + cardID);
		//Debug.Log(injector);
        //Debug.Log(injector.players[0]);
        //Debug.Log(injector.players[0].deck);

        //Debug.Log(cs_simplified);
        //Debug.Log(cs_simplified.players[0]);
        //Debug.Log(cs_simplified.players[0].deck);

        //injector.players[0].activeCard = injector.players[0].deck.GetCardById("" + cardID);

        cs_simplified.player.activeCard = cs_simplified.player.deck.GetCardById("" + cardID);


        //getting the correct source image for the preview
        //Debug.Log("Load source image preview");
        //injector.LoadSourceImagePreview (cardID);

        cs_simplified.LoadSourceImagePreview(cardID);
        
    }

    //public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
    //    if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
    //        Debug.Log("FOUND");
    //    } else {
    //        Debug.Log("LOST");
    //    }
    //}
}
 
