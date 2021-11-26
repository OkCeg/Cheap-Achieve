using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArrow : MonoBehaviour
{
    private Vector3 startPos;
    //set both in inspector
    [SerializeField] private bool[] hasEvent;
    [SerializeField] private GameObject[] eventSprites;

    //for randomizing event days
    private int count = 0;

    //set to 1 at start
    [SerializeField] public static int Week = 0;
    public static string EventName = "";
    private string[] eventChoose = {"Blizzard", "LightningStorm", "Volcano"};

    private void Awake()
    {
        startPos = transform.localPosition;
    }

    public void Set()
    {
        if (hasEvent[DayEnd.DayInWeek - 1])
        {
            hasEvent[DayEnd.DayInWeek - 1] = false;
            InstateEvent();
        }
        if (DayEnd.DayInWeek != 1)
        {
            transform.localPosition += new Vector3(150, 0, 0);
        }
        else
        //encounters added in DayEnd
        {
            transform.localPosition = startPos;
            Week++;

            //not in UpdateValue() because net worth should be changed after check
            switch (Week)
            {
                case 1:
                    NetWorthCheck.NetWorthNeeded = 15;
                    break;
                case 2:
                    NetWorthCheck.NetWorthNeeded = 36;
                    break;
                case 3:
                    NetWorthCheck.NetWorthNeeded = 60;
                    break;
                case 4:
                    NetWorthCheck.NetWorthNeeded = 90;
                    break;
                case 5:
                    NetWorthCheck.NetWorthNeeded = 135;
                    break;
                default:
                    NetWorthCheck.NetWorthNeeded = 100 * (Week - 4);
                    break;
            }

            RandomizeEvents();
        }
        SetEvents();
    }

    private void SetEvents()
    {
        for (int i = 0; i < hasEvent.Length; i++)
        {
            if (hasEvent[i])
            {
                eventSprites[i].SetActive(true);
            }
            else
            {
                eventSprites[i].SetActive(false);
            }
        }
    }

    private void InstateEvent()
    {
        EventName = eventChoose[Random.Range(0, eventChoose.Length)];
    }
    //random events every week (2 events; favored towards the end of week)
    private void RandomizeEvents()
    {
        for (int i = hasEvent.Length - 1; i >= 3; i--)
        {
            if (!hasEvent[i] && count < 2 && Random.value < 0.5f)
            {
                hasEvent[i] = true;
                count++;
            }
        }

        if (count < 2)
        {
            RandomizeEvents();
        }
        else
        {
            count = 0;
        }            
    }
}
