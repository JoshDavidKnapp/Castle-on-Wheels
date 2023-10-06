using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StrongHoldSiegeSpawn : MonoBehaviour
{
    /// <summary>
    /// 
    /// ----------------------------------------------------------------------------------------------------------
    /// NOT IN USE (SEE STRONGHOLD SCRIPT)
    /// ----------------------------------------------------------------------------------------------------------
    /// 
    /// - Will handle when siege units are spawn
    /// - Each siege enemy will be in an array
    /// 
    /// 
    /// - Each level gets progressively more difficult units, as well as slightly faster times to spawn these units. Spawn system has a given time per level, time decreases slightly as players take long time to beat level or reach checkpoint
    /// 
    /// - stronghold will spawn either by how long it takes the player to get through or by location. when players hit a certain point. It will spawn an engine.
    /// </summary>

    //siege engine prefabs
    [Header("Siege Engine Prefabs")]
    public GameObject SeigeTower;
    public GameObject SiegeBastilla;
    public GameObject SiegeCannon;

    [Header("Array Prefab")]
    //public array stores all enemy types
    public GameObject[] SiegeArray;

    [Header("Total Spawns Left")]
    //each level has different amount of engines
    public int siegeSpawns;

    //each level only has certain engines
    //level1 has only tower seige
    //level 2 has tower and bastilla
    //level 3 has tower, bastilla and cannon

    [Header("Each Level will have different Engine Spawn times")]
    //each level has a delay between seige engine spawns
    public float level1SiegeEnginesSpawntimes = 30;
    public float level2SiegeEnginesSpawntimes = 30;
    public float level3SiegeEnginesSpawntimes = 30;
    public float level4SiegeEnginesSpawntimes = 30;

    //local timer
    private float _spawnTimer = 0;

    [Header("Game Over bool")]
    //is game over?
    public bool gameOver = false;

    [Header("Spawning Location")]
    public GameObject spawningLocation;

   
    
    //we can have mutliple towers in at once


    void Awake()
    {
        //setups
        siegeSpawns = findSiegeSpawns();
       
    }

    
    void Start()
    {
        //start running this when game starts
        StartCoroutine(spawnStartDelay());
    }


    void Update()
    {
        //siegeSpawns = findSiegeSpawns();

        //if the level progresses far enough we will start to spawn engines more often
        //we start this 30 seconds in
        if(Time.time >= 30 && Time.time % 30 == 0)
        {
            Debug.Log("-10 seconds in engine spawn times");
            //loose 1/3 of the wait timer every 30 seconds
            _spawnTimer = _spawnTimer - (_spawnTimer/3);
        }
    }

    //determine which level we are on
    int findSiegeSpawns()
    {
        int spawns;
        Scene scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "Level 1":
                spawns = 1;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level1SiegeEnginesSpawntimes;
                break;
            case "Level 2":
                spawns = 2;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level2SiegeEnginesSpawntimes;
                break;
            case "Level 3":
                spawns = 4;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level3SiegeEnginesSpawntimes;
                break;
            case "Level 4":
                spawns = 7;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level4SiegeEnginesSpawntimes;
                break;
            case "DylanTest": //TEST SCENE
                spawns = 1;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level1SiegeEnginesSpawntimes;
                break;
            default:
                Debug.Log("StrongHoldSiegeSpawns: findSeigeSpawns(): Not proper name found. Setting spawns to 0...");
                spawns = 0;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level1SiegeEnginesSpawntimes;
                break;
        }
        SetArray(spawns);

        return spawns;
    }

    //slight delay at beginning of level before first engine spawns
    IEnumerator spawnStartDelay()
    {
        Debug.Log("starting spawn start delay");
        yield return new WaitForSeconds(20f);
        StartCoroutine(spawnSystemTime(_spawnTimer));
    }

    //engeine spawn system
    IEnumerator spawnSystemTime(float levelTime)
    {

        yield return new WaitForSeconds(levelTime);
        if(siegeSpawns > 0 && gameOver == false)
        {
            //spawn seige engine
            siegeSpawns--;
            //when we spawn a siege engine, we need to assign this engine the path for the level and the castle player in the level
            

        }
        if(gameOver == false)
        {
            StartCoroutine(spawnSystemTime(_spawnTimer));
        }
    }

    //
    GameObject determineSiegeType()
    {
        GameObject siege;
        siege = SiegeArray[Random.Range(0, siegeSpawns - 1)];


        return siege;
    }

    void SetArray(int arrayCount)
    {
        if(arrayCount >= 1)
        {
            SiegeArray[0] = SeigeTower;
        }
        if(arrayCount >= 2)
        {
            SiegeArray[1] = SiegeBastilla;
        }
        if(arrayCount >= 4)
        {
            SiegeArray[2] = SiegeCannon;
        }
    }
}
