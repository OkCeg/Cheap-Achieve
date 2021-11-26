using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TribeText : MonoBehaviour
{
    private CardData cardData;
    private Text text;

    private void Awake()
    {
        cardData = GetComponentInParent<CardData>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = cardData.tribe;
    }
}
