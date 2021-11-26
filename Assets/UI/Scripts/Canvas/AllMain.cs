using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllMain : MonoBehaviour
{
    public GameObject allEncounterCards;
    public GameObject encounterCard1;
    public GameObject encounterCard2;
    public GameObject encounterCard3;

    public Image[] hand;

    public GameObject[] buttonsGameObjects;

    public Sprite[] common;
    public Sprite[] uncommon;
    public Sprite[] rare;
    public Sprite[] veryRare;
    public Sprite[] token;
    public Sprite[] secret;
    public Sprite[] all;

    public List<Sprite> commonArtifacts;
    public List<Sprite> uncommonArtifacts;
    public List<Sprite> rareArtifacts;
    public List<Sprite> veryRareArtifacts;
    public int[] repeatCount;

    public static int artifactCount;

    public bool[] artifactActivated;

    public Sprite[] borders;

    public static int CoinCount;

    public static float rareChanceMultiplier = 1;

    public Sprite defaultSprite;
    public Sprite empty;
    //used for null catching
    public Image defaultImage;

    public static ResetAll resetAll;

    public GameObject[] selectedList;

    private void Awake()
    {
        resetAll = GetComponent<ResetAll>();
        repeatCount = new int[4];
        artifactCount = commonArtifacts.Count + uncommonArtifacts.Count + rareArtifacts.Count + veryRareArtifacts.Count;
        artifactActivated = new bool[artifactCount];
        AssignAll();
        selectedList = new GameObject[artifactCount];
    }

    public bool HandFull()
    {
        return HandSize() == hand.Length;
    }

    public int CardsOnBoard()
    {
        int count = 0;
        foreach (GameObject button in buttonsGameObjects)
        {
            if (button.GetComponent<Image>().sprite != defaultSprite)
            {
                count++;
            }
        }
        return count;
    }

    public int HandSize()
    {
        int count = 0;
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i].sprite != empty)
            {
                count++;
            }
        }
        return count;
    }

    public Sprite FindCard(string cardName)
    {
        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].name.Equals(cardName))
            {
                return all[i];
            }
        }
        return defaultSprite;
    }

    //completes all sprite array
    private void AssignAll()
    {
        int sum = common.Length + uncommon.Length + rare.Length + veryRare.Length + token.Length + secret.Length
            + artifactCount;
        all = new Sprite[sum];

        int index = 0;
        int baseIndex = 0;
        while (baseIndex < common.Length)
        {
            all[index] = common[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < uncommon.Length)
        {
            all[index] = uncommon[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < rare.Length)
        {
            all[index] = rare[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < veryRare.Length)
        {
            all[index] = veryRare[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < secret.Length)
        {
            all[index] = secret[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < commonArtifacts.Count)
        {
            all[index] = commonArtifacts[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < uncommonArtifacts.Count)
        {
            all[index] = uncommonArtifacts[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < rareArtifacts.Count)
        {
            all[index] = rareArtifacts[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < veryRareArtifacts.Count)
        {
            all[index] = veryRareArtifacts[baseIndex];
            index++;
            baseIndex++;
        }
        baseIndex = 0;
        while (baseIndex < token.Length)
        {
            all[index] = token[baseIndex];
            index++;
            baseIndex++;
        }
    }

    private void Update()
    {
        UpdateValue();
    }

    //updates rarity weight and payment (must update manually)
    public static void UpdateValue()
    {
        //see spreadsheet for percentage for each week
        switch (SetArrow.Week)
        {
            //zero because StartGame() changes week value to 1 after adding encounters
            case 0:
            case 1:
                Encounter.baseRarityWeight = new float[] { 97, 3, 0, 0 };
                Pay.payment = 8;
                break;
            case 2:
                Encounter.baseRarityWeight = new float[] { 80, 20, 2 * rareChanceMultiplier, 0 };
                Pay.payment = 40;
                break;
            case 3:
                Encounter.baseRarityWeight = new float[] { 80, 35, 7 * rareChanceMultiplier, 0.5f * rareChanceMultiplier};
                Pay.payment = 100;
                break;
            case 4:
                Encounter.baseRarityWeight = new float[] { 80, 50, 15 * rareChanceMultiplier, 1 * rareChanceMultiplier};
                Pay.payment = 250;
                break;
            case 5:
                Encounter.baseRarityWeight = new float[] { 80, 65, 25 * rareChanceMultiplier, 2 * rareChanceMultiplier};
                Pay.payment = 400;
                break;
            case 6:
                Encounter.baseRarityWeight = new float[] { 75, 80, 35 * rareChanceMultiplier, 3 * rareChanceMultiplier};
                Pay.payment = 600;
                break;
            case 7:
                Encounter.baseRarityWeight = new float[] { 75, 80, 35 * rareChanceMultiplier, 3 * rareChanceMultiplier };
                Pay.payment = 850;
                break;
            default:
                Encounter.baseRarityWeight = new float[] { 75, 80, 35 * rareChanceMultiplier, 3 * rareChanceMultiplier };
                Pay.payment = 250 * (SetArrow.Week - 4);
                break;
        }
    }

    public void AddCard(Sprite k)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i].sprite == empty)
            {
                hand[i].sprite = k;
                return;
            }
        }
    }

    public Sprite RandomCommon()
    {
        return common[Random.Range(0, common.Length)];
    }

    public Sprite RandomUncommon()
    {
        return uncommon[Random.Range(0, uncommon.Length)];
    }

    public Sprite RandomRare()
    {
        return rare[Random.Range(0, rare.Length)];
    }
}
