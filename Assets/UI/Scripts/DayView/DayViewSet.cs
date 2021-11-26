using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayViewSet : MonoBehaviour
{
    private void Start()
    {
        transform.position += new Vector3(0, Screen.height, 0);
    }
}
