using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndOptions : MonoBehaviour
{
    //set in inspector
    public GameObject titleScreen;
    public GameObject optionsMenu;
    public GameObject howToPlay;
    public GameObject game;
    public GameObject disasterPanel;
    public GameObject devButtons;
    public GameObject boardScreen;
    public GameObject dayScreen;
    public GameObject warning;

    private AllMain allMain;
    private Encounter enc;

    public static bool InGame;

    private void Awake()
    {
        titleScreen.SetActive(true);
        optionsMenu.SetActive(false);
        howToPlay.SetActive(false);
        game.SetActive(false);
        disasterPanel.SetActive(false);
        boardScreen.SetActive(true);
        dayScreen.SetActive(true);
        warning.SetActive(false);

        //comment to have dev access
        devButtons.SetActive(false);

        allMain = GetComponent<AllMain>();
        enc = GetComponent<Encounter>();
    }

    //displays options screen
    public void Options()
    {
        optionsMenu.SetActive(true);
        if (InGame)
        {
            boardScreen.SetActive(false);
        }
        else
        {
            titleScreen.SetActive(false);
        }
    }

    //displays how to play
    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        if (InGame)
        {
            boardScreen.SetActive(false);
        }
        else
        {
            titleScreen.SetActive(false);
        }
    }

    //goes back to title screen
    public void Back()
    {
        optionsMenu.SetActive(false);
        howToPlay.SetActive(false);
        if (InGame)
        {
            boardScreen.SetActive(true);
        }
        else
        {
            titleScreen.SetActive(true);
        }
    }

    //starts game
    public void StartGame()
    {
        InGame = true;

        enc.allEncounterCards.SetActive(false);

        AllMain.UpdateValue();
        enc.AddEncounter();
        enc.AddEncounter();
        enc.AddEncounter();

        game.SetActive(true);
        titleScreen.SetActive(false);

        GetComponentInChildren<SetArrow>().Set();
    }
}
