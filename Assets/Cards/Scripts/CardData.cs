using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{
    private GameObject[] buttons;
    public int coinsPerDay;
    public int baseCPD;
    public int sellPrice;
    public int baseSP;
    public int cpdAdjustment;
    public int spAdjustment;

    private AllMain allMain;
    private Image image;

    public bool[] artifactApplied;
    public int artifactNumber;

    public string rarity;
    public string[] rarityNames = { "Common", "Uncommon", "Rare", "Very Rare" };

    public string tribe;
    public string infoText;

    public int cpdMultiplier = 1;
    public int spMultiplier = 1;

    public int freezeTurns;
    public int paraTurns;
    public int scorchTurns;

    public bool destroyImmune;
    public bool freezeImmune;
    public bool paraImmune;
    public bool scorchImmune;

    private bool isButton;

    private Adjacency adjacency;

    private void Start()
    {
        allMain = GetComponentInParent<AllMain>();
        buttons = allMain.buttonsGameObjects;
        image = GetComponent<Image>();
        isButton = IsButton();
        adjacency = GetComponent<Adjacency>();
        artifactApplied = new bool[allMain.artifactActivated.Length];
    }

    private void Update()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        string find = image.sprite.name;
        switch(find)
        {
            case "Two":
                baseCPD = 2;
                baseSP = 1;
                tribe = "Number";
                infoText = "No additional effects.";
                rarity = rarityNames[0];
                break;
            case "Three":
                baseCPD = 3;
                baseSP = 3;
                tribe = "Number";
                infoText = "No additional effects.";
                rarity = rarityNames[1];
                break;
            case "Four":
                baseCPD = 4;
                baseSP = 0;
                tribe = "Number";
                infoText = "When Played: Give all other numbers +2 generation and +2 sell price.";
                rarity = rarityNames[2];
                break;
            case "Five":
                baseCPD = 5;
                baseSP = 3;
                tribe = "Number";
                infoText = "When Played: Give all other numbers -1 generation and +2 sell price.";
                rarity = rarityNames[2];
                break;
            case "Six":
                baseCPD = 6;
                baseSP = 6;
                tribe = "Number";
                infoText = "When Played: Give all other numbers +6 generation and +6 sell price.";
                rarity = rarityNames[3];
                break;
            case "Seven":
                baseCPD = 7;
                baseSP = 7;
                tribe = "Number";
                infoText = "Whenever you play a number, give all other numbers +1/+1.";
                rarity = rarityNames[3];
                break;
            case "Empty Bottle":
                baseCPD = 1;
                baseSP = 3;
                tribe = "";
                infoText = "End of Day: Destroys an adjacent flower to transform into a concoction.";
                rarity = rarityNames[0];
                break;
            case "Flag of Utility":
                baseCPD = 0;
                baseSP = 0;
                tribe = "Flag";
                infoText = "Adjacent cards have +1 generation and +1 sell price.";
                rarity = rarityNames[0];
                break;
            case "Flag of Strength":
                baseCPD = 0;
                baseSP = 1;
                tribe = "Flag";
                infoText = "Adjacent cards have +1 generation and +2 sell price.";
                rarity = rarityNames[1];
                break;
            case "Flag of Bravery":
                baseCPD = 0;
                baseSP = 2;
                tribe = "Flag";
                infoText = "Adjacent cards have +2 generation and +2 sell price and are immune to freeze and paralysis.";
                rarity = rarityNames[2];
                break;
            case "Flag of Value":
                baseCPD = 0;
                baseSP = 3;
                tribe = "Flag";
                infoText = "Adjacent cards have x2 sell price.";
                rarity = rarityNames[3];
                break;
            case "Whistle":
                baseCPD = 1;
                baseSP = 3;
                tribe = "";
                infoText = "Start of Week: Gain +1 generation";
                rarity = rarityNames[0];
                break;
            case "Snowman":
                baseCPD = 2;
                baseSP = 2;
                tribe = "";
                infoText = "When Played: Freeze adjacent cards for 2 days and give them +2 generation.";
                rarity = rarityNames[1];
                break;
            case "Voltaic Dart":
                baseCPD = 3;
                baseSP = 0;
                tribe = "";
                infoText = "When Played: Paralyze adjacent cards for 2 days and give them +3 sell price.";
                rarity = rarityNames[1];
                break;
            case "Torch":
                baseCPD = 4;
                baseSP = 4;
                tribe = "";
                infoText = "When Played: Scorch this for 3 days.";
                rarity = rarityNames[1];
                break;
            case "Brodinger's Cat":
                baseCPD = -8;
                baseSP = 5;
                tribe = "Animal";
                infoText = "End of Day: Gain 8 coins.";
                rarity = rarityNames[2];
                break;
            case "One":
                baseCPD = 1;
                baseSP = 1;
                tribe = "Number";
                infoText = "When Played: If your hand is empty, encounter a rare or better card.";
                rarity = rarityNames[2];
                break;
            case "Pinata":
                baseCPD = 1;
                baseSP = 3;
                tribe = "";
                infoText = "When Destroyed: Gain 10 coins.";
                rarity = rarityNames[0];
                break;
            case "Crab":
                baseCPD = 2;
                baseSP = 3;
                tribe = "Animal";
                infoText = "When Played: Destroy adjacent cards and gain +1 generation for each destroyed.";
                rarity = rarityNames[1];
                break;
            case "Marshmallow":
                baseCPD = 1;
                baseSP = 3;
                tribe = "Food";
                infoText = "When affected by Scorch, transform into a Cooked Marshmallow with +1/+2.";
                rarity = rarityNames[0];
                break;
            case "Trash Bin":
                baseCPD = 2;
                baseSP = 1;
                tribe = "";
                infoText = "End of Week: Destroy adjacent cards without tribes. Gain 4 coins for each destroyed.";
                rarity = rarityNames[0];
                break;
            case "Fool's Gold":
                baseCPD = 2;
                baseSP = 2;
                tribe = "";
                infoText = "You have a x1.3 chance to find rare and very rare cards.";
                rarity = rarityNames[1];
                break;
            case "Gold Bar":
                baseCPD = 5;
                baseSP = 8;
                tribe = "";
                infoText = "You have a x4 chance to find rare and very rare cards.";
                rarity = rarityNames[3];
                break;
            case "Heart Rake":
                baseCPD = 3;
                baseSP = 3;
                tribe = "";
                infoText = "Gain +2/+2 whenever an adjacent card is destroyed.";
                rarity = rarityNames[2];
                break;
            case "Commander Grant":
                baseCPD = 3;
                baseSP = 2;
                tribe = "Human";
                infoText = "When Played: Give all Flags +2 generation and +2 sell price.";
                rarity = rarityNames[2];
                break;
            case "Ancient Scythe":
                baseCPD = 5;
                baseSP = 5;
                tribe = "";
                infoText = "End of Day: Destroy adjacent cards and gain half their stats (rounded up).";
                rarity = rarityNames[3];
                break;
            case "Wrench":
                baseCPD = 0;
                baseSP = 0;
                tribe = "";
                infoText = "When Played: Cure all status effects.";
                rarity = rarityNames[1];
                break;
            case "Pizza":
                baseCPD = 4;
                baseSP = 3;
                tribe = "Food";
                infoText = "Gain +1/+1 whenever you play a food.";
                rarity = rarityNames[2];
                break;
            case "Daisy":
                baseCPD = 1;
                baseSP = 3;
                tribe = "Flower";
                infoText = "Can be combined with an Empty Bottle to form a Concoction of Peace.";
                rarity = rarityNames[0];
                break;
            case "Amaryllis":
                baseCPD = 2;
                baseSP = 3;
                tribe = "Flower";
                infoText = "Can be combined with an Empty Bottle to form a Concoction of Sparks.";
                rarity = rarityNames[1];
                break;
            case "Sunflower":
                baseCPD = 3;
                baseSP = 3;
                tribe = "Flower";
                infoText = "Can be combined with an Empty Bottle to form a Concoction of Luck.";
                rarity = rarityNames[2];
                break;
            case "Death Rose":
                baseCPD = 4;
                baseSP = 4;
                tribe = "Flower";
                infoText = "(Warning: Unstable.) Can be combined with an Empty Bottle to form a Concoction of Misery.";
                rarity = rarityNames[3];
                break;
            case "Starfish":
                baseCPD = 0;
                baseSP = 2;
                tribe = "Animal";
                infoText = "Start Of Week: Gain 15 coins.";
                rarity = rarityNames[0];
                break;
            case "Potato":
                baseCPD = 2;
                baseSP = 1;
                tribe = "Food";
                infoText = "No additional effects.";
                rarity = rarityNames[0];
                break;
            case "Milk":
                baseCPD = 2;
                baseSP = 2;
                tribe = "Food";
                infoText = "Whenever you play a food, give it +1 sell price.";
                rarity = rarityNames[1];
                break;
            case "Avocado":
                baseCPD = 3;
                baseSP = 3;
                tribe = "Food";
                infoText = "Adjacent foods have x3 sell price.";
                rarity = rarityNames[3];
                break;
            case "Shuriken":
                baseCPD = 0;
                baseSP = 0;
                tribe = "";
                infoText = "Has +1 sell price for every shuriken. Has +1 generation for every two shurikens.";
                rarity = rarityNames[0];
                break;
            case "Ice Cream Cone":
                baseCPD = 2;
                baseSP = 2;
                tribe = "Food";
                infoText = "Whenever a card is frozen, gain +1/+1. When Played: Freeze adjacent cards for 1 day.";
                rarity = rarityNames[2];
                break;
            case "Friendly Fire":
                baseCPD = 2;
                baseSP = 2;
                tribe = "";
                infoText = "When Played: Scorch this for 2 days.";
                rarity = rarityNames[0];
                break;
            case "Little Rock":
                baseCPD = 1;
                baseSP = 2;
                tribe = "";
                infoText = "When Destroyed: Add a random common card to your hand.";
                rarity = rarityNames[0];
                break;
            case "Big Rock":
                baseCPD = 3;
                baseSP = 3;
                tribe = "";
                infoText = "When Destroyed: Add a random uncommon and rare card to your hand.";
                rarity = rarityNames[2];
                break;
            case "Ladybug":
                baseCPD = 1;
                baseSP = 2;
                tribe = "Animal";
                infoText = "You have a x1.05 chance to find rare and very rare cards.";
                rarity = rarityNames[0];
                break;
            case "Ruler":
                baseCPD = 2;
                baseSP = 1;
                tribe = "";
                infoText = "No additional effects.";
                rarity = rarityNames[0];
                break;
            //tokens
            case "Cooked Marshmallow":
                baseCPD = 3;
                baseSP = 4;
                tribe = "Food";
                infoText = "Toasty. Immune to Scorch's effect.";
                rarity = rarityNames[0];
                scorchImmune = true;
                break;
            case "Concoction of Peace":
                baseCPD = 1;
                baseSP = 3;
                tribe = "Concoction";
                infoText = "Start of Day: Cure all status effects of this and adjacent cards.";
                rarity = rarityNames[0];
                break;
            case "Concoction of Sparks":
                baseCPD = 2;
                baseSP = 3;
                tribe = "Concoction";
                infoText = "Whenever paralysis is cured, gain +1/+2.";
                rarity = rarityNames[1];
                break;
            case "Concoction of Luck":
                baseCPD = 3;
                baseSP = 3;
                tribe = "Concoction";
                infoText = "You have a x2.5 chance to find rare and very rare cards.";
                rarity = rarityNames[2];
                break;
            case "Concoction of Misery":
                baseCPD = 4;
                baseSP = 4;
                destroyImmune = true;
                tribe = "Concoction";
                infoText = "Immune to destroy. Whenever a card is destroyed, gain +4/+4. Start of Day: Destroy all cards on the corner.";
                rarity = rarityNames[3];
                break;
            //artifacts
            case "Prime Time":
                tribe = "Artifact";
                infoText = "Two, Three, Five, and Seven have +2 generation.";
                artifactNumber = 0;
                rarity = rarityNames[0];
                break;
            case "PEMDAS":
                tribe = "Artifact";
                infoText = "Numbers adjacent to each other have +4 generation and +4 sell price.";
                artifactNumber = 1;
                rarity = rarityNames[2];
                break;
            case "Solar Eclipse":
                tribe = "Artifact";
                infoText = "Start of Day and Start of Week effects trigger twice.";
                artifactNumber = 2;
                rarity = rarityNames[2];
                break;
            case "Lunar Eclipse":
                tribe = "Artifact";
                infoText = "End of Day and End of Week effects trigger twice.";
                rarity = rarityNames[2];
                artifactNumber = 3;
                break;
            case "Spatula":
                tribe = "Artifact";
                infoText = "Start of Day: Give a random card +1 generation or +1 sell price.";
                rarity = rarityNames[1];
                artifactNumber = 4;
                break;
            case "Grant's Contract":
                tribe = "Artifact";
                infoText = "Flags also affect diagonally touching cards.";
                rarity = rarityNames[1];
                artifactNumber = 5;
                break;
            case "Brick":
                tribe = "Artifact";
                infoText = "Start of Day: Gain 1 coin. This artifact can be found repeatedly.";
                rarity = rarityNames[0];
                artifactNumber = 6;
                break;
            case "Pamphlet":
                tribe = "Artifact";
                infoText = "The net worth requirement is reduced by 10. This artifact can be found repeatedly.";
                rarity = rarityNames[1];
                artifactNumber = 7;
                break;
            case "Piggy Bank":
                tribe = "Artifact";
                infoText = "End of Day: Give a random card +1/+1 for each card in your hand. This artifact can be found repeatedly.";
                rarity = rarityNames[2];
                artifactNumber = 8;
                break;
            case "Disco Ball":
                tribe = "Artifact";
                infoText = "Start of Day: Give a random card +5/+5. This artifact can be found repeatedly.";
                rarity = rarityNames[3];
                artifactNumber = 9;
                break;
            case "Tower of Pizza":
                tribe = "Artifact";
                infoText = "Foods have x3 sell price.";
                rarity = rarityNames[3];
                artifactNumber = 10;
                break;
            case "Cheese":
                tribe = "Artifact";
                infoText = "Foods have +1 sell price.";
                rarity = rarityNames[0];
                artifactNumber = 11;
                break;
            case "Treasure Map":
                tribe = "Artifact";
                infoText = "You encounter an additional card each day.";
                rarity = rarityNames[3];
                artifactNumber = 12;
                break;
            case "Katana":
                tribe = "Artifact";
                infoText = "Your shurikens have an additional +1 sell price for each shuriken.";
                rarity = rarityNames[1];
                artifactNumber = 13;
                break;
            case "Toy Race Cars":
                tribe = "Artifact";
                infoText = "End of Week: Gain 9 gold.";
                rarity = rarityNames[0];
                artifactNumber = 14;
                break;
            case "Flower Basket":
                tribe = "Artifact";
                infoText = "Flowers have +2 generation and +2 sell price.";
                rarity = rarityNames[1];
                artifactNumber = 15;
                break;
            case "Yin":
                tribe = "Artifact";
                infoText = "End of Day: Gain 4 coins if there are at least 5 blanks on board.";
                rarity = rarityNames[1];
                artifactNumber = 16;
                break;
            case "Yang":
                tribe = "Artifact";
                infoText = "Start of Day: Gain 1 coin for every 5 cards on board.";
                rarity = rarityNames[1];
                artifactNumber = 17;
                break;
            case "Pack Tactics":
                tribe = "Artifact";
                infoText = "Animals have +1 generation. Animals are immune to freeze.";
                rarity = rarityNames[0];
                artifactNumber = 18;
                break;
            case "Lucky Block":
                tribe = "Artifact";
                infoText = "When Selected: Encounter a common and uncommon card.";
                rarity = rarityNames[0];
                artifactNumber = 19;
                break;
            case "Star Card":
                tribe = "Artifact";
                infoText = "Your cards with no additional effects have +1/+2.";
                rarity = rarityNames[0];
                artifactNumber = 20;
                break;
            //secrets
            case "Among Us":
                baseCPD = 25;
                baseSP = 25;
                tribe = "Secret";
                infoText = "Sus";
                rarity = rarityNames[3];
                break;
            case "Skip":
                baseCPD = 0;
                baseSP = 10000;
                tribe = "Secret";
                infoText = "Skippity hoppity. Immune to status and destroy.";
                freezeImmune = true;
                paraImmune = true;
                scorchImmune = true;
                destroyImmune = true;
                rarity = rarityNames[3];
                break;
            default:
                baseCPD = 0;
                baseSP = 0;
                cpdAdjustment = 0;
                spAdjustment = 0;
                tribe = "";
                infoText = "No effect.";
                freezeTurns = 0;
                paraTurns = 0;
                scorchTurns = 0;
                for (int i = 0; i < artifactApplied.Length; i++)
                {
                    artifactApplied[i] = false;
                }
                rarity = "";
                destroyImmune = false;
                freezeImmune = false;
                paraImmune = false;
                scorchImmune = false;
                cpdMultiplier = 1;
                spMultiplier = 1;
                break;
        }

        if (isButton)
        {
            CalculateAmount();
            for (int i = 0; i < artifactApplied.Length; i++)
            {
                if (allMain.artifactActivated[i])
                {
                    ApplyArtifact(i);
                }
            }
        }
    }

    public void CureStatus()
    {
        if (freezeTurns > 0 || paraTurns > 0 || scorchTurns > 0)
        {
            TriggerCure();
        }

        freezeTurns = 0;
        paraTurns = 0;
        scorchTurns = 0;
    }

    public void ZeroStatus()
    {
        freezeTurns = 0;
        paraTurns = 0;
        scorchTurns = 0;
    }

    public void TriggerCure()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            CardData cardData = buttons[i].GetComponent<CardData>();
            switch (buttons[i].GetComponent<Image>().sprite.name)
            {
                case "Concoction of Sparks":
                    if (paraTurns > 0)
                    {
                        cardData.cpdAdjustment += 1;
                        cardData.spAdjustment += 2;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public bool ScorchCheck()
    {
        if (!scorchImmune)
        {
            bool[] adjacency = GetComponent<Adjacency>().adjacent;
            for (int i = 0; i < buttons.Length; i++)
            {
                //adjacent to scorch or is scorched
                if (scorchTurns > 0 || (adjacency[i] && buttons[i].GetComponent<CardData>().scorchTurns > 0))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void CalculateAmount()
    {
        coinsPerDay = (baseCPD + cpdAdjustment) * cpdMultiplier;
        sellPrice = (baseSP + spAdjustment) * spMultiplier;

        if (freezeTurns > 0 && !freezeImmune)
        {
            coinsPerDay = 0;
            sellPrice /= 2;
        }
        else if (paraTurns > 0 && !paraImmune)
        {
            coinsPerDay /= 2;
            sellPrice = 0;
            //affects both scorch and paralysis (gen and sell both go to zero)
            if (ScorchCheck())
            {
                coinsPerDay = 0;
            }
        }
        else if (ScorchCheck())
        {
            coinsPerDay = 0;
        }
        else
        {
            freezeTurns = 0;
            paraTurns = 0;
            scorchTurns = 0;
        }
    }

    private bool IsButton()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if(gameObject == buttons[i])
            {
                return true;
            }
        }
        return false;
    }

    public void ApplyArtifact(int k)
    {
        bool found = false;
        switch (k)
        {
            case 0:
                //Prime Time
                string find = image.sprite.name;
                if (!artifactApplied[k])
                {
                    if (find.Equals("Two") || find.Equals("Three") || find.Equals("Five") || find.Equals("Seven"))
                    {
                        cpdAdjustment += 2;
                        artifactApplied[k] = true;
                    }
                }
                break;
            case 1:
                //PEMDAS
                if (!artifactApplied[k])
                {
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();
                        if (adjacency.adjacent[i] && tribe.Equals("Number") && cardData.tribe.Equals("Number") && !artifactApplied[k])
                        {
                            cpdAdjustment += 4;
                            spAdjustment += 4;
                            artifactApplied[k] = true;
                        }
                    }
                }
                else
                {
                    //testing if there is no adjacent number
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        CardData cardData = buttons[i].GetComponent<CardData>();
                        if (adjacency.adjacent[i] && tribe.Equals("Number") && cardData.tribe.Equals("Number"))
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        cpdAdjustment -= 4;
                        spAdjustment -= 4;
                        artifactApplied[k] = false;
                    }
                }
                break;
            case 2:
                //Solar Eclipse
                //in CardPlay
                break;
            case 3:
                //Lunar Eclipse
                //in CardPlay
                break;
            case 4:
                //Spatula
                //in CardPlay
                break;
            case 5:
                //Grant's Contract
                //in CardPlay
                break;
            case 6:
                //Brick
                //in CardPlay
                break;
            case 7:
                //Pamphlet
                //in SelectEncounterCard
                break;
            case 8:
                //Piggy Bank
                //in CardPlay
                break;
            case 9:
                //Disco Ball
                //in CardPlay
                break;
            case 10:
                //Tower of Pizza
                if (!artifactApplied[k])
                {
                    if (tribe.Equals("Food"))
                    {
                        spMultiplier *= 3;
                        artifactApplied[k] = true;
                    }
                }
                break;
            case 11:
                //Cheese
                if (!artifactApplied[k])
                {
                    if (tribe.Equals("Food"))
                    {
                        spAdjustment++;
                        artifactApplied[k] = true;
                    }
                }
                break;
            case 12:
                //Treasure Map
                //in DayEnd
                break;
            case 13:
                //Katana
                //in CardPlay and SelectEncounterCard
                break;
            case 14:
                //Toy Race Cars
                //in CardPlay
                break;
            case 15:
                //Flower Basket
                if (!artifactApplied[k])
                {
                    if (tribe.Equals("Flower"))
                    {
                        cpdAdjustment += 2;
                        spAdjustment += 2;
                        artifactApplied[k] = true;
                    }
                }
                break;
            case 16:
                //Yin
                //in CardPlay
                break;
            case 17:
                //Yang
                //in CardPlay
                break;
            case 18:
                //Pack Tactics
                if (!artifactApplied[k])
                {
                    if (tribe.Equals("Animal"))
                    {
                        cpdAdjustment++;
                        freezeImmune = true;
                        artifactApplied[k] = true;
                    }
                }
                break;
            case 19:
                //Lucky Block
                //in SelectEncounterCard
                break;
            case 20:
                //Star Card
                if (!artifactApplied[k])
                {
                    if (infoText.Equals("No additional effects."))
                    {
                        cpdAdjustment += 1;
                        spAdjustment += 2;
                        artifactApplied[k] = true;
                    }
                }
                break;
            default:
                break;
        }
    }
}
