using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSizeText : MonoBehaviour
{
    private Text text;
    private Image[] hand;

    private void Start()
    {
        text = GetComponent<Text>();
        hand = GetComponentInParent<AllMain>().hand;
    }

    private void Update()
    {
        int count = 0;
        for(int i = 0; i < hand.Length; i++)
        {
            if (hand[i].sprite != GetComponentInParent<AllMain>().empty)
            {
                count++;
            }
        }
        text.text = "Hand Size: " + count + "/" + hand.Length;
    }
}
