using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RarityBorder : MonoBehaviour
{
    private Sprite[] borders;

    private RectTransform rt;
    private float width;
    private float height;

    private AllMain allMain;
    private Sprite empty;
    private GameObject[] buttons;

    private CardData cardData;

    private Image image;
    private Image defaultImage;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        borders = allMain.borders;
        empty = allMain.empty;
        defaultImage = allMain.defaultImage;
        buttons = allMain.buttonsGameObjects;

        rt = gameObject.GetComponent<RectTransform>();
        width = rt.rect.width;
        height = rt.rect.height;

        image = Instantiate(defaultImage, gameObject.transform);

        image.rectTransform.sizeDelta = new Vector2(width, height);

        image.transform.SetParent(gameObject.transform.parent);
        image.transform.SetAsFirstSibling();

        cardData = GetComponent<CardData>();
    }

    private void Update()
    {
        switch (cardData.rarity)
        {
            case "Common":
                image.sprite = borders[0];
                break;
            case "Uncommon":
                image.sprite = borders[1];
                break;
            case "Rare":
                image.sprite = borders[2];
                break;
            case "Very Rare":
                image.sprite = borders[3];
                break;
            default:
                //for board buttons
                image.sprite = empty;
                break;
        }
    }
}
