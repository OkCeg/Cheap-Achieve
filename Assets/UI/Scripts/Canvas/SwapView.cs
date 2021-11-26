using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapView : MonoBehaviour
{
    public GameObject game;
    private bool isBoard = true;

    //may want SmoothDamp later
    public void Swap()
    {
        if (isBoard)
        {
            game.transform.position = Vector3.Lerp(game.transform.position, new Vector3(game.transform.position.x, game.transform.position.y - Screen.height), 1f);
            isBoard = false;
        }
        else
        {
            game.transform.position = Vector3.Lerp(game.transform.position, new Vector3(game.transform.position.x, game.transform.position.y + Screen.height), 1f);
            isBoard = true;
        }
    }
}
