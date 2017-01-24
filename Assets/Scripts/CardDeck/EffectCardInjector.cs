using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectCardInjector : MonoBehaviour {

    public bool effectCardPerTurn;

    //public EffectCardInjector effectCardInjector;

    //need in case to apply an effect to a unit
    public GameObject effectCardPannel;

    public GameObject portraitButton1;
    public GameObject portraitButton2;
    public GameObject portraitButton3;

    public GameObject effectCardImage;
    public Text effectCardInfo;
    public Text unitStatsPreviewEffectCard;

    //to continue the proces of using a card
    public GameObject activateEffectCardButton;
    public GameObject declineUseEffectCardButton;

    //to end the process
    public GameObject exitEffectCardButton;

    //to apply boost on units
    public GameObject activateChangesEffectCardButton;
    public GameObject declineChangesEffectCardButton;

    public GameObject minimizeEffectCardButton;
    public GameObject showEffectCardProcessButton;

    public Player[] player;

    //sprite array for the images of the effect cards
    public Sprite[] spriteArrayEffectCards;

    // Use this for initialization
    void Start()
    {
        //effectCardInjector = FindObjectOfType<EffectCardInjector>();

        portraitButton1 = GameObject.Find("PortraitButton1");
        portraitButton2 = GameObject.Find("PortraitButton2");
        portraitButton3 = GameObject.Find("PortraitButton3");

        activateEffectCardButton = GameObject.Find("activateEffectCardButton");
        activateChangesEffectCardButton = GameObject.Find("activateChangesEffectCardButton");
        exitEffectCardButton = GameObject.Find("ExitEffectCardButton");

        minimizeEffectCardButton = GameObject.Find("MinimizeEffectCardButton");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateEffectCard()
    {
        if (effectCardPerTurn == false) //&& this button is pressed
        {
            //HighlightPortrait();
            //HideInjectionButton();
            //HideCardImage();
            //ShowAbortInjectionButton();
        }
        else
            Debug.Log("You cannot inject another card on the same turn.");

    }


    /// <summary>
    /// Desactivates the process of injection
    /// </summary>
    public void ExitEffectCard()
    {
        effectCardPannel.SetActive(false);

    }

    /// <summary>
    /// Needed if the player is injecting some behaviour/boost
    /// on a unit. We need to go back to select another unit
    /// </summary>
    public void ActivateDeclineEffectCardButton()
    {
        declineUseEffectCardButton.SetActive(false);
        //HighlightPortrait();
        activateEffectCardButton.SetActive(false);
        effectCardImage.SetActive(false);
        exitEffectCardButton.SetActive(false);
        activateChangesEffectCardButton.SetActive(false);
    }

    ///// <summary>
    ///// Loads the source image preview.
    ///// </summary>
    ///// <param name="sourceImage">Source image.</param>
    //public void LoadSourceImagePreview(int sourceImage)
    //{
    //    //players[]
    //    Debug.Log("Source image: " + sourceImage);
    //    effectCardImage.GetComponent<Image>().sprite = spriteArrayEffectCards[sourceImage];

    //    //for (int i = 0; i < 16; i++)
    //    //{

    //    //    cardImages[i].SetActive(false);
    //    //}

    //    //cardImages[sourceImage].SetActive(true);

    //    int[] cardAttrs = player.activeCard.attributes;

    //    effectCardInfo.text = "Info about the effect card activated";
    //}

    ///// <summary>
    ///// This function will be called when the playes has pressed
    ///// a portrait
    ///// The unit portrait on the UI will need to have a
    ///// call to this function
    ///// Thhis funcion will show the changes provides on the card choosed
    ///// </summary>

    //public void ShowChangesOnThisCard(int unitSelected)
    //{
    //    activateChangesEffectCardButton.SetActive(true);
    //    declineChangesEffectCardButton.SetActive(true);

    //    ShowUnitStatsPreview(unitSelected);
    //}

    ///// <summary>
    ///// Needed for effect cards that boost the units
    ///// </summary>
    //public void ShowUnitStatsPreview(int unitID)
    //{
    //    //Debug.Log("Unit name: " + player.units[unitID]);
    //    //Debug.Log("Player " + player);
    //    //Debug.Log("Units " + player.units[0]);

    //    Attribute[] arr_attributesUnit = new Attribute[player.units[unitID].attrs.Count];
    //    //player.units[unitID].attrs.Values.CopyTo(arr_attributesUnit, 0);
    //    player.units[unitID].attrs.Values.CopyTo(arr_attributesUnit, 0);

    //    // Attribute[] arr_attributesActiveCard = new Attribute[player1.activeCard.attributes.GetLength(0)];
    //    //  player1.activeCard.attributes.CopyTo(arr_attributesActiveCard, 0);
    //    //if (player.activeCard == null){
    //    if (player.activeCard == null)
    //    {
    //        Debug.LogError("Active card is not setup! Aborting");
    //        return;
    //    }
    //    //int[] cardAttrs = player.activeCard.attributes;
    //    int[] cardAttrs = player.activeCard.attributes;


    //    string[] statBonus = new string[cardAttrs.Length];
    //    for (int i = 0; i < statBonus.Length; i++)
    //    {
    //        if (cardAttrs[i] < 0)
    //        {
    //            statBonus[i] = "<color=#ff0000ff> " + cardAttrs[i] + " </color>";
    //        }
    //        else
    //        {

    //            statBonus[i] = "<color=#00ff00ff> +" + cardAttrs[i] + " </color>";
    //        }
    //    }

    //    //here we are checking if the card can be applied
    //    //if not, we will design a function to finish the injection process
    //    //if (!player.activeCard.isNotOverflowing(arr_attributesUnit))//if is false
    //    if (!player.activeCard.isNotOverflowing(arr_attributesUnit))
    //    {
    //        Debug.LogError("The card overflows unit stats");
    //        return;
    //    }

    //    //we need to make calculations of the attributes to show then on the
    //    //following text value, which belongs to the UI

    //    Debug.Log("debug for unit stats" + unitStatsPreview);

    //    ////Here we upload the unit stats preview with the 
    //    //values calculated with card functions
    //    //One this has been done, the player can press Activate Injection
    //    //Activate Injection button will call the funcion ApplyChangesInjection
    //    //unitStatsPreview = GameObject.Find("UnitStatsPreview").GetComponent<Text>();
    //    //unitStatsPreview.text = "StatsChangedPreview\n Unit-> " + player.units[unitID].name +
    //    unitStatsPreviewEffectCard.text = "StatsChangedPreview\n Unit-> " + player.units[unitID].name +
    //            "\nStrenght:" + arr_attributesUnit[0].Value + statBonus[0]
    //            + "\nAgility:" + arr_attributesUnit[1].Value + statBonus[1]
    //            + "\nRange:" + arr_attributesUnit[2].Value + statBonus[2];

    //    Debug.Log("wbiwbv");
    //}


    ///// <summary>
    ///// Apply the changes of the injection of the card selected on the character
    ///// </summary>
    //public void ApplyChangesInjection(int unitID)
    //{
    //    //Attribute[] arr = new Attribute[player.units[unitID].attrs.Count];
    //    //player.units[unitID].attrs.Values.CopyTo(arr, 0);

    //    Attribute[] arr_attributesUnit = new Attribute[player.units[unitID].attrs.Count];
    //    //player.units[unitID].attrs.Values.CopyTo(arr_attributesUnit, 0);
    //    player.units[unitID].attrs.Values.CopyTo(arr_attributesUnit, 0);
    //    //if (player.activeCard.isNotOverflowing(arr_attributesUnit))
    //    if (player.activeCard.isNotOverflowing(arr_attributesUnit))
    //    {
    //        //player.units[unitID].Buff(player.activeCard.attributes);
    //        player.units[unitID].Buff(player.activeCard.attributes);
    //        // player[i].deck.Bury(player[i].activeCard);
    //        // player[i].activeCard = null;
    //        //    // holdingCard = false;
    //        //}
    //    }
    //    //Once the changes had been applied to the unit, we desactivate the process of injection
    //    Debug.Log("Finishing the process of injection of the card selected");
    //    effectCardPannel.SetActive(false);
    //}

    //here we place some effect card needed behaviours

    public void UseEffectOnGridPosition()
    {

    }

    public void ChooseEnemyUnit()
    {

    }
    

    //------------------------------------------------------------------------------

    /// <summary>
    /// hides the pannel with the procces of injection
    /// lets the player see whats happening on the field
    /// </summary>
    public void ActivateMinimizeEffectCardProcess()
    {
        effectCardPannel.SetActive(false);
        showEffectCardProcessButton.SetActive(true);
    }

    /// <summary>
    /// Activates again the pannel which contains the info and about
    /// related to the injection of cards
    /// Called by the ShowInjectionProcessButton button button
    /// </summary>
    public void ActivateEffectCardPannel()
    {
        effectCardPannel.SetActive(true);
    }

    public void ActivateShowEffectCardProcessButton()
    {
        showEffectCardProcessButton.SetActive(false);
        ActivateEffectCardPannel();
    }


}
