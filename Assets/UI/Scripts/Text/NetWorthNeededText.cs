using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorthNeededText : MonoBehaviour
{
    private Text netWorthNeededText;

    private void Start()
    {
        netWorthNeededText = GetComponent<Text>();
    }

    private void Update()
    {
        netWorthNeededText.text = "Net Worth Requirement: " + (NetWorthCheck.NetWorthNeeded - NetWorthCheck.NetAdjustment);
    }
}
