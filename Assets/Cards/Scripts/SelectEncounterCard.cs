using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectEncounterCard : MonoBehaviour
{
    private AllMain allMain;
    private Encounter encounter;
    private GameObject allEncounterCards;
    private Image image;
    private CardData cardData;
    private Image[] hand;
    private GameObject[] buttons;

    private List<Sprite> commonArtifacts;
    private List<Sprite> uncommonArtifacts;
    private List<Sprite> rareArtifacts;
    private List<Sprite> veryRareArtifacts;

    private GameObject[] selectedList;

    //for artifact list
    public static int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        encounter = GetComponentInParent<Encounter>();
        allEncounterCards = allMain.allEncounterCards;
        image = GetComponent<Image>();
        cardData = GetComponent<CardData>();
        hand = allMain.hand;
        buttons = allMain.buttonsGameObjects;

        commonArtifacts = allMain.commonArtifacts;
        uncommonArtifacts = allMain.uncommonArtifacts;
        rareArtifacts = allMain.rareArtifacts;
        veryRareArtifacts = allMain.veryRareArtifacts;

        selectedList = allMain.selectedList;
    }

    public void SelectCard()
    {
        if (cardData.tribe.Equals("Artifact"))
        {
            allMain.artifactActivated[cardData.artifactNumber] = true;

            if (!cardData.infoText.Contains("repeatedly"))
            {
                //exception error not thrown for removing objects not in list
                commonArtifacts.Remove(image.sprite);
                uncommonArtifacts.Remove(image.sprite);
                rareArtifacts.Remove(image.sprite);
                veryRareArtifacts.Remove(image.sprite);
            }
            else
            {
                switch (image.sprite.name)
                {
                    case "Brick":
                        allMain.repeatCount[0]++;
                        break;
                    case "Pamphlet":
                        allMain.repeatCount[1]++;
                        break;
                    case "Piggy Bank":
                        allMain.repeatCount[2]++;
                        break;
                    case "Disco Ball":
                        allMain.repeatCount[3]++;
                        break;
                    default:
                        break;
                }                    
            }

            ApplyArtifact(cardData.artifactNumber);
            AddToArtifactList();
            allEncounterCards.SetActive(false);
        }
        //make sure hand is not full
        else if(!allMain.HandFull())
        {
            allMain.AddCard(image.sprite);
            allEncounterCards.SetActive(false);
        }
    }

    //for artifacts that only apply effects once (see full list in CardData)
    private void ApplyArtifact(int k)
    {
        switch(k)
        {
            case 7:
                //Pamphlet
                NetWorthCheck.NetAdjustment += 10;
                break;
            case 13:
                //Katana
                foreach (GameObject button in buttons)
                {
                    if (button.GetComponent<Image>().sprite.name.Equals("Shuriken"))
                    {
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            if (button.GetComponent<CardPlay>().affecting[i])
                            {
                                button.GetComponent<CardData>().spAdjustment++;
                            }
                        }
                    }
                }
                break;
            case 19:
                //Lucky Block
                encounter.AddEncounter(1, 0, 0, 0);
                encounter.AddEncounter(0, 1, 0, 0);
                break;
            default:
                break;
        }
    }

    //for artifact list
    private void AddToArtifactList()
    {
        //checks for repeatability
        if (cardData.infoText.Contains("repeatedly"))
        {
            selectedList[index].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            selectedList[index].transform.GetChild(1).gameObject.SetActive(false);
        }

        if (index < selectedList.Length)
        {
            switch (image.sprite.name)
            {
                case "Brick":
                    if (allMain.repeatCount[0] == 1)
                    {
                        selectedList[index].GetComponent<Image>().sprite = image.sprite;
                        index++;
                    }
                    break;
                case "Pamphlet":
                    if (allMain.repeatCount[1] == 1)
                    {
                        selectedList[index].GetComponent<Image>().sprite = image.sprite;
                        index++;
                    }
                    break;
                case "Piggy Bank":
                    if (allMain.repeatCount[2] == 1)
                    {
                        selectedList[index].GetComponent<Image>().sprite = image.sprite;
                        index++;
                    }
                    break;
                case "Disco Ball":
                    if (allMain.repeatCount[3] == 1)
                    {
                        selectedList[index].GetComponent<Image>().sprite = image.sprite;
                        index++;
                    }
                    break;
                default:
                    selectedList[index].GetComponent<Image>().sprite = image.sprite;
                    index++;
                    break;
            }
        }
    }
}
