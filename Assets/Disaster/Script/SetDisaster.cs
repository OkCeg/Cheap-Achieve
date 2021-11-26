using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDisaster : MonoBehaviour
{
    //set in inspector
    [SerializeField] private GameObject eventObject;
    [SerializeField] private GameObject newEventObject;
    [SerializeField] private Sprite blizzard;
    [SerializeField] private Sprite lightningstorm;
    [SerializeField] private Sprite volcano;

    private AllMain allMain;

    private Image eventImage;

    private Disaster disaster;
    public string eventInfo;

    public static bool EventActive;

    private void Start()
    {
        allMain = GetComponent<AllMain>();
        disaster = GetComponent<Disaster>();
        eventImage = eventObject.GetComponent<Image>();
    }

    public void TriggerEvent()
    {
        switch (SetArrow.EventName)
        {
            case "Blizzard":
                disaster.Blizzard();
                break;
            case "LightningStorm":
                disaster.LightningStorm();
                break;
            case "Volcano":
                disaster.Volcano();
                break;
            default:
                break;
        }
        SetArrow.EventName = "";
    }

    public void SetEvent()
    {
        switch (SetArrow.EventName)
        {
            case "Blizzard":
                EventActive = true;
                eventImage.sprite = blizzard;
                eventInfo = "Freezes all cards for 6 days at the end of the day.";
                break;
            case "LightningStorm":
                EventActive = true;
                eventImage.sprite = lightningstorm;
                eventInfo = "Paralyzes all cards for 6 days at the end of the day.";
                break;
            case "Volcano":
                EventActive = true;
                eventImage.sprite = volcano;
                eventInfo = "Scorches all cards for 6 days and destroys 3 random cards at the end of the day.";
                break;
            default:
                EventActive = false;
                eventImage.sprite = allMain.empty;
                eventInfo = "welcome! care for a talk?";
                break;
        }
    }

    private void Update()
    {
        if (EventActive)
        {
            newEventObject.SetActive(true);
        }
        else
        {
            newEventObject.SetActive(false);
        }
    }
}
