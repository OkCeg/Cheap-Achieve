using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandInfo : MonoBehaviour
{
    //set in inspector
    [SerializeField] private GameObject cardInfo;

    private PlayHand playHand;
    private Image image;
    private AllMain allMain;

    private NameText nameText;
    private CardInfoText ciText;
    private TribeText tribeText;
    private BaseGenText bgText;
    private BaseSellText bsText;

    private void Awake()
    {
        playHand = GetComponent<PlayHand>();
        image = GetComponent<Image>();
        allMain = GetComponentInParent<AllMain>();

        nameText = GetComponentInChildren<NameText>();
        ciText = GetComponentInChildren<CardInfoText>();
        tribeText = GetComponentInChildren<TribeText>();
        bgText = GetComponentInChildren<BaseGenText>();
        bsText = GetComponentInChildren<BaseSellText>();
    }

    private void Update()
    {
        if (playHand.isSelected && image.sprite != allMain.empty)
        {
            nameText.UpdateText();
            ciText.UpdateText();
            tribeText.UpdateText();
            bgText.UpdateText();
            bsText.UpdateText();

            cardInfo.SetActive(true);
        }
        else
        {
            cardInfo.SetActive(false);
        }
    }
}
