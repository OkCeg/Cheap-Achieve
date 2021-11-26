using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSellText : MonoBehaviour
{
    //set in inspector
    [SerializeField] private GameObject textObject;
    [SerializeField] private GameObject symbol;

    private CardData cardData;
    private Text text;

    private void Awake()
    {
        cardData = GetComponent<CardData>();
        text = textObject.GetComponent<Text>();
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (cardData.tribe != "Artifact")
        {
            text.text = "" + cardData.baseSP;
            symbol.SetActive(true);
            textObject.SetActive(true);
        }
        else
        {
            symbol.SetActive(false);
            textObject.SetActive(false);
        }
    }
}
