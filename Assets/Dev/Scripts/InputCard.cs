using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCard : MonoBehaviour
{
    private AllMain allMain;
    private Text inputFieldText;

    //set in inspector
    [SerializeField] private GameObject encounterCard;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        inputFieldText = GetComponentInChildren<Text>();
    }

    public void Find()
    {
        Sprite findSprite = allMain.FindCard(inputFieldText.text);
        if (findSprite != allMain.defaultSprite)
        {
            encounterCard.GetComponent<Image>().sprite = findSprite;
        }
    }
}
