using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellCard : MonoBehaviour
{
    private AllMain allMain;
    private CardData cardData;
    private CardPlay cardPlay;
    private Image image;
    private Button button;
    private bool canSell;
    private bool[] affecting;
    private GameObject[] allButtons;
    private bool[] artifactActivated;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        cardData = GetComponent<CardData>();
        cardPlay = GetComponent<CardPlay>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        //adds the sell script to the button at start
        button.onClick.AddListener(Sell);
        allButtons = allMain.buttonsGameObjects;
        affecting = cardPlay.affecting;
        artifactActivated = allMain.artifactActivated;
    }

    public void Sell()
    {
        if(SellSetUp.sellSelected)
        {
            //Debug.Log("selling");
            ReverseAura();
            AllMain.CoinCount += cardData.sellPrice;
            image.sprite = allMain.defaultSprite;
            //for situations when Aura() takes priority over CardData's Update()
            cardData.UpdateData();
        }
    }

    //reverses Aura(), also for Destroy
    public void ReverseAura()
    {
        string find = image.sprite.name;

        switch (find)
        {
            case "Flag of Utility":
                for (int i = 0; i < allButtons.Length; i++)
                {
                    CardData cardData = allButtons[i].GetComponent<CardData>();

                    if (affecting[i])
                    {
                        cardData.cpdAdjustment -= 1;
                        cardData.spAdjustment -= 1;
                        affecting[i] = false;
                    }
                }
                break;
            case "Flag of Strength":
                for (int i = 0; i < allButtons.Length; i++)
                {
                    CardData cardData = allButtons[i].GetComponent<CardData>();

                    if (affecting[i])
                    {
                        cardData.cpdAdjustment -= 2;
                        cardData.spAdjustment -= 1;
                        affecting[i] = false;
                    }
                }
                break;
            case "Flag of Bravery":
                for (int i = 0; i < allButtons.Length; i++)
                {
                    CardData cardData = allButtons[i].GetComponent<CardData>();

                    if (affecting[i])
                    {
                        cardData.cpdAdjustment -= 2;
                        cardData.spAdjustment -= 2;
                        cardData.freezeImmune = false;
                        cardData.paraImmune = false;
                        affecting[i] = false;
                    }
                }
                break;
            case "Fool's Gold":
                AllMain.rareChanceMultiplier /= 1.3f;
                break;
            case "Gold Bar":
                AllMain.rareChanceMultiplier /= 4f;
                break;
            case "Concoction of Luck":
                AllMain.rareChanceMultiplier /= 2.5f;
                break;
            case "Ladybug":
                AllMain.rareChanceMultiplier /= 1.05f;
                break;
            case "Avocado":
                for (int i = 0; i < allButtons.Length; i++)
                {
                    CardData cardData = allButtons[i].GetComponent<CardData>();

                    if (affecting[i])
                    {
                        cardData.spMultiplier /= 3;
                        affecting[i] = false;
                    }
                }
                break;
            case "Flag of Value":
                for (int i = 0; i < allButtons.Length; i++)
                {
                    CardData cardData = allButtons[i].GetComponent<CardData>();

                    if (affecting[i])
                    {
                        cardData.spMultiplier /= 2;
                        affecting[i] = false;
                    }
                }
                break;
            case "Shuriken":
                int count = 0;
                for (int i = 0; i < allButtons.Length; i++)
                {
                    if (affecting[i])
                    {
                        count++;
                        allButtons[i].GetComponent<CardData>().spAdjustment--;

                        //Katana check
                        if (artifactActivated[13])
                        {
                            allButtons[i].GetComponent<CardData>().spAdjustment--;
                        }

                        affecting[i] = false;
                    }
                }
                if (count % 2 == 0)
                {
                    for (int i = 0; i < allButtons.Length; i++)
                    {
                        if (allButtons[i].GetComponent<Image>().sprite.name.Equals("Shuriken"))
                        {
                            allButtons[i].GetComponent<CardData>().cpdAdjustment--;
                        }
                    }
                }
                break;
            default:
                break;
        }

        //cards that affect the sold card will no longer affect the card
        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].GetComponent<CardPlay>().affecting[cardPlay.FindButtonNumber()] = false;
        }
    }
}
