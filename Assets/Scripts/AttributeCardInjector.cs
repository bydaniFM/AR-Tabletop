using System.Collections;

using System.Collections.Generic;

using UnityEngine.UI;

using UnityEngine;



namespace ARTCards
{
        
    public class AttributeCardInjector : MonoBehaviour
    {
               
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
        public Text unitStatsPreview;
        public GameObject minimizeProcessButton;
        public GameObject showInjectionProcessButton;

       // public Player player1;
       // public Player player2;

       public Player[] players;

       // public Unit unit1;
       // public Unit unit2;
       // public Unit unit3;

	    //public Attribute tmp_strength;
        //public Attribute tmp_agility;
        //public Attribute tmp_range;

        public ScannedCardActivator scannedCardActivator;

        // Use this for initialization
        void Awake()
        {
        	
           // player1 = new Player();
        }


        void Start()
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

           // unit1 = new ARTCards.Unit();
            unitStatsPreview = GameObject.Find("UnitStatsPreview").GetComponent<Text>();

			players = new Player[2];
			players[0] = new Player();
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
            HideInjectionButton();
            HideCardImage();
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

        public void ShowChangesOnThisCard(int unitSelected)
        {
            activateChangesButton.SetActive(true);
            declineInjectionButton.SetActive(true);

            ShowUnitStatsPreview(unitSelected);
        }

        /// <summary>
        /// Takes the unit stats and show them with the changes of the card
        /// on the UI as a text
        /// </summary>
        public void ShowUnitStatsPreview(int unitID)
        {           
            Debug.Log("Unit name: " + players[0].units[unitID]);
            //string[] keys = new string[unit1.attrs.Count];
            //unit1.attrs.Keys.CopyTo(keys, 0);

            ////access to the Unit related to the portrait
            //unit1.attrs.TryGetValue(keys[0], out tmp_strength);
            //unit1.attrs.TryGetValue(keys[1], out tmp_agility);
            //unit1.attrs.TryGetValue(keys[2], out tmp_range);

            ////unit1.attrs.TryGetValue("Strength", out tmp_strength);
            ////unit1.attrs.TryGetValue("Agility", out tmp_agility);
            ////unit1.attrs.TryGetValue("Range", out tmp_range);

            //STARTS HERE: We need the attributes of the card we are going to apply
            //and the attributes from the Unit chosen
            //When we press on a portrait button, the inspector sends an int with the unit we want
            Debug.Log("Player " + players[0]);
            Debug.Log("Units " + players[0].units[0]);

            Attribute[] arr_attributesUnit = new Attribute[players[0].units[unitID].attrs.Count];
            players[0].units[unitID].attrs.Values.CopyTo(arr_attributesUnit, 0);


            // Attribute[] arr_attributesActiveCard = new Attribute[player1.activeCard.attributes.GetLength(0)];
            //  player1.activeCard.attributes.CopyTo(arr_attributesActiveCard, 0);
			if (players[0].activeCard == null){
            	Debug.LogError("Active card is not setup! Aborting");
            	return;
            }
            int[] cardAttrs = players[0].activeCard.attributes;


            string[] statBonus = new string[cardAttrs.Length];
            for (int i = 0; i < statBonus.Length; i++)
            {
                if (cardAttrs[i] < 0)
                {
                    statBonus[i] = "<color=#ff0000ff> " + cardAttrs[i] + " </color>";
                }
                else {

                    statBonus[i] = "<color=#00ff00ff> +" + cardAttrs[i] + " </color>";
                }
            }

            //here we are checking if the card can be applied
            //if not, we will design a function to finish the injection process
            if (!players[0].activeCard.isNotOverflowing(arr_attributesUnit))//if is false
            {
                Debug.LogError("The card overflows unit stats");
                return;
            }

            //we need to make calculations of the attributes to show then on the
            //following text value, which belongs to the UI

            Debug.Log("debug for unit stats" + unitStatsPreview);

            ////Here we upload the unit stats preview with the 
            //values calculated with card functions
            //One this has been done, the player can press Activate Injection
            //Activate Injection button will call the funcion ApplyChangesInjection
            //unitStatsPreview = GameObject.Find("UnitStatsPreview").GetComponent<Text>();
            unitStatsPreview.text = "StatsChangedPreview\n Unit-> " + players[0].units[unitID].name +
                    "\nStrenght:" + arr_attributesUnit[0].Value + statBonus[0] 
                    + "\nAgility:" + arr_attributesUnit[1].Value + statBonus[1] 
                    + "\nRange:" + arr_attributesUnit[2].Value + statBonus[2];

            Debug.Log("wbiwbv");
        }


        /// <summary>
        /// Apply the changes of the injection of the card selected on the character
        /// </summary>
        public void ApplyChangesInjection(int unitID)
        {
			//Attribute[] arr = new Attribute[players[0].units[unitID].attrs.Count];
			//players[0].units[unitID].attrs.Values.CopyTo(arr, 0);

            Attribute[] arr_attributesUnit = new Attribute[players[0].units[unitID].attrs.Count];
            players[0].units[unitID].attrs.Values.CopyTo(arr_attributesUnit, 0);

            if (players[0].activeCard.isNotOverflowing(arr_attributesUnit))
            {
                players[0].units[unitID].Buff(players[0].activeCard.attributes);
               // players[i].deck.Bury(players[i].activeCard);
                // players[i].activeCard = null;
            //    // holdingCard = false;
            //}
            }
            //Once the changes had been applied to the unit, we desactivate the process of injection
            Debug.Log("Finishing the process of injection of the card selected");
            injectionPannel.SetActive(false);
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

    }

}
