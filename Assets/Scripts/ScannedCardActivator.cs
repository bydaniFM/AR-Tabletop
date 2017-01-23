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
public class ScannedCardActivator : MonoBehaviour
{
    public TrackableBehaviour mTrackableBehaviour;
    public bool cardTracked;
    public int cardID;
    private AttributeCardInjector injector;

    public GameObject plane;

    void Start()
    {
        injector = FindObjectOfType<AttributeCardInjector>();
        cardTracked = true;
    }

   /* public ScannedCardActivator()
    { 
        cardTracked = true;
    }*/

    void Update()
    {
        if (plane.gameObject.GetComponent<MeshRenderer>().enabled) {
            if (cardTracked) {
                cardID = Int32.Parse(transform.parent.name);
                Debug.Log("CardID tracked: " + cardID);
                cardTracked = false;
                Debug.Log(injector);
                Debug.Log(injector.player);
                Debug.Log(injector.player.deck);
                injector.player.activeCard = injector.player.deck.GetCardById("" + cardID);
            }
        }
    } 


}
 
