using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisasterInfoText : MonoBehaviour
{
    private Text text;
    private SetDisaster setDisaster;

    private void Start()
    {
        text = GetComponent<Text>();
        setDisaster = GetComponentInParent<SetDisaster>();
    }

    private void Update()
    {
        text.text = setDisaster.eventInfo;
    }
}
