using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatArtifactText : MonoBehaviour
{
    private AllMain allMain;
    private Image image;
    private Text text;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        image = GetComponentInParent<Image>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        switch (image.sprite.name)
        {
            case "Brick":
                if (allMain.repeatCount[0] > 1)
                {
                    text.text = "" + allMain.repeatCount[0];
                }
                else
                {
                    text.text = "";
                }
                break;
            case "Pamphlet":
                if (allMain.repeatCount[1] > 1)
                {
                    text.text = "" + allMain.repeatCount[1];
                }
                else
                {
                    text.text = "";
                }
                break;
            case "Piggy Bank":
                if (allMain.repeatCount[2] > 1)
                {
                    text.text = "" + allMain.repeatCount[2];
                }
                else
                {
                    text.text = "";
                }
                break;
            case "Disco Ball":
                if (allMain.repeatCount[3] > 1)
                {
                    text.text = "" + allMain.repeatCount[3];
                }
                else
                {
                    text.text = "";
                }
                break;
            default:
                text.text = "";
                break;
        }
    }
}
