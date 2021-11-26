using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private AllMain allMain;
    private GameObject[] buttons;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        buttons = allMain.buttonsGameObjects;
    }

    //ensures all Updates are in order
    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<CardPlay>().MyUpdate();
        }
    }
}
