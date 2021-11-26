using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInfo : MonoBehaviour
{
    public static bool showEncounterHelp = true;

    public void EncounterSwitch()
    {
        showEncounterHelp = !showEncounterHelp;
    }
}
