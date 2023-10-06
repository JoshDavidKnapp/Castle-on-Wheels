using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class GameData : ScriptableObject
{
    public int EnemyGruntHealth = 10;

    public int coins;

    public Vector3 Waypoint;

    [Header("Militia Settings")]
    public int militiaLevel = 1;
    public int militiaHealth = 20;
    public int militiaDamage = 10;
    public int militiaCost = 5;    // added for testing


    [Header("Archer Settings")]
    public int archerLevel = 1;
    public int archerHealth = 10;
    public int archerDamage = 20;
    public int archerCost = 5;    // added for testing

    [Header("Heavy Settings")]
    public int heavyLevel = 1;
    public int heavyHealth = 30;
    public int heavyDamage = 10;
    public int heavyCost = 8;    // added for testing

    [Header("Balloon Settings")]
    public int balloonLevel = 1;
    public int balloonHealth = 10;
    public int balloonDamage = 30;
    public int balloonCost = 7;    // added for testing

    [Header("Assassin Settings")]
    public int assassinLevel = 1;
    public int assassinHealth = 10;
    public int assassinDamage = 40;
    public int assassinCost = 6;    // added for testing

    [Header("Sorcerer Settings")]
    public int sorcLevel = 1;
    public int sorcHealth = 30;
    public int sorcDamage = 50;
    public int sorcererCost = 4;  // added for testing

    [Header("Castle Settings")]
    public int castleLevel = 1;
    public int castleHealth = 100;
    public int castleMaxHealth = 100;
    public int castleDamage = 100;
    public int castleSpeed = 5; //changed for testing

    public int castleCoinsRequired = 100;
    public int castleCoinsRequiredIncrease = 100;

    public int foodTotal = 10;
    public float foodRate = 5.0f;
    public int foodCurrent = 10;

    [Header("Store Settings")]
    public bool isArcherUnlocked = false;
    public bool isHeavyUnlocked = false;
    public bool isBalloonUnlocked = false;
    public bool isAssassinUnlocked = false;
    public bool isSorcUnlocked = false;

    public int militiaCoinsRequired = 50;
    public int militiaCoinsRequiredIncrease = 25;

    public int archerCoinsRequired = 75;
    public int archerCoinsRequiredIncrease = 50;

    public int heavyCoinsRequired = 125;
    public int heavyCoinsRequiredIncrease = 75;

    public int balloonCoinsRequired = 150;
    public int balloonCoinsRequiredIncrease = 100;

    public int assassinCoinsRequired = 175;
    public int assassinCoinsRequiredIncrease = 125;

    public int sorcCoinsRequired = 200;
    public int sorcCoinsRequiredIncrease = 150;

    

    public int foodCoinsRequired = 50;
    public int foodCoinsRequiredIncrease = 100;

    [Header("Level Panel Settings")]
    public bool isLevel1Complete = false;
    public bool isLevel2Complete = false;
    public bool isLevel3Complete = false;


}
