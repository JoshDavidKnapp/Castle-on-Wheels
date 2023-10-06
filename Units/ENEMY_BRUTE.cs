using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_BRUTE : ENEMY
{
    private void Start()
    {
        enemyStats.health = currentData.bHealth;
        speed = currentData.bSpeed;
    }
}
