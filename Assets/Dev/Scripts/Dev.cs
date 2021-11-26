using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dev : MonoBehaviour
{
    private KeyCode[] konami;
    private int index;

    private GameObject devButtons;

    private void Start()
    {
        index = 0;
        devButtons = GetComponentInParent<StartAndOptions>().devButtons;
        konami = new KeyCode[] { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow,
            KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A };
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(konami[index]))
            {
                index++;
            }
            else
            {
                index = 0;
            }

            if(index == konami.Length)
            {
                Debug.Log("yay");
                devButtons.SetActive(true);
                index = 0;
            }
        }
    }
}
