using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayEnd : MonoBehaviour
{
    private GameObject[] buttons;
    public static bool IsDayEnd = false;
    public static int DayInWeek = 1;

    private Encounter encounter;
    private SetDisaster setDisaster;
    private AllMain allMain;
    private StartAndOptions sao;

    private bool[] artifactActivated;

    //set in inspector
    [SerializeField] private GameObject arrowParent;
    [SerializeField] private GameObject allEncCards;

    public GameObject warning;

    private void Start()
    {
        allMain = GetComponent<AllMain>();
        buttons = allMain.buttonsGameObjects;
        encounter = GetComponent<Encounter>();
        setDisaster = GetComponent<SetDisaster>();
        sao = GetComponent<StartAndOptions>();

        artifactActivated = allMain.artifactActivated;

        warning = sao.warning;
    }

    public void DayEnded()
    {
        if (SetArrow.EventName.Equals(""))
        {
            DoEndStuff();
        }
        else
        {
            warning.SetActive(true);
        }
    }

    public void DoEndStuff()
    {
        warning.SetActive(false);

        for (int i = 0; i < buttons.Length; i++)
        {
            CardData cardData = buttons[i].GetComponent<CardData>();
            AllMain.CoinCount += cardData.coinsPerDay;

            if (cardData.freezeTurns == 1 || cardData.paraTurns == 1 || cardData.scorchTurns == 1)
            {
                cardData.TriggerCure();
            }
            cardData.freezeTurns--;
            cardData.paraTurns--;
            cardData.scorchTurns--;

            cardData.UpdateData();
        }

        IsDayEnd = true;

        DayInWeek++;
        if (DayInWeek >= 8)
        {
            DayInWeek = 1;
            NetWorthCheck.CheckNetWorth();
        }

        //check to see if resetting is in process
        if (!ResetAll.isResetting)
        {
            //trigger event and check for new event
            setDisaster.TriggerEvent();
            arrowParent.GetComponent<SetArrow>().Set();
            setDisaster.SetEvent();

            //updates rarity value and payment
            AllMain.UpdateValue();

            //adds encounter at the start of each day
            encounter.AddEncounter();
            //Treasure Map check
            if (artifactActivated[12])
            {
                encounter.AddEncounter();
            }

            //for total of 3 encounters at the start of each week
            if (DayInWeek == 1)
            {
                encounter.AddEncounter();
                encounter.AddArtifact();
            }
        }

        ResetAll.isResetting = false;
    }

    public void ProceedNo()
    {
        warning.SetActive(false);
    }

    public void ProceedYes()
    {
        DoEndStuff();
    }
}
