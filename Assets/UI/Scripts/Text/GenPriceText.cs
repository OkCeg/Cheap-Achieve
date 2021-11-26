using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenPriceText : MonoBehaviour
{
    private AllMain allMain;

    private Image image;
    private Text genText;
    private CardData cardData;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();

        image = GetComponentInParent<Image>();
        genText = GetComponent<Text>();
        cardData = GetComponentInParent<CardData>();
    }

    private void Update()
    {
        if (image.sprite != allMain.defaultSprite)
        {
            //shows sun sprite
            transform.GetChild(0).gameObject.SetActive(true);

            genText.text = "" + cardData.coinsPerDay;
            if (cardData.coinsPerDay > cardData.baseCPD)
            {
                genText.color = new Color(0, 128 / 255f, 43 / 255f);
            }
            else if (cardData.coinsPerDay < cardData.baseCPD)
            {
                genText.color = new Color(1, 26 / 255f, 26 / 255f);
            }
            else
            {
                genText.color = Color.black;
            }
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            genText.text = "";
        }
    }
}
