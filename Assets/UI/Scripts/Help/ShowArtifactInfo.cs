using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowArtifactInfo : MonoBehaviour
{
    private AllMain allMain;
    private Image image;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (image.sprite == allMain.empty)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
