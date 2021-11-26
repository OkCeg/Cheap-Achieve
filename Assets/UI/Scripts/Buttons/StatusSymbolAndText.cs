using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusSymbolAndText : MonoBehaviour
{
    private CardData cardData;
    private Text text;
    private Image image;
    [SerializeField] private Sprite snowflake;
    [SerializeField] private Sprite lightning;
    [SerializeField] private Sprite fire;
    [SerializeField] private Sprite empty;

    private void Start()
    {
        cardData = GetComponentInParent<CardData>();
        text = transform.GetChild(0).GetComponent<Text>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (cardData.freezeTurns > 0)
        {
            image.sprite = snowflake;
            text.text = "" + cardData.freezeTurns;
        }
        else if (cardData.paraTurns > 0)
        {
            image.sprite = lightning;
            text.text = "" + cardData.paraTurns;
        }
        else if (cardData.scorchTurns > 0)
        {
            image.sprite = fire;
            text.text = "" + cardData.scorchTurns;
        }
        else
        {
            image.sprite = empty;
            text.text = "";
        }
    }
}
