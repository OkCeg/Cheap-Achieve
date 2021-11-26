using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBoardInfo : MonoBehaviour
{
    //set in inspector
    [SerializeField] private GameObject cardInfo;

    //may move to another class
    //for checking if mouse is over button (used in conjunction with PlayHand and CardPlay
    public static bool MouseOver;

    private AllMain allMain;
    private Image image;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        image = GetComponent<Image>();
    }

    public void EnableInfo()
    {
        MouseOver = true;
        if (image.sprite != allMain.defaultSprite)
        {
            cardInfo.SetActive(true);
        }
    }

    public void DisableInfo()
    {
        MouseOver = false;
        cardInfo.SetActive(false);
    }
}
