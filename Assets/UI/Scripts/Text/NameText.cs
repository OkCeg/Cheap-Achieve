using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour
{
    private Image image;
    private Text text;
    private RectTransform parentRect;

    private int defaultSize;

    //for color
    private CardData cardData;

    private void Awake()
    {
        image = transform.parent.parent.GetComponent<Image>();
        text = GetComponent<Text>();
        parentRect = GetComponent<RectTransform>();

        defaultSize = text.fontSize;

        cardData = GetComponentInParent<CardData>();
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        text.fontSize = defaultSize;

        //no while loop just in case of crash
        for (int i = 0; i < 5; i++)
        {
            if (TooLarge())
            {
                text.fontSize -= 3;
            }
        }

        text.text = image.sprite.name;

        //color
        switch (cardData.rarity)
        {
            case "Common":
                //black
                text.color = new Color(0, 0, 0);
                break;
            case "Uncommon":
                //light green
                text.color = new Color(165 / 255f, 249 / 255f, 106 / 255f);
                break;
            case "Rare":
                //light orange
                text.color = new Color(249 / 255f, 190 / 255f, 73 / 255f);
                break;
            case "Very Rare":
                //magenta
                text.color = new Color(179 / 255f, 0, 179 / 255f);
                break;
            default:
                //black (just in case)
                text.color = new Color(0, 0, 0);
                break;
        }
    }

    //check if textWidth is greater than allowed width
    private bool TooLarge()
    {
        float textWidth = LayoutUtility.GetPreferredWidth(text.rectTransform);

        float allowedWidth = parentRect.rect.width;

        return allowedWidth <= textWidth;
    }
}
