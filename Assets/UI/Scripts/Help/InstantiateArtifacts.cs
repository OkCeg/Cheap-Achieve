using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateArtifacts : MonoBehaviour
{
    //set in inspector
    [SerializeField] private GameObject artifact;
    [SerializeField] private GameObject parent;

    private Vector3 initial;
    private AllMain allMain;

    private int left = 0;
    private int down = 0;

    private void Start()
    {
        parent.SetActive(true);

        allMain = GetComponent<AllMain>();
        initial = new Vector3(-650, 250);

        for (int i = 0; i < AllMain.artifactCount; i++)
        {
            if (initial.x + left * 250 >= Screen.width / 2)
            {
                down++;
                left = 0;
            }
            allMain.selectedList[i] = Instantiate(artifact, new Vector3(initial.x + left * 250, initial.y - down * 425), Quaternion.identity);
            allMain.selectedList[i].transform.SetParent(parent.transform, false);
            allMain.selectedList[i].transform.GetChild(1).gameObject.SetActive(false);
            left++;
        }

        parent.SetActive(false);
    }
}
