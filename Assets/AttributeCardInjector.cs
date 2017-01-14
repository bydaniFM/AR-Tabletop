using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeCardInjector : MonoBehaviour {

    //one injection of attribute cards per turn
    public bool injectionPerTurn = false;

    public GameObject portraitButton1;
    public GameObject portraitButton2;
    public GameObject portraitButton3;

    public GameObject injectButton;
    public GameObject activateChangesButton;
    public GameObject declineInjectionButton;
    public GameObject abortInjectionButton;
    public GameObject injectionPannel;
    public GameObject cardImage;
    public GameObject minimizeProcessButton;
    public GameObject showInjectionProcessButton;

    // Use this for initialization
    void Start ()
    {
        portraitButton1 = GameObject.Find("PortraitButton1");
        portraitButton2 = GameObject.Find("PortraitButton2");
        portraitButton3 = GameObject.Find("PortraitButton3");
        injectButton = GameObject.Find("InjectButton");
        activateChangesButton = GameObject.Find("ActivateChangesButton");
        declineInjectionButton = GameObject.Find("DeclineInjectionButton");
        abortInjectionButton = GameObject.Find("AbortInjectionButton");
        injectionPannel = GameObject.Find("InjectionPannel");
        cardImage = GameObject.Find("CardImage");
        minimizeProcessButton = GameObject.Find("MinimizeProcessButton");
        showInjectionProcessButton = GameObject.Find("ShowInjectionProcessButton");

        activateChangesButton.SetActive(false);
        declineInjectionButton.SetActive(false);
        abortInjectionButton.SetActive(false);
        showInjectionProcessButton.SetActive(false);

        ShowInjectionButton();
        ShowCardImage();
        ActivatePannel();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Shows the injection button.
    /// </summary>
    public void ShowInjectionButton()
    {
        injectButton.SetActive(true);
    }

    /// <summary>
    /// Shows the image of the card that we are going
    /// to use as preview
    /// </summary>
    public void ShowCardImage()
    {

    }

    public void HideCardImage()
    {

    }

    /// <summary>
    /// Hides the button which begins the injection process of the card
    /// </summary>
    public void HideInjectionButton()
    {
        injectButton.SetActive(false);
    }

    /// <summary>
    /// If you press this button, the procces of injection
    /// of the card scanned begins
    /// It cannot be pressed if another injection has been done on this turn
    /// by the same player
    /// </summary>
    public void ActivateInjectionButton()
    {
        if (injectionPerTurn == false) //&& this button is pressed
        {
            HighlightPortrait();
            HideInjectionButton();
            HideCardImage();
            ShowAbortInjectionButton();
        }
            
        else
            Debug.Log("You cannot inject another card on the same turn.");
    }

    //-------------------------------------------------------------------
    /// <summary>
    /// Highlight the portrait button which shows the units
    /// will need communication with others objetcs form the ui
    /// </summary>
    public void HighlightPortrait()
    {

    }

    public void HideHighlightPortrait()
    {
        
    }
    //-------------------------------------------------------------------

    /// <summary>
    /// Shows the AbortInjectionButton
    /// </summary>
    public void ShowAbortInjectionButton()
    {
        abortInjectionButton.SetActive(true);
    }

    /// <summary>
    /// Desactivates the process of injection
    /// </summary>
    public void AbortInjectionButton()
    {
        injectionPannel.SetActive(false);
    }

    /// <summary>
    /// Goes back, highlights again the portraits of the units and 
    /// </summary>
    public void ShowDeclineInjectionButton()
    {
        declineInjectionButton.SetActive(true);
    }

    public void ActivateDeclineInjectionButton()
    {
        declineInjectionButton.SetActive(false);
        HighlightPortrait();
        ShowAbortInjectionButton();
        HideActivateChangesButton();
    }

    /// <summary>
    /// This function will be called when the playes has pressed
    /// a portrait
    /// The unit portrait on the UI will need to have a
    /// call to this function
    /// Thhis funcion will show the changes provides on the card choosed
    /// </summary>
    public void ShowChangesOnThisCard()
    {
        activateChangesButton.SetActive(true);
        declineInjectionButton.SetActive(true);
    }

    /// <summary>
    /// Activates the changes of the injection of the card selected on the character
    /// </summary>
    public void ActivateChangesInjection()
    {

    }

    public void HideActivateChangesButton()
    {
        activateChangesButton.SetActive(false);
    }


    //------------------------------------------------------------------------------
    /// <summary>
    /// hides the pannel with the procces of injection
    /// lets the players see whats happening on the field
    /// </summary>
    public void ActivateMinimizeButton()
    {
        injectionPannel.SetActive(false);
        showInjectionProcessButton.SetActive(true);
    }

    /// <summary>
    /// Activates again the pannel which contains the info and about
    /// related to the injection of cards
    /// Called by the ShowInjectionProcessButton button button
    /// </summary>
    public void ActivatePannel()
    {
        injectionPannel.SetActive(true);
    }

    public void ActivateShowInjectionProcessButton()
    {
        showInjectionProcessButton.SetActive(false);
        ActivatePannel();
    }
}
