using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    public GameData gameData;

    public Text coinsText;


    [Header("Militia Settings")]
    public Text militiaHealthText;
    public Text militiaDamageText;
    public Text militiaLevelText;

    public Text militiaUpgradeText;

    public int militiaLevelIncrease = 1;
    public int militiaHealthIncrease = 10;
    public int militiaDamageIncrease = 5;


    [Header("Archer Settings")]
    public Text archerHealthText;
    public Text archerDamageText;
    public Text archerLevelText;

    public Text archerUpgradeText;

    public int archerLevelIncrease = 1;
    public int archerHealthIncrease = 5;
    public int archerDamageIncrease = 10;


    [Header("Heavy Settings")]
    public Text heavyHealthText;
    public Text heavyDamageText;
    public Text heavyLevelText;

    public Text heavyUpgradeText;

    public int heavyLevelIncrease = 1;
    public int heavyHealthIncrease = 10;
    public int heavyDamageIncrease = 10;


    [Header("Balloon Settings")]
    public Text balloonHealthText;
    public Text balloonDamageText;
    public Text balloonLevelText;

    public Text balloonUpgradeText;

    public int balloonLevelIncrease = 1;
    public int balloonHealthIncrease = 5;
    public int balloonDamageIncrease = 20;


    [Header("Assassin Settings")]
    public Text assassinHealthText;
    public Text assassinDamageText;
    public Text assassinLevelText;

    public Text assassinUpgradeText;

    public int assassinLevelIncrease = 1;
    public int assassinHealthIncrease = 10;
    public int assassinDamageIncrease = 25;


    [Header("Sorcerer Settings")]
    public Text sorcHealthText;
    public Text sorcDamageText;
    public Text sorcLevelText;

    public Text sorcUpgradeText;

    public int sorcLevelIncrease = 1;
    public int sorcHealthIncrease = 20;
    public int sorcDamageIncrease = 15;


    [Header("Food Settings")]
    public Text foodTotalText;
    public Text foodRateText;
    public Text foodUpgradeText;

    public int foodTotalIncrease = 1;
    public float foodRateIncrease = 1.0f;


    [Header("Castle Settings")]
    public Text castleHealthText;
    public Text castleDamageText;
    public Text castleLevelText;
    public Text castleSpeedText;


    public Text castleUpgradeText;

    public int castleLevelIncrease = 1;
    public int castleHealthIncrease = 10;
    public int castleDamageIncrease = 2;
    public int castleSpeedIncrease = 3;





    public void UpgradeMilitia()
    {
        if(gameData.coins >= gameData.militiaCoinsRequired)
        {
            gameData.militiaLevel += militiaLevelIncrease;
            gameData.militiaHealth += militiaHealthIncrease;
            gameData.militiaDamage += militiaDamageIncrease;

            militiaHealthText.text = "Health: " + gameData.militiaHealth.ToString();
            militiaDamageText.text = "Damage: " + gameData.militiaDamage.ToString();
            militiaLevelText.text = "Level: " + gameData.militiaLevel.ToString();

            gameData.coins = gameData.coins - gameData.militiaCoinsRequired;
            coinsText.text = "Coins: " + gameData.coins.ToString();
            gameData.militiaCoinsRequired += gameData.militiaCoinsRequiredIncrease;
            militiaUpgradeText.text = "Upgrade (" + gameData.militiaCoinsRequired + " Coins)";

        }


    }

    public void UpgradeArcher()
    {
        if(gameData.coins >= gameData.archerCoinsRequired)
        {
            gameData.archerLevel += archerLevelIncrease;
            gameData.archerHealth += archerHealthIncrease;
            gameData.archerDamage += archerDamageIncrease;

            archerHealthText.text = "Health: " + gameData.archerHealth.ToString();
            archerDamageText.text = "Damage: " + gameData.archerDamage.ToString();
            archerLevelText.text = "Level: " + gameData.archerLevel.ToString();

            gameData.coins = gameData.coins - gameData.archerCoinsRequired;
            coinsText.text = "Coins: " + gameData.coins.ToString();
            gameData.archerCoinsRequired += gameData.archerCoinsRequiredIncrease;
            archerUpgradeText.text = "Upgrade (" + gameData.archerCoinsRequired + " Coins)";
        }
        
    }


    public void UpgradeHeavy()
    {
        if (gameData.coins >= gameData.heavyCoinsRequired)
        {
            gameData.heavyLevel += heavyLevelIncrease;
            gameData.heavyHealth += heavyHealthIncrease;
            gameData.heavyDamage += heavyDamageIncrease;

            heavyHealthText.text = "Health: " + gameData.heavyHealth.ToString();
            heavyDamageText.text = "Damage: " + gameData.heavyDamage.ToString();
            heavyLevelText.text = "Level: " + gameData.heavyLevel.ToString();

            gameData.coins = gameData.coins - gameData.heavyCoinsRequired;
            coinsText.text = "Coins: " + gameData.coins.ToString();
            gameData.heavyCoinsRequired += gameData.heavyCoinsRequiredIncrease;
            heavyUpgradeText.text = "Upgrade (" + gameData.heavyCoinsRequired + " Coins)";
        }

    }


    public void UpgradeBalloon()
    {
        if (gameData.coins >= gameData.balloonCoinsRequired)
        {
            gameData.balloonLevel += balloonLevelIncrease;
            gameData.balloonHealth += balloonHealthIncrease;
            gameData.balloonDamage += balloonDamageIncrease;

            balloonHealthText.text = "Health: " + gameData.balloonHealth.ToString();
            balloonDamageText.text = "Damage: " + gameData.balloonDamage.ToString();
            balloonLevelText.text = "Level: " + gameData.balloonLevel.ToString();

            gameData.coins = gameData.coins - gameData.balloonCoinsRequired;
            coinsText.text = "Coins: " + gameData.coins.ToString();
            gameData.balloonCoinsRequired += gameData.balloonCoinsRequiredIncrease;
            balloonUpgradeText.text = "Upgrade (" + gameData.balloonCoinsRequired + " Coins)";
        }

    }



    public void UpgradeAssassin()
    {
        if (gameData.coins >= gameData.assassinCoinsRequired)
        {
            gameData.assassinLevel += assassinLevelIncrease;
            gameData.assassinHealth += assassinHealthIncrease;
            gameData.assassinDamage += assassinDamageIncrease;

            assassinHealthText.text = "Health: " + gameData.assassinHealth.ToString();
            assassinDamageText.text = "Damage: " + gameData.assassinDamage.ToString();
            assassinLevelText.text = "Level: " + gameData.assassinLevel.ToString();

            gameData.coins = gameData.coins - gameData.assassinCoinsRequired;
            coinsText.text = "Coins: " + gameData.coins.ToString();
            gameData.assassinCoinsRequired += gameData.assassinCoinsRequiredIncrease;
            assassinUpgradeText.text = "Upgrade (" + gameData.assassinCoinsRequired + " Coins)";
        }

    }



    public void UpgradeSorc()
    {
        if (gameData.coins >= gameData.sorcCoinsRequired)
        {
            gameData.sorcLevel += sorcLevelIncrease;
            gameData.sorcHealth += sorcHealthIncrease;
            gameData.sorcDamage += sorcDamageIncrease;

            sorcHealthText.text = "Health: " + gameData.sorcHealth.ToString();
            sorcDamageText.text = "Damage: " + gameData.sorcDamage.ToString();
            sorcLevelText.text = "Level: " + gameData.sorcLevel.ToString();

            gameData.coins = gameData.coins - gameData.sorcCoinsRequired;
            coinsText.text = "Coins: " + gameData.coins.ToString();
            gameData.sorcCoinsRequired += gameData.sorcCoinsRequiredIncrease;
            sorcUpgradeText.text = "Upgrade (" + gameData.sorcCoinsRequired + " Coins)";
        }

    }

    public void ResetStore()
    {
        gameData.coins = 0;

        gameData.militiaLevel = 1;
        gameData.militiaHealth = 10;
        gameData.militiaDamage = 10;
        gameData.militiaCoinsRequired = 50;

        gameData.archerLevel = 1;
        gameData.archerHealth = 10;
        gameData.archerDamage = 20;
        gameData.archerCoinsRequired = 75;

        gameData.heavyLevel = 1;
        gameData.heavyHealth = 20;
        gameData.heavyDamage = 10;
        gameData.heavyCoinsRequired = 100;

        gameData.balloonLevel = 1;
        gameData.balloonHealth = 15;
        gameData.balloonDamage = 30;
        gameData.balloonCoinsRequired = 125;

        gameData.assassinLevel = 1;
        gameData.assassinHealth = 20;
        gameData.assassinDamage = 40;
        gameData.assassinCoinsRequired = 150;

        gameData.sorcLevel = 1;
        gameData.sorcHealth = 50;
        gameData.sorcDamage = 50;
        gameData.sorcCoinsRequired = 175;

        gameData.isArcherUnlocked = false;
        gameData.isHeavyUnlocked = false;
        gameData.isBalloonUnlocked = false;
        gameData.isAssassinUnlocked = false;
        gameData.isSorcUnlocked = false;

        gameData.foodTotal = 100;
        gameData.foodRate = 5.0f;
        gameData.foodCoinsRequired = 100;

        gameData.castleLevel = 1;
        gameData.castleHealth = 150;
        gameData.castleMaxHealth = 150;
        gameData.castleDamage = 100;
        gameData.castleSpeed = 5; // changed for testing
        gameData.castleCoinsRequired = 100;

        gameData.isLevel1Complete = false;
        gameData.isLevel2Complete = false;
        gameData.isLevel3Complete = false;
    }


    public void UpgradeFood()
    {
        if (gameData.coins >= gameData.foodCoinsRequired)
        {
            gameData.foodTotal += foodTotalIncrease;
            gameData.foodRate += foodRateIncrease;



            foodTotalText.text = "Total: " + gameData.foodTotal.ToString();
            foodRateText.text = "Regen Rate: " + gameData.foodRate.ToString();

            gameData.coins = gameData.coins - gameData.foodCoinsRequired;
            coinsText.text = "Coins: " + gameData.coins.ToString();
            gameData.foodCoinsRequired += gameData.foodCoinsRequiredIncrease;
            foodUpgradeText.text = "Upgrade (" + gameData.foodCoinsRequired + " Coins)";
        }
    }


    public void UpgradeCastle()
    {
        if (gameData.coins >= gameData.castleCoinsRequired)
        {
            gameData.castleLevel += castleLevelIncrease;
            gameData.castleMaxHealth += castleHealthIncrease;
            gameData.castleDamage += castleDamageIncrease;
            gameData.castleSpeed += castleSpeedIncrease;

            castleSpeedText.text = "Speed: " + gameData.castleSpeed.ToString();
            castleHealthText.text = "Health: " + gameData.castleMaxHealth.ToString();
            castleDamageText.text = "Damage: " + gameData.castleDamage.ToString();
            castleLevelText.text = "Level: " + gameData.castleLevel.ToString();

            gameData.coins = gameData.coins - gameData.castleCoinsRequired;
            coinsText.text = "Coins: " + gameData.coins.ToString();
            gameData.castleCoinsRequired += gameData.castleCoinsRequiredIncrease;
            castleUpgradeText.text = "Upgrade (" + gameData.castleCoinsRequired + " Coins)";
        }

    }




    void Start()
    {
        coinsText.text = "Coins: " + gameData.coins.ToString();



        militiaUpgradeText.text = "Upgrade (" + gameData.militiaCoinsRequired + " Coins)";

        militiaHealthText.text = "Health: " + gameData.militiaHealth.ToString();
        militiaDamageText.text = "Damage: " + gameData.militiaDamage.ToString();
        militiaLevelText.text = "Level: " + gameData.militiaLevel.ToString();



        archerUpgradeText.text = "Upgrade (" + gameData.archerCoinsRequired + " Coins)";

        archerHealthText.text = "Health: " + gameData.archerHealth.ToString();
        archerDamageText.text = "Damage: " + gameData.archerDamage.ToString();
        archerLevelText.text = "Level: " + gameData.archerLevel.ToString();



        heavyUpgradeText.text = "Upgrade (" + gameData.heavyCoinsRequired + " Coins)";

        heavyHealthText.text = "Health: " + gameData.heavyHealth.ToString();
        heavyDamageText.text = "Damage: " + gameData.heavyDamage.ToString();
        heavyLevelText.text = "Level: " + gameData.heavyLevel.ToString();



        balloonUpgradeText.text = "Upgrade (" + gameData.balloonCoinsRequired + " Coins)";

        balloonHealthText.text = "Health: " + gameData.balloonHealth.ToString();
        balloonDamageText.text = "Damage: " + gameData.balloonDamage.ToString();
        balloonLevelText.text = "Level: " + gameData.balloonLevel.ToString();



        assassinUpgradeText.text = "Upgrade (" + gameData.assassinCoinsRequired + " Coins)";

        assassinHealthText.text = "Health: " + gameData.assassinHealth.ToString();
        assassinDamageText.text = "Damage: " + gameData.assassinDamage.ToString();
        assassinLevelText.text = "Level: " + gameData.assassinLevel.ToString();



        sorcUpgradeText.text = "Upgrade (" + gameData.sorcCoinsRequired + " Coins)";

        sorcHealthText.text = "Health: " + gameData.sorcHealth.ToString();
        sorcDamageText.text = "Damage: " + gameData.sorcDamage.ToString();
        sorcLevelText.text = "Level: " + gameData.sorcLevel.ToString();



        foodTotalText.text = "Total: " + gameData.foodTotal.ToString();
        foodRateText.text = "Regen Rate: " + gameData.foodRate.ToString();
        foodUpgradeText.text = "Upgrade (" + gameData.foodCoinsRequired + " Coins)";




        castleUpgradeText.text = "Upgrade (" + gameData.castleCoinsRequired + " Coins)";

        castleHealthText.text = "Health: " + gameData.castleHealth.ToString();
        castleDamageText.text = "Damage: " + gameData.castleDamage.ToString();
        castleLevelText.text = "Level: " + gameData.castleLevel.ToString();
        castleSpeedText.text = "Speed: " + gameData.castleSpeed.ToString();




    }

    void Update()
    {

    }
}


    
   


