using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowArtifactMenu : MonoBehaviour
{
    //set in inspector
    [SerializeField] private GameObject artifactMenu;
    [SerializeField] private GameObject boardView;

    public void SwitchToArtifact()
    {
        artifactMenu.SetActive(true);
        boardView.SetActive(false);
    }

    public void SwitchToBoard()
    {
        artifactMenu.SetActive(false);
        boardView.SetActive(true);
    }
}
