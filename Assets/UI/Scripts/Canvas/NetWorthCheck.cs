using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorthCheck : MonoBehaviour
{
    private GameObject[] buttons;
    private int sum = 0;

    public static int NetWorthSum;

    //set in SetArrow
    public static int NetWorthNeeded;
    public static int NetAdjustment;

    private void Start()
    {
        buttons = GetComponentInParent<AllMain>().buttonsGameObjects;
    }

    private void Update()
    {
        NetWorthSum = Sum();
    }

    private int Sum()
    {
        sum = 0;
        for (int i = 0; i < buttons.Length; i++)
        {
            sum += buttons[i].GetComponent<CardData>().sellPrice;
        }
        return sum;
    }

    public static void CheckNetWorth()
    {
        if(NetWorthSum < NetWorthNeeded - NetAdjustment)
        {
            AllMain.resetAll.Reset();
        }
    }
}
