using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRUNT_ENEMY : ENEMY
{
    private void Start()
    {
        speed = currentData.gSpeed;
        enemyStats.health = currentData.gHealth;

    }

}
