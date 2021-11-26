using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHand : MonoBehaviour
{
    //no static because multiple hand images
    public bool isSelected = false;

    public void Selected()
    {
        //Debug.Log("selected" + gameObject.name);
        isSelected = true;
    }

    //isSelected set to false in CardPlay if ShowBoardInfo.MouseOver is false
    public void Deselected()
    {
        //Debug.Log("deselected" + gameObject.name);
        if (!ShowBoardInfo.MouseOver)
        {
            isSelected = false;
        }
    }
}
