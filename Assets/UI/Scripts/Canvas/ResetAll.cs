using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetAll : MonoBehaviour
{
    private AllMain allMain;
    private GameObject[] buttons;
    private Image[] hand;
    private StartAndOptions startAndOptions;
    private Encounter encounter;

    public static bool isResetting;

    private void Start()
    {
        allMain = GetComponent<AllMain>();
        buttons = allMain.buttonsGameObjects;
        hand = allMain.hand;
        startAndOptions = GetComponent<StartAndOptions>();
        encounter = GetComponent<Encounter>();
    }

    //disable if debug testing
    public void Reset()
    {
        //set to false in DayEnd
        isResetting = true;

        AllMain.CoinCount = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = allMain.defaultSprite;
            buttons[i].GetComponent<Button>().enabled = true;
        }
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].sprite = allMain.empty;
        }

        //resets artifacts
        for (int i = 0; i < allMain.artifactActivated.Length; i++)
        {
            allMain.artifactActivated[i] = false;
        }
        for (int i = 0; i < allMain.repeatCount.Length; i++)
        {
            allMain.repeatCount[i] = 0;
        }

        //zero list of artifacts
        for (int i = 0; i < allMain.selectedList.Length; i++)
        {
            allMain.selectedList[i].GetComponent<Image>().sprite = allMain.empty;
        }

        //zero encounter and encounter info like in DayEnd
        Encounter.EncountersLeft = 0;
        for (int i = encounter.encounterType.Count - 1; i >= 0; i--)
        {
            encounter.encounterType.RemoveAt(0);
        }
        for (int i = encounter.weight.Count - 1; i >= 0; i--)
        {
            encounter.weight.RemoveAt(0);
        }

        //reset affecting
        foreach (GameObject button in buttons)
        {
            CardPlay cardPlay = button.GetComponent<CardPlay>();
            for (int i = 0; i < cardPlay.affecting.Length; i++)
            {
                cardPlay.affecting[i] = false;
            }
        }

        NetWorthCheck.NetAdjustment = 0;
        DayEnd.DayInWeek = 1;
        SetArrow.Week = 0;
        SetDisaster.EventActive = false;
        SetArrow.EventName = "";
        GetComponent<ShowListButton>().enabled = true;
        SelectEncounterCard.index = 0;
        AllMain.rareChanceMultiplier = 1;

        StartAndOptions.InGame = false;
        startAndOptions.game.SetActive(false);
        startAndOptions.titleScreen.SetActive(true);
    }
}
