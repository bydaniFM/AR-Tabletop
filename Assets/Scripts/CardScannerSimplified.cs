using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using ARTCards;
using UnityEngine.UI;

public class CardScannerSimplified : MonoBehaviour {

    public Image cardImage;
    public Text cardStatsPreview;

    public Sprite[] spriteArray;

    public Player player;

    //public GameObject[] arrCardCanvas;

    
    // Use this for initialization
    void Start ()
    {
        cardImage = GetComponent<Image>();
        cardImage = GameObject.Find("ImageCardPreview").GetComponent<Image>();
        cardStatsPreview = GameObject.Find("CardStatsPreview").GetComponent<Text>();

        player = new Player();

        //arrCardCanvas = new GameObject[15];
        //for (int i = 0; i < 15; i++) {
        //    Debug.Log("Initialasing card " + i);
        //    arrCardCanvas[i] = GameObject.Find("Card" + i);//("Card" + i.ToString());
        //    if (arrCardCanvas != null)
        //        arrCardCanvas[i].SetActive(false);
        //}
    }

    /// <summary>
    /// Loads the source image preview.
    /// </summary>
    /// <param name="sourceImage">Source image.</param>
    public void LoadSourceImagePreview(int sourceImage)
    {
        //players[]
        //Debug.Log("Source image: " + sourceImage);
        cardImage.sprite = spriteArray[sourceImage];

        int[] cardAttrs = player.activeCard.attributes;

        cardStatsPreview.text = "CardStatsPreview " +
                "\nStrenght:" + cardAttrs[0]
                + "\nAgility:" + cardAttrs[1]
                + "\nRange:" + cardAttrs[2];



        //arrCardCanvas[sourceImage].SetActive(true);
    }
}
