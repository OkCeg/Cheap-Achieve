using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoText : MonoBehaviour
{
    private CardData cardData;
    private Text text;
    private RectTransform parentRect;

    private int defaultSize;

    private void Awake()
    {
        cardData = GetComponentInParent<CardData>();
        text = GetComponent<Text>();
        parentRect = GetComponent<RectTransform>();

        defaultSize = text.fontSize;
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        text.fontSize = defaultSize;

        //no while loop just in case of crash
        for (int i = 0; i < 8; i++)
        {
            if (TooLarge())
            {
                text.fontSize -= 3;
            }
        }

        text.text = cardData.infoText;
    }

    //check if textWidth is greater than allowed width
    private bool TooLarge()
    {
        float textHeight = LayoutUtility.GetPreferredHeight(text.rectTransform);

        float allowedHeight = parentRect.rect.height;

        return allowedHeight <= textHeight;
    }
}
