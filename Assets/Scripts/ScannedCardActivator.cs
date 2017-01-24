using System.Collections;

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
    public AttributeCardInjector injector;

    public UIController UIController;

    public GameObject plane;

    void Start()
    {
        injector = FindObjectOfType<AttributeCardInjector>();
        cardTracked = true;
        cardID = Int32.Parse(transform.name);
    }

   /* public ScannedCardActivator()
    { 
        cardTracked = true;
    }*/

    void Update()
    {
        if (plane.gameObject.GetComponent<MeshRenderer>().enabled) {
            if (cardTracked) {
                //cardID = Int32.Parse(transform.name);
                Debug.Log("CardID tracked: " + cardID);
                this.Activate();
                cardTracked = false;
                //Debug.Log(injector);
                //Debug.Log(injector.player);
                //Debug.Log(injector.player.deck);
                
            }
        }
    } 

    public void Activate()
    {
        Debug.Log("Activate");
        //injector.player.activeCard = injector.player.deck.GetCardById("" + cardID);
        //injector.LoadSourceImagePreview(cardID);

        UIController.player.activeCard = UIController.player.deck.GetCardById(UIController.cardImageArray[cardID].textId);
        UIController.LoadSourceImagePreview(cardID);
        UIController.LoadAttributeCardButtons();

        Debug.Log("Scanned card"+ UIController.player.activeCard.id);
    }


}
 
