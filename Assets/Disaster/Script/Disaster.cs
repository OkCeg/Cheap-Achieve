using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Disaster : MonoBehaviour
{
    private AllMain allMain;
    private GameObject[] buttons;

    private void Start()
    {
        allMain = GetComponent<AllMain>();
        buttons = allMain.buttonsGameObjects;
    }

    public void Blizzard()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<CardPlay>().ApplyStatus("freeze", 6);
        }
    }

    public void LightningStorm()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<CardPlay>().ApplyStatus("paralysis", 6);
        }
    }

    public void Volcano()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<CardPlay>().ApplyStatus("scorch", 6);
        }

        int count = 0;
        int[] list = buttons[0].GetComponent<CardPlay>().GenerateRandomListOf25();

        for (int i = 0; i < 25; i++)
        {
            if (count < 3 && buttons[list[i]].GetComponent<Destroy>().DestroyObject())
            {
                count++;
            }
        }
    }
}
