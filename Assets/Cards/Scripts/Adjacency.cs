using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjacency : MonoBehaviour
{
    private AllMain allMain;
    private GameObject[] buttons;

    private RectTransform rt;
    private float width;

    public bool[] adjacent;
    //adjacent with corners
    public bool[] adjacentWC;

    public static bool[] perimeter = { true, true, true, true, true,
        true, false, false, false, true,
        true, false, false, false, true,
        true, false, false, false, true,
        true, true, true, true, true };

    public static bool[] corner = { true, false, false, false, true,
        false, false, false, false, false,
        false, false, false, false, false,
        false, false, false, false, false,
        true, false, false, false, true };

    private void Awake()
    {
        allMain = GetComponentInParent<AllMain>();
        buttons = allMain.buttonsGameObjects;

        rt = GetComponent<RectTransform>();
        width = rt.rect.width;

        adjacent = new bool[buttons.Length];
        adjacentWC = new bool[buttons.Length];

        for(int i = 0; i < buttons.Length; i++)
        {
            adjacent[i] = Vector3.Distance(transform.localPosition, buttons[i].transform.localPosition) <= GetComponent<CircleCollider2D>().radius + buttons[i].GetComponent<CircleCollider2D>().radius && gameObject != buttons[i];
            adjacentWC[i] = Vector3.Distance(transform.localPosition, buttons[i].transform.localPosition) <= transform.Find("AdjacencyCollider").GetComponent<CircleCollider2D>().radius + buttons[i].transform.Find("AdjacencyCollider").GetComponent<CircleCollider2D>().radius && gameObject != buttons[i];
        }
    }
}
