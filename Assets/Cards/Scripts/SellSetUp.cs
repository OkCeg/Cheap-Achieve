using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellSetUp : MonoBehaviour
{
    public static bool sellSelected;
    private AllMain allMain;
    private GameObject[] buttons;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        buttons = allMain.buttonsGameObjects;
    }

    public void SellSelected()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            Button buttonComponent = buttons[i].GetComponent<Button>();
            buttonComponent.enabled = true;
        }
        //Debug.Log("yep sell");
        sellSelected = true;
    }

    public void SellDeselected()
    {
        //Debug.Log("nop sell");
        StartCoroutine(WaitForDeselect());
    }

    //wait couple frames so that deselecting and button click can register properly
    private IEnumerator WaitForDeselect()
    {
        yield return new WaitForSeconds(0.2f);
        sellSelected = false;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<Image>().sprite != allMain.defaultSprite)
            {
                buttons[i].GetComponent<Button>().enabled = false;
            }
        }
        StopCoroutine(WaitForDeselect());
    }
}
