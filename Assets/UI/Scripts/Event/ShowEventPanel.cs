using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEventPanel : MonoBehaviour
{
    //set in inspector
    [SerializeField] private GameObject panel;

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void DisablePanel()
    {
        panel.SetActive(false);
    }
}
