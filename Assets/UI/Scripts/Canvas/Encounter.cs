using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    public GameObject allEncounterCards;
    private AllMain allMain;
    private StartAndOptions sao;

    private Image image1;
    private Image image2;
    private Image image3;

    private Sprite[] commonCards;
    private Sprite[] uncommonCards;
    private Sprite[] rareCards;
    private Sprite[] veryRareCards;

    private List<Sprite> commonArtifacts;
    private List<Sprite> uncommonArtifacts;
    private List<Sprite> rareArtifacts;
    private List<Sprite> veryRareArtifacts;

    //min weight (see SetPayAndWeight)
    public static float[] baseRarityWeight = { 9, 1, 0, 0 };
    private float commonWeight;
    private float uncommonWeight;
    private float rareWeight;
    private float veryRareWeight;

    public static int EncountersLeft;

    public List<float> weight = new List<float>();
    public List<string> encounterType = new List<string>();

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        sao = GetComponentInParent<StartAndOptions>();

        allEncounterCards = allMain.allEncounterCards;
        image1 = allMain.encounterCard1.GetComponent<Image>();
        image2 = allMain.encounterCard2.GetComponent<Image>();
        image3 = allMain.encounterCard3.GetComponent<Image>();

        commonCards = allMain.common;
        uncommonCards = allMain.uncommon;
        rareCards = allMain.rare;
        veryRareCards = allMain.veryRare;

        commonArtifacts = allMain.commonArtifacts;
        uncommonArtifacts = allMain.uncommonArtifacts;
        rareArtifacts = allMain.rareArtifacts;
        veryRareArtifacts = allMain.veryRareArtifacts;
    }

    public void AddEncounter(float common, float uncommon, float rare, float veryrare)
    {
        weight.Add(common);
        weight.Add(uncommon);
        weight.Add(rare);
        weight.Add(veryrare);
        encounterType.Add("card");
        EncountersLeft++;
    }

    public void AddArtifact(float common, float uncommon, float rare, float veryrare)
    {
        weight.Add(common);
        weight.Add(uncommon);
        weight.Add(rare);
        weight.Add(veryrare);
        encounterType.Add("artifact");
        EncountersLeft++;
    }

    private void EncounterSet()
    {
        commonWeight = weight[0];
        uncommonWeight = weight[1];
        rareWeight = weight[2];
        veryRareWeight = weight[3];

        for(int i = 0; i < 4; i++)
        {
            weight.RemoveAt(0);
        }

        allEncounterCards.SetActive(true);

        if (encounterType[0].Equals("card"))
        {
            image1.sprite = Roll(commonWeight, uncommonWeight, rareWeight, veryRareWeight);
            image2.sprite = Roll(commonWeight, uncommonWeight, rareWeight, veryRareWeight);
            image3.sprite = Roll(commonWeight, uncommonWeight, rareWeight, veryRareWeight);

            for (int i = 0; i < 3; i++)
            {
                if (image2.sprite == image1.sprite)
                {
                    image2.sprite = Roll(commonWeight, uncommonWeight, rareWeight, veryRareWeight);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (image3.sprite == image2.sprite || image3.sprite == image1.sprite)
                {
                    image3.sprite = Roll(commonWeight, uncommonWeight, rareWeight, veryRareWeight);
                }
            }
        }
        else
        {
            image1.sprite = RollArtifact(commonWeight, uncommonWeight, rareWeight, veryRareWeight);
            image2.sprite = RollArtifact(commonWeight, uncommonWeight, rareWeight, veryRareWeight);
            image3.sprite = RollArtifact(commonWeight, uncommonWeight, rareWeight, veryRareWeight);

            for (int i = 0; i < 3; i++)
            {
                if (image2.sprite == image1.sprite)
                {
                    image2.sprite = RollArtifact(commonWeight, uncommonWeight, rareWeight, veryRareWeight);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (image3.sprite == image2.sprite || image3.sprite == image1.sprite)
                {
                    image3.sprite = RollArtifact(commonWeight, uncommonWeight, rareWeight, veryRareWeight);
                }
            }
        }
        
        encounterType.RemoveAt(0);
    }

    private Sprite Roll(float common, float uncommon, float rare, float veryrare)
    {
        float[] rarityWeight = { common, uncommon, rare, veryrare };

        float sum = 0;
        for (int i = 0; i < rarityWeight.Length; i++)
        {
            sum += rarityWeight[i];
        }

        float num = Random.Range(0, sum);

        if (num < rarityWeight[0])
        {
            return commonCards[(int)Random.Range(0, commonCards.Length)];
        }
        else if (num < rarityWeight[0] + rarityWeight[1])
        {
            return uncommonCards[(int)Random.Range(0, uncommonCards.Length)];
        }
        else if (num < rarityWeight[0] + rarityWeight[1] + rarityWeight[2])
        {
            return rareCards[(int)Random.Range(0, rareCards.Length)];
        }
        else
        {
            return veryRareCards[(int)Random.Range(0, veryRareCards.Length)];
        }
    }

    private Sprite RollArtifact(float common, float uncommon, float rare, float veryrare)
    {
        float[] rarityWeight = { common, uncommon, rare, veryrare };

        float sum = 0;
        for (int i = 0; i < rarityWeight.Length; i++)
        {
            sum += rarityWeight[i];
        }

        float num = Random.Range(0, sum);

        if (num < rarityWeight[0])
        {
            return commonArtifacts[Random.Range(0, commonArtifacts.Count)];
        }
        else if (num < rarityWeight[0] + rarityWeight[1])
        {
            return uncommonArtifacts[(int)Random.Range(0, uncommonArtifacts.Count)];
        }
        else if (num < rarityWeight[0] + rarityWeight[1] + rarityWeight[2])
        {
            return rareArtifacts[(int)Random.Range(0, rareArtifacts.Count)];
        }
        else
        {
            return veryRareArtifacts[(int)Random.Range(0, veryRareArtifacts.Count)];
        }
    }

    private void Update()
    {
        if(EncountersLeft > 0 && !allEncounterCards.activeInHierarchy && sao.boardScreen.activeInHierarchy)
        {
            EncounterSet();
            UpdateText();

            allEncounterCards.SetActive(true);
            EncountersLeft--;
        }
    }

    public void AddEncounter()
    {
        AddEncounter(baseRarityWeight[0], baseRarityWeight[1], baseRarityWeight[2], baseRarityWeight[3]);
    }

    public void AddArtifact()
    {
        AddArtifact(baseRarityWeight[0], baseRarityWeight[1], baseRarityWeight[2], baseRarityWeight[3]);
    }

    //not sure if this works or not
    private void UpdateText()
    {
        allEncounterCards.GetComponentInChildren<NameText>().UpdateText();
        allEncounterCards.GetComponentInChildren<CardInfoText>().UpdateText();
        allEncounterCards.GetComponentInChildren<TribeText>().UpdateText();
        allEncounterCards.GetComponentInChildren<BaseGenText>().UpdateText();
        allEncounterCards.GetComponentInChildren<BaseSellText>().UpdateText();
    }
}
