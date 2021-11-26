using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowListButton : MonoBehaviour
{
    private AllMain allMain;
    private bool[] artifactActivated;

    //set in inspector
    [SerializeField] private GameObject button;

    private void Start()
    {
        allMain = GetComponent<AllMain>();
        artifactActivated = allMain.artifactActivated;
    }

    private void Update()
    {
        if (ArtifactAchieved())
        {
            button.SetActive(true);
            //disables the script once artifact is found (reenables after reset)
            GetComponent<ShowListButton>().enabled = false;
        }
        else
        {
            button.SetActive(false);
        }
    }

    private bool ArtifactAchieved()
    {
        for (int i = 0; i < artifactActivated.Length; i++)
        {
            if (artifactActivated[i])
            {
                return true;
            }
        }

        return false;
    }
}
