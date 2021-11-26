using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorthText : MonoBehaviour
{
    private Text netWorthText;

    private void Start()
    {
        netWorthText = GetComponent<Text>();
    }

    private void Update()
    {
        netWorthText.text = "Current Net Worth: " + NetWorthCheck.NetWorthSum;
    }
}
