using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCountText : MonoBehaviour
{
    private Text coinCountText;

    private void Start()
    {
        coinCountText = GetComponent<Text>();
    }

    private void Update()
    {
        coinCountText.text = "Coins: " + AllMain.CoinCount;
    }
}
