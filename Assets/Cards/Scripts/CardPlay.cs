using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlay : MonoBehaviour
{
    //empty sprite
    private Image defaultImage;
    //blank
    private Sprite defaultSprite;

    private AllMain allMain;
    private Image[] hand;
    private Image image;
    private Button button;
    private GameObject[] buttons;
    private Adjacency adjacency;
    private bool[] adjacent;
    private bool[] adjacentWC;
    public bool[] affecting;
    private CardData thisCardData;
    private Encounter encounter;
    private bool[] artifactActivated;

    //triggers StartOfDay() and EndOfDay()
    private bool ready = true;
    //triggers artifact
    private static bool artifactReady = true;

    //artifact numbers that trigger at start and end of day, update this for artifacts
    //scuffed system
    private int[] startArtifacts = { 4, 6, 9, 17 };
    private int[] endArtifacts = { 8, 14, 16 };

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        hand = allMain.hand;
        defaultImage = allMain.defaultImage;
        defaultSprite = allMain.defaultSprite;
        image = GetComponent<Image>();
        button = GetComponent<Button>();

        //adds ButtonSelect to the OnClick in Button automatically
        button.onClick.AddListener(ButtonSelect);

        buttons = allMain.buttonsGameObjects;
        adjacency = GetComponent<Adjacency>();
        adjacent = adjacency.adjacent;
        adjacentWC = adjacency.adjacentWC;
        affecting = new bool[buttons.Length];
        thisCardData = GetComponent<CardData>();
        encounter = GetComponentInParent<Encounter>();
        artifactActivated = allMain.artifactActivated;
}

    public void ButtonSelect()
    {
        Image selected = Selected();

        //makes sure to check if selected is a hand
        for (int i = 0; i < hand.Length; i++)
        {
            if (selected == hand[i])
            {
                selected.GetComponent<PlayHand>().isSelected = false;
            }
        }

        //if a card is selected
        if (selected != defaultImage)
        {
            //calculating sprite not necessary because only adjustments are updated
            image.sprite = selected.sprite;

            //for proper WhenPlayed()
            Sprite temp = selected.sprite;

            selected.sprite = allMain.empty;

            WhenPlayed(temp);
            TriggerWhenPlayed();

            button.enabled = false;
        }            
    }

    //only one image can be selected at a time, so find the card that is selected
    private Image Selected()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            if(hand[i].GetComponent<PlayHand>().isSelected)
            {
                return hand[i];
            }
        }
        return defaultImage;
    }

    private void WhenPlayed(Sprite find)
    {
        switch(find.name)
        {
            case "Four":
                for(int i = 0; i < buttons.Length; i++)
                {
                    CardData cardData = buttons[i].GetComponent<CardData>();

                    //gives all other numbers +2 gen and +2 sell
                    if (cardData.tribe.Equals("Number") && buttons[i] != gameObject)
                    {
                        cardData.cpdAdjustment += 2;
                        cardData.spAdjustment += 2;
                    }
                }
                break;
            case "Five":
                for (int i = 0; i < buttons.Length; i++)
                {
                    CardData cardData = buttons[i].GetComponent<CardData>();

                    if (cardData.tribe.Equals("Number") && buttons[i] != gameObject)
                    {
                        cardData.cpdAdjustment -= 1;
                        cardData.spAdjustment += 2;
                    }
                }
                break;
            case "Six":
                for (int i = 0; i < buttons.Length; i++)
                {
                    CardData cardData = buttons[i].GetComponent<CardData>();

                    //gives all other numbers +6 gen and +6 sell
                    if (cardData.tribe.Equals("Number") && buttons[i] != gameObject)
                    {
                        cardData.cpdAdjustment += 6;
                        cardData.spAdjustment += 6;
                    }
                }
                break;
            case "Snowman":
                for (int i = 0; i < buttons.Length; i++)
                {
                    CardData cardData = buttons[i].GetComponent<CardData>();
                    //freezes adjacent cards for 2 days and gives them +2 gen
                    if (adjacent[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                    {
                        buttons[i].GetComponent<CardPlay>().ApplyStatus("freeze", 2);
                        cardData.cpdAdjustment += 2;
                    }
                }
                break;
            case "Voltaic Dart":
                for (int i = 0; i < buttons.Length; i++)
                {
                    CardData cardData = buttons[i].GetComponent<CardData>();
                    //paralyze adjacent cards for 2 days and gives them +5 sell
                    if (adjacent[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                    {
                        buttons[i].GetComponent<CardPlay>().ApplyStatus("paralysis", 2);
                        cardData.spAdjustment += 3;
                    }
                }
                break;
            case "Torch":
                thisCardData.scorchTurns = 3;
                break;
            case "One":
                if (allMain.HandSize() == 0)
                {
                    encounter.AddEncounter(0, 0, 100, 10);
                }
                break;
            case "Crab":
                for (int i = 0; i < buttons.Length; i++)
                {
                    //destroys adjacent cards
                    if (adjacent[i])
                    {
                        if (buttons[i].GetComponent<Destroy>().DestroyObject())
                        {
                            thisCardData.cpdAdjustment++;
                        }
                    }
                }
                break;
            case "Commander Grant":
                for (int i = 0; i < buttons.Length; i++)
                {
                    CardData cardData = buttons[i].GetComponent<CardData>();
                    //gives all flags +1 gen and +2 sell
                    if (cardData.tribe.Equals("Flag"))
                    {
                        cardData.cpdAdjustment += 2;
                        cardData.spAdjustment += 2;
                    }
                }
                break;
            case "Wrench":
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].GetComponent<CardData>().CureStatus();
                }
                break;
            case "Ice Cream Cone":
                for (int i = 0; i < buttons.Length; i++)
                {
                    CardData cardData = buttons[i].GetComponent<CardData>();
                    //freezes adjacent cards for 2 days and gives them +2 gen
                    if (adjacent[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                    {
                        buttons[i].GetComponent<CardPlay>().ApplyStatus("freeze", 1);
                    }
                }
                break;
            case "Friendly Fire":
                thisCardData.scorchTurns = 2;
                break;
            //secrets
            case "Among Us":
                for (int i = 0; i < buttons.Length; i++)
                {

                    if (buttons[i].GetComponent<Image>().sprite == defaultSprite)
                    {
                        buttons[i].GetComponent<Image>().sprite = allMain.FindCard("Among Us");
                    }
                }
                break;
            default:
                break;
        }
    }

    public void TriggerWhenPlayed()
    {
        thisCardData.UpdateData();
        for (int i = 0; i < buttons.Length; i++)
        {
            CardData cardData = buttons[i].GetComponent<CardData>();
            switch (buttons[i].GetComponent<Image>().sprite.name)
            {
                case "Pizza":
                    if (thisCardData.tribe.Equals("Food") && gameObject != buttons[i])
                    {
                        cardData.cpdAdjustment++;
                        cardData.spAdjustment++;
                    }
                    break;
                case "Seven":
                    if (thisCardData.tribe.Equals("Number") && gameObject != buttons[i])
                    {
                        for (int j = 0; j < buttons.Length; j++)
                        {
                            CardData cardData2 = buttons[j].GetComponent<CardData>();
                            if (cardData2.tribe.Equals("Number") && buttons[i] != buttons[j])
                            {
                                cardData2.cpdAdjustment++;
                                cardData2.spAdjustment++;
                            }
                        }
                    }
                    break;
                case "Milk":
                    if (thisCardData.tribe.Equals("Food") && gameObject != buttons[i])
                    {
                        thisCardData.spAdjustment++;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //triggered in UpdateManager
    public void MyUpdate()
    {
        //maybe add boolean to CardData to see if there is an aura (while on board) effect
        Aura();
        if (DayEnd.IsDayEnd && ready)
        {
            StartOfDay();
            EndOfDay();

            //Solar Eclipse check
            if (artifactActivated[2])
            {
                StartOfDay();
            }

            //Lunar Eclipse check
            if (artifactActivated[3])
            {
                EndOfDay();
            }

            artifactReady = false;
        }
        if (NoneReady())
        {
            //ensures all button readies are set to true
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<CardPlay>().ready = true;
                artifactReady = true;
            }
            DayEnd.IsDayEnd = false;
        }
    }

    //also make sure to change ReverseAura() in SellCard
    private void Aura()
    {
        int beforeCount = 0;
        int nowCount = 0;
        switch (image.sprite.name)
        {
            case "Flag of Utility":
                if (!artifactActivated[5])
                {
                    //regular
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();

                        //gives adjacent cards +1 gen and +1 sell
                        if (adjacent[i] && !affecting[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            cardData.cpdAdjustment += 1;
                            cardData.spAdjustment += 1;
                            affecting[i] = true;
                        }
                    }
                }
                else
                {
                    //with Grant's Contract
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();

                        //gives adjacent cards +1 gen and +1 sell
                        if (adjacentWC[i] && !affecting[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            cardData.cpdAdjustment += 1;
                            cardData.spAdjustment += 1;
                            affecting[i] = true;
                        }
                    }
                }
                
                break;
            case "Flag of Strength":
                if (!artifactActivated[5])
                {
                    //regular
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();

                        //gives adjacent cards +2 gen and +1 sell
                        if (adjacent[i] && !affecting[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            cardData.cpdAdjustment += 2;
                            cardData.spAdjustment += 1;
                            affecting[i] = true;
                        }
                    }
                }
                else
                {
                    //with Grant's Contract
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();

                        //gives adjacent cards +2 gen and +1 sell
                        if (adjacentWC[i] && !affecting[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            cardData.cpdAdjustment += 2;
                            cardData.spAdjustment += 1;
                            affecting[i] = true;
                        }
                    }
                }
                break;
            case "Flag of Bravery":
                if (!artifactActivated[5])
                {
                    //regular
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();

                        if (adjacent[i] && !affecting[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            cardData.cpdAdjustment += 2;
                            cardData.spAdjustment += 2;
                            cardData.freezeImmune = true;
                            cardData.paraImmune = true;
                            affecting[i] = true;
                        }
                    }
                }
                else
                {
                    //with Grant's Contract
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();

                        if (adjacentWC[i] && !affecting[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            cardData.cpdAdjustment += 2;
                            cardData.spAdjustment += 2;
                            cardData.freezeImmune = true;
                            cardData.paraImmune = true;
                            affecting[i] = true;
                        }
                    }
                }
                break;
            case "Flag of Value":
                if (!artifactActivated[5])
                {
                    //regular
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();

                        if (adjacent[i] && !affecting[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            cardData.spMultiplier *= 2;
                            affecting[i] = true;
                        }
                    }
                }
                else
                {
                    //with Grant's Contract
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();

                        if (adjacentWC[i] && !affecting[i] && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            cardData.spMultiplier *= 2;
                            affecting[i] = true;
                        }
                    }
                }
                break;
            case "Marshmallow":
                if (thisCardData.ScorchCheck())
                {
                    image.sprite = allMain.FindCard("Cooked Marshmallow");
                }
                break;
            case "Fool's Gold":
                if (!affecting[FindButtonNumber()])
                {
                    AllMain.rareChanceMultiplier *= 1.3f;
                    affecting[FindButtonNumber()] = true;
                    AllMain.UpdateValue();
                }
                break;
            case "Gold Bar":
                if (!affecting[FindButtonNumber()])
                {
                    AllMain.rareChanceMultiplier *= 4f;
                    affecting[FindButtonNumber()] = true;
                    AllMain.UpdateValue();
                }
                break;
            case "Concoction of Luck":
                if (!affecting[FindButtonNumber()])
                {
                    AllMain.rareChanceMultiplier *= 2.5f;
                    affecting[FindButtonNumber()] = true;
                    AllMain.UpdateValue();
                }
                break;
            case "Ladybug":
                if (!affecting[FindButtonNumber()])
                {
                    AllMain.rareChanceMultiplier *= 1.05f;
                    affecting[FindButtonNumber()] = true;
                    AllMain.UpdateValue();
                }
                break;
            case "Avocado":
                for (int i = 0; i < buttons.Length; i++)
                {
                    CardData cardData = buttons[i].GetComponent<CardData>();

                    if (adjacent[i] && !affecting[i] && cardData.tribe.Equals("Food"))
                    {
                        cardData.spMultiplier *= 3;
                        affecting[i] = true;
                    }
                }
                break;
            case "Shuriken":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].GetComponent<Image>().sprite.name.Equals("Shuriken"))
                    {
                        if (!affecting[i])
                        {
                            nowCount++;
                            thisCardData.spAdjustment++;

                            //Katana check
                            if (artifactActivated[13])
                            {
                                thisCardData.spAdjustment++;
                            }

                            affecting[i] = true;
                        }
                        else
                        {
                            beforeCount++;
                        }
                    }
                }
                for (int i = 0; i < (nowCount + beforeCount) / 2 - beforeCount / 2; i++)
                {
                    thisCardData.cpdAdjustment++;
                }
                break;
            default:
                break;
        }
    }

    //code simulates end of day (right after coins were given)
    private void StartOfDay()
    {
        //artifact trigger for StartOfDay()
        foreach (int i in startArtifacts)
        {
            if (artifactActivated[i] && artifactReady)
            {
                StartArtifact(i);
            }
        }

        string find = image.sprite.name;
        switch (find)
        {
            case "Whistle":
                if (DayEnd.DayInWeek == 1)
                {
                    thisCardData.cpdAdjustment += 1;
                }
                break;
            case "Concoction of Peace":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (adjacent[i] || gameObject == buttons[i])
                    {
                        buttons[i].GetComponent<CardData>().CureStatus();
                    }
                }
                break;
            case "Concoction of Misery":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (Adjacency.corner[i])
                    {
                        buttons[i].GetComponent<Destroy>().DestroyObject();
                    }
                }
                break;
            case "Starfish":
                if (DayEnd.DayInWeek == 1)
                {
                    AllMain.CoinCount += 15;
                }
                break;
            default:
                break;
        }
        ready = false;
    }

    //exactly the same as StartOfDay(), may change later
    private void EndOfDay()
    {
        //artifact trigger for EndOfDay()
        foreach (int i in endArtifacts)
        {
            if (artifactActivated[i] && artifactReady)
            {
                EndArtifact(i);
            }
        }

        bool found = false;
        string find = image.sprite.name;
        switch (find)
        {
            case "Brodinger's Cat":
                AllMain.CoinCount += 8;
                break;
            case "Trash Bin":
                if (DayEnd.DayInWeek == 1)
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        if (adjacent[i] && buttons[i].GetComponent<CardData>().tribe.Equals("") && buttons[i].GetComponent<Image>().sprite != defaultSprite)
                        {
                            if (buttons[i].GetComponent<Destroy>().DestroyObject())
                            {
                                AllMain.CoinCount += 4;
                            }
                        }
                    }
                }
                break;
            case "Ancient Scythe":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (adjacent[i])
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();
                        thisCardData.cpdAdjustment += (cardData.coinsPerDay + 1) / 2;
                        thisCardData.spAdjustment += (cardData.sellPrice + 1) / 2;
                        buttons[i].GetComponent<Destroy>().DestroyObject();
                    }
                }
                break;
            case "Daisy":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (!found && adjacent[i] && buttons[i].GetComponent<Image>().sprite.name.Equals("Empty Bottle"))
                    {
                        found = true;
                        image.sprite = defaultSprite;
                        button.enabled = true;
                        buttons[i].GetComponent<Image>().sprite = allMain.FindCard("Concoction of Peace");
                    }
                }
                break;
            case "Amaryllis":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (!found && adjacent[i] && buttons[i].GetComponent<Image>().sprite.name.Equals("Empty Bottle"))
                    {
                        found = true;
                        image.sprite = defaultSprite;
                        button.enabled = true;
                        buttons[i].GetComponent<Image>().sprite = allMain.FindCard("Concoction of Sparks");
                    }
                }
                break;
            case "Sunflower":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (!found && adjacent[i] && buttons[i].GetComponent<Image>().sprite.name.Equals("Empty Bottle"))
                    {
                        found = true;
                        image.sprite = defaultSprite;
                        button.enabled = true;
                        buttons[i].GetComponent<Image>().sprite = allMain.FindCard("Concoction of Luck");
                    }
                }
                break;
            case "Death Rose":
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (!found && adjacent[i] && buttons[i].GetComponent<Image>().sprite.name.Equals("Empty Bottle"))
                    {
                        found = true;
                        image.sprite = defaultSprite;
                        button.enabled = true;
                        buttons[i].GetComponent<Image>().sprite = allMain.FindCard("Concoction of Misery");
                    }
                }
                break;
            default:
                break;
        }
        ready = false;
    }

    //for StartOfDay() and EndOfDay()
    private bool NoneReady()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].GetComponent<CardPlay>().ready)
            {
                return false;
            }
        }
        return true;
    }

    public void ApplyStatus(string status, int turns)
    {
        thisCardData.ZeroStatus();

        foreach (GameObject button in buttons)
        {
            CardData cardData = button.GetComponent<CardData>();

            switch (button.GetComponent<Image>().sprite.name)
            {
                case "Ice Cream Cone":
                    if (status.Equals("freeze"))
                    {
                        cardData.cpdAdjustment++;
                        cardData.spAdjustment++;
                    }
                    break;
                default:
                    break;
            }
        }
        switch (status)
        {
            case "freeze":
                thisCardData.freezeTurns = turns;
                break;
            case "scorch":
                thisCardData.scorchTurns = turns;
                break;
            case "paralysis":
                thisCardData.paraTurns = turns;
                break;
            default:
                break;
        }
    }

    public int[] GenerateRandomListOf25()
    {
        int[] arr = new int[25];

        //set an array from 0 to 24
        for (int i = 0; i < 25; i++)
        {
            arr[i] = i;
        }

        //sort array to randomize it (fisher-yates shuffle)
        for (int i = 0; i < 25; i++)
        {
            int temp = arr[i];
            int rand = Random.Range(i, 24);
            arr[i] = arr[rand];
            arr[rand] = temp;
        }

        return arr;
    }

    public int FindButtonNumber()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (gameObject == buttons[i])
            {
                return i;
            }
        }
        //return should never be reached
        return 0;
    }

    public void StartArtifact(int k)
    {
        bool found = false;
        int[] list = GenerateRandomListOf25();
        switch (k)
        {
            case 4:
                //Spatula
                for (int i = 0; i < 25; i++)
                {
                    int num = list[i];
                    if (!found && buttons[num].GetComponent<Image>().sprite != defaultSprite)
                    {
                        CardData cardData = buttons[num].GetComponent<CardData>();
                        found = true;
                        if (Random.value < 0.5f)
                        {
                            cardData.cpdAdjustment++;
                        }
                        else
                        {
                            cardData.spAdjustment++;
                        }
                    }
                }
                break;
            case 6:
                //Brick
                for (int i = 0; i < allMain.repeatCount[0]; i++)
                {
                    AllMain.CoinCount++;
                }
                break;
            case 9:
                //Disco Ball
                for (int count = 0; count < allMain.repeatCount[3]; count++)
                {
                    found = false;
                    list = GenerateRandomListOf25();
                    for (int i = 0; i < 25; i++)
                    {
                        int num = list[i];
                        if (!found && buttons[num].GetComponent<Image>().sprite != defaultSprite)
                        {
                            CardData cardData = buttons[num].GetComponent<CardData>();
                            found = true;
                            cardData.cpdAdjustment += 5;
                            cardData.spAdjustment += 5;
                        }
                    }
                }
                break;
            case 17:
                //Yang
                AllMain.CoinCount += allMain.CardsOnBoard() / 5;
                break;
            default:
                break;
        }
    }

    public void EndArtifact(int k)
    {
        bool found = false;
        int[] list = GenerateRandomListOf25();
        switch (k)
        {
            case 8:
                //Piggy Bank
                for (int main = 0; main < allMain.repeatCount[2]; main++)
                {
                    //triggers 1 + hand size
                    for (int count = 0; count <= allMain.HandSize(); count++)
                    {
                        found = false;
                        list = GenerateRandomListOf25();
                        for (int i = 0; i < 25; i++)
                        {
                            int num = list[i];
                            if (!found && buttons[num].GetComponent<Image>().sprite != defaultSprite)
                            {
                                CardData cardData = buttons[num].GetComponent<CardData>();
                                found = true;

                                cardData.cpdAdjustment++;
                                cardData.spAdjustment++;
                            }
                        }
                    }
                }
                break;
            case 14:
                //Toy Race Cars
                if (DayEnd.DayInWeek == 1)
                {
                    AllMain.CoinCount += 9;
                }
                break;
            case 16:
                //Yin
                if (allMain.CardsOnBoard() <= 20)
                {
                    AllMain.CoinCount += 4;
                }
                break;
            default:
                break;
        }
    }
}
