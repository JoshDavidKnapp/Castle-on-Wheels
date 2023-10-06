using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTimerChange : MonoBehaviour
{
    /// <summary>
    /// *********************NO LONGER IN USE**********************************************************************
    /// 
    /// 
    /// 
    /// To better optimize game, we must change spawner timers far away from player in order to limit to many enemies at once
    /// 
    /// as player progresses, spawners far away will have longer times while closer ones spawn faster
    /// 
    /// numbers will most likely need to be adjusted as entire process revised to incorperate different levels with different numbers of spawners
    /// 
    /// FUTURE SCRIPT:
    /// - will measure if any allied units or player controlled object breach a spawners radius and will turn them on that way
    /// </summary>
    [Header ("All spawners in array. Added in the order they appear in inspector (R1-4, then L1-3)")]
    public GameObject[] spawners;

    [Header ("Trigger gameobjects")]
    //when it hits the changer, the closest 3 are set to 3 sec, the farthests ones are set to 20-30sec
    public GameObject timerChange1;
    public GameObject timerChange2;
    public GameObject timerChange3;

    [Header ("Used for debugging. To see which one has been activated")]
    public bool changeCheck1 = false;
    public bool changeCheck2 = false;
    public bool changeCheck3 = false;

    // Start is called before the first frame update
    void Start()
    {
        //spawners = new GameObject[8];
    }

    //each element of array corresponds to one spawner. It is in order as appearing in inspector.
    //element 0 starts with enemySpawner, element 1 is enemySpawner1, etc
    private void OnTriggerEnter(Collider other)
    {//9 total spawners for level 1
        if(other.name == "TimerChange1")
        {
            Debug.Log("change 1");
            changeCheck1 = true;
            spawners[0].GetComponent<ENEMY_SPAWN>().canSpawn = true; //r1
            spawners[1].GetComponent<ENEMY_SPAWN>().canSpawn = true; //r2
            spawners[1].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 30f;
            spawners[2].GetComponent<ENEMY_SPAWN>().canSpawn = false; //r3
            spawners[3].GetComponent<ENEMY_SPAWN>().canSpawn = false; //r4
            spawners[4].GetComponent<ENEMY_SPAWN>().canSpawn = false; //r5   
            spawners[5].GetComponent<ENEMY_SPAWN>().canSpawn = true; //l1
            spawners[5].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 20f;
            spawners[6].GetComponent<ENEMY_SPAWN>().canSpawn = true; //l2
            spawners[6].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 30f;
            spawners[7].GetComponent<ENEMY_SPAWN>().canSpawn = false; //l3
            spawners[8].GetComponent<ENEMY_SPAWN>().canSpawn = false; //l4


        }
        if (other.name == "TimerChange2")
        {
            Debug.Log("Change 2");
            changeCheck2 = true;
            spawners[0].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            spawners[1].GetComponent<ENEMY_SPAWN>().canSpawn = true;
            spawners[2].GetComponent<ENEMY_SPAWN>().canSpawn = true;
            spawners[2].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 15f;
            spawners[3].GetComponent<ENEMY_SPAWN>().canSpawn = true;
            spawners[3].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 20f;
            spawners[4].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            spawners[5].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            spawners[6].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            spawners[6].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 40f;
            spawners[7].GetComponent<ENEMY_SPAWN>().canSpawn = true; //l3
            spawners[7].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 25f;
            spawners[8].GetComponent<ENEMY_SPAWN>().canSpawn = true; //l4
            spawners[8].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 45f;
        }
        if (other.name == "TimerChange3")
        {
            Debug.Log("change 3");
            changeCheck3 = true;
            spawners[0].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            spawners[1].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            spawners[2].GetComponent<ENEMY_SPAWN>().canSpawn = true;
            spawners[2].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 60f;
            spawners[3].GetComponent<ENEMY_SPAWN>().canSpawn = true;
            spawners[3].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 30f;
            spawners[4].GetComponent<ENEMY_SPAWN>().canSpawn = true;
            spawners[4].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 30f;
            spawners[5].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            spawners[6].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            spawners[7].GetComponent<ENEMY_SPAWN>().canSpawn = true; //l3
            spawners[7].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 40f;
            spawners[8].GetComponent<ENEMY_SPAWN>().canSpawn = true; //l4
            spawners[8].GetComponent<ENEMY_SPAWN>().timeBetweenSpawns = 30f;
            //original
            //spawners[7].GetComponent<ENEMY_SPAWN>().canSpawn = false;
            //spawners[0].GetComponent<ENEMY_UNIT_SPAWN>().timeBetweenSpawns = 22;
            //spawners[1].GetComponent<ENEMY_UNIT_SPAWN>().timeBetweenSpawns = 20;
            //spawners[2].GetComponent<ENEMY_UNIT_SPAWN>().timeBetweenSpawns = 3;
            //spawners[3].GetComponent<ENEMY_UNIT_SPAWN>().timeBetweenSpawns = 5;
            //spawners[4].GetComponent<ENEMY_UNIT_SPAWN>().timeBetweenSpawns = 20;
            //spawners[5].GetComponent<ENEMY_UNIT_SPAWN>().timeBetweenSpawns = 10;
            //spawners[6].GetComponent<ENEMY_UNIT_SPAWN>().timeBetweenSpawns = 3;
            //spawners[7].GetComponent<ENEMY_UNIT_SPAWN>().timeBetweenSpawns = 10;
        }
    }
}
