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

    public Player[] players;

    
    // Use this for initialization
    void Start ()
    {
        cardImage = GetComponent<Image>();
        cardImage = GameObject.Find("ImageCardPreview").GetComponent<Image>();
        cardStatsPreview = GameObject.Find("CardStatsPreview").GetComponent<Text>();

        players = new Player[2];
        players[0] = new Player();
    }

    /// <summary>
    /// Loads the source image preview.
    /// </summary>
    /// <param name="sourceImage">Source image.</param>
    public void LoadSourceImagePreview(int sourceImage)
    {
        //players[]
        Debug.Log("Source image: " + sourceImage);
        cardImage.sprite = spriteArray[sourceImage];

        int[] cardAttrs = players[0].activeCard.attributes;

        cardStatsPreview.text = "CardStatsPreview " +
                "\nStrenght:" + cardAttrs[0]
                + "\nAgility:" + cardAttrs[1]
                + "\nRange:" + cardAttrs[2];
    }
}
