using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEncounterInfo : MonoBehaviour
{
    //set in inspector
    [SerializeField] private GameObject cardInfo;

    public void Update()
    {
        if (ToggleInfo.showEncounterHelp)
        {
            cardInfo.SetActive(true);
        }
        else
        {
            cardInfo.SetActive(false);
        }
    }
}
