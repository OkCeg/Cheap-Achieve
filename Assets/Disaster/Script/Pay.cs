using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pay : MonoBehaviour
{
    private StartAndOptions sao;
    private GameObject disasterPanel;

    public static int payment = 30;

    private void Start()
    {
        sao = GetComponentInParent<StartAndOptions>();
        disasterPanel = sao.disasterPanel;
    }

    public void PayForDisaster()
    {
        if (AllMain.CoinCount >= payment)
        {
            AllMain.CoinCount -= payment;
            SetDisaster.EventActive = false;
            disasterPanel.SetActive(false);
            SetArrow.EventName = "";
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
