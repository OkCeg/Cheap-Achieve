using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellPriceText : MonoBehaviour
{
    private AllMain allMain;

    private Image image;
    private Text sellText;
    private CardData cardData;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();

        image = GetComponentInParent<Image>();
        sellText = GetComponent<Text>();
        cardData = GetComponentInParent<CardData>();
    }

    private void Update()
    {
        if (image.sprite != allMain.defaultSprite)
        {
            //shows money bag sprite
            transform.GetChild(0).gameObject.SetActive(true);
            sellText.text = "" + cardData.sellPrice;
            if (cardData.sellPrice > cardData.baseSP)
            {
                sellText.color = new Color(0, 128 / 255f, 43 / 255f);
            }
            else if (cardData.sellPrice < cardData.baseSP)
            {
                sellText.color = new Color(1, 26 / 255f, 26 / 255f);
            }
            else
            {
                sellText.color = Color.black;
            }
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            sellText.text = "";
        }
    }
}
