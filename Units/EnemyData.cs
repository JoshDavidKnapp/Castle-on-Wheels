using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Grunt Stats")]
    public int gHealth;
    public int gDamage;
    public int gSpeed;
    public int gTimeBetweenAttacks;
    [Header("Slinger Stats")]
    public int sHealth;
    public int sDamage;
    public int sSpeed;
    public int sTimeBetweenAttacks;
    [Header("Brute")]
    public int bHealth;
    public int bDamage;
    public int bSpeed;
    public int bTimeBetweenAttacks;
    [Header("Damage from Horseman")]
    public int hDamage;


}
