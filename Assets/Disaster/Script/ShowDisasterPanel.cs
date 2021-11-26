using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDisasterPanel : MonoBehaviour
{
    private StartAndOptions sao;
    private GameObject disasterPanel;

    private void Start()
    {
        sao = GetComponentInParent<StartAndOptions>();
        disasterPanel = sao.disasterPanel;
    }

    public void Show()
    {
        disasterPanel.SetActive(true);
    }

    public void Hide()
    {
        disasterPanel.SetActive(false);
    }
}
