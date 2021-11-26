using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    private AllMain allMain;
    private Image image;
    private Button button;
    private Adjacency adjacency;
    private SellCard sellCard;
    private CardData cardData;

    private GameObject[] buttons;
    private bool[] adjacent;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        adjacency = GetComponent<Adjacency>();
        sellCard = GetComponent<SellCard>();
        cardData = GetComponent<CardData>();

        buttons = allMain.buttonsGameObjects;
        adjacent = adjacency.adjacent;
    }

    public bool DestroyObject()
    {
        if (image.sprite != allMain.defaultSprite && !cardData.destroyImmune)
        {
            sellCard.ReverseAura();
            WhenDestroyed(image.sprite.name);
            TriggerDestroyed();
            image.sprite = allMain.defaultSprite;
            button.enabled = true;

            //update so that card stats go to zero (in case of multiple destroys)
            cardData.UpdateData();
            return true;
        }
        return false;
    }

    private void WhenDestroyed(string name)
    {
        switch (name)
        {
            case "Pinata":
                AllMain.CoinCount += 10;
                break;
            case "Little Rock":
                allMain.AddCard(allMain.RandomCommon());
                break;
            case "Big Rock":
                allMain.AddCard(allMain.RandomUncommon());
                allMain.AddCard(allMain.RandomRare());
                break;
            default:
                break;
        }
    }

    private void TriggerDestroyed()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            CardData cardData = buttons[i].GetComponent<CardData>();
            switch (buttons[i].GetComponent<Image>().sprite.name)
            {
                case "Heart Rake":
                    if (adjacent[i])
                    {
                        cardData.cpdAdjustment += 2;
                        cardData.spAdjustment += 2;
                    }
                    break;
                case "Concoction of Misery":
                    cardData.cpdAdjustment += 4;
                    cardData.spAdjustment += 4;
                    break;
                default:
                    break;
            }
        }
    }
}
