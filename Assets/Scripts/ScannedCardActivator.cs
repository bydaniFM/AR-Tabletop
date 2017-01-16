using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using Vuforia;

using System;

using System.Linq;






/// <summary>

/// This script will be inside the image target.

/// Will be activated when the imagetarget is scanned

/// This will activate the control of injection cards

public class ScannedCardActivator : MonoBehaviour
{
    public TrackableBehaviour mTrackableBehaviour;

    public bool cardTracked;

    public int cardID;

    public ScannedCardActivator()
    { 

        cardTracked = true;

    }

     
    void Update()
    { 
 
        if (cardTracked)
 
        {
 
            cardID = Int32.Parse(transform.parent.name);
 
            Debug.Log("CardID tracked: " + cardID);
 
            cardTracked = false;
 
        }
 
    }
 
}
 
