using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum engineType
{
    towerEngine,
    bastillaEngine,
    cannonCartEngine,
    none
}

public class Stronghold : MonoBehaviour
{
    /// <summary>
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
    //spawning engine is active
    public bool _spawningActive = false;
    //counter for timer reductio
    private float _counter = 0;
    //public float timer = 0.0f;
    private bool _decrementCooldown = false;
    [Header("Hpw often the timer for when we spawn engines decrease")]
    public int spawnDelayDecreaseTimer = 5;

    [Header("Game Over bool")]
    //is game over?
    public bool gameOver = false;

    [Header("Spawning Location")]
    public GameObject spawningLocation;
    [Header("Spawn Delay")]
    public int spawnDelay = 20;

    [Header("Path for engines to travel")]
    public DrawPath path;
    [Header("Player castle")]
    public GameObject player;

    [Header("Stronghold Health")]
    public int health = 100;

    [Header("Gamedata")]
    public GameData gameData;
    private Vector3 pos;

    [Header("UI Panel")]
    public GameObject winPanel;

    [Header("Debugging engine spawning")]
    public bool spawningActiveD = false;
    [Header("health bar script")]
    public HEALTH_BAR healthBar;

    public int siegeTypes;


    void Awake()
    {
        //setups
        siegeSpawns = findSiegeSpawns();

    }


    void Start()
    {
        //start running this when game starts
        if(spawningActiveD)
            StartCoroutine(spawnStartDelay());
    }


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameData.isLevel1Complete = true;
            gameData.coins += 200;
            winPanel.SetActive(true);
            transform.position = new Vector3(0, 0, 0);
            StartCoroutine("Wait");

        }

        //siegeSpawns = findSiegeSpawns();

        //if the level progresses far enough we will start to spawn engines more often
        //we start this 30 seconds in
        //we start counting when we are in spawning engine coroutine
        if (_spawningActive && siegeSpawns > 0)
        {
            _counter += Time.deltaTime;

            //Debug.Log(num);
            //Debug.Log((int)_counter);
            if(_counter > spawnDelayDecreaseTimer && (int)_counter % spawnDelayDecreaseTimer == 0)
            {
                //Debug.Log("GOT EM");
                //we cant make the decrease to low
                //has a cool down so it doesnt quickly repeat
                if(_spawnTimer >= 10 && _decrementCooldown == false)
                {
                    StartCoroutine(decrementTimer());
                }
            }
        }
    }
    
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);

    }

    //spawning systems

    //avoid spam use 
    private IEnumerator decrementTimer()
    {
        _decrementCooldown = true;
        yield return new WaitForSeconds(1.0f);
        Debug.Log("-10 seconds in engine spawn times");
        //loose 1/3 of the wait timer every 30 seconds
        _spawnTimer = _spawnTimer - (_spawnTimer / 3);
        _decrementCooldown = false;
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
                siegeTypes = 1;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level1SiegeEnginesSpawntimes;
                break;
            case "Level 2":
                spawns = 2;
                siegeTypes = 2;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level2SiegeEnginesSpawntimes;
                break;
            case "Level 3":
                spawns = 4;
                siegeTypes = 3;
                SiegeArray = new GameObject[spawns];
                _spawnTimer = level3SiegeEnginesSpawntimes;
                break;
            case "Level 4":
                spawns = 7;
                siegeTypes = 3;
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
        yield return new WaitForSeconds(spawnDelay);
        _spawningActive = true;
        StartCoroutine(spawnSystemTime(_spawnTimer));
    }

    //engeine spawn system
    IEnumerator spawnSystemTime(float levelTime)
    {

        yield return new WaitForSeconds(levelTime);
        if (siegeSpawns > 0 && gameOver == false)
        {
            //if there are no objects that are a siege tower
            if(FindObjectsOfType<EnemySiegeTowerAI>().Length < 1)
            {
                //spawn seige engine
                siegeSpawns--;
                //when we spawn a siege engine, we need to assign this engine the path for the level and the castle player in the level
                //will not spawn multiple siege towers in level, only one at a time
                Debug.Log("spawned engine");

                GameObject engine = Instantiate(determineSiegeType(), spawningLocation.transform.position, this.transform.rotation);
                engine.GetComponent<SiegeTracker>().pathToFollowS = path;
                engine.GetComponent<SiegeTracker>().castleObj = player;
            }

        }
        //we stop spawning if the game ends or if we are out of spawns
        if (gameOver == false && siegeSpawns > 0)
        {
            StartCoroutine(spawnSystemTime(_spawnTimer));
        }
    }

    //which engine are we using for the spawning
    GameObject determineSiegeType()
    {
        int num = Random.Range(0, siegeTypes);
        //Debug.Log(num);
        GameObject siege;
        siege = SiegeArray[num];


        return siege;
    }

    //set up the array based on what level
    void SetArray(int arrayCount)
    {
        if (arrayCount >= 1)
        {
            SiegeArray[0] = SeigeTower;
        }
        if (arrayCount >= 2)
        {
            SiegeArray[1] = SiegeBastilla;
        }
        if (arrayCount >= 4)
        {
            SiegeArray[2] = SiegeCannon;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ally")
        {
            health--;
            StartCoroutine(Damage());
        }
        if(other.tag == "Horse Attack")
        {
            health -= 5;
            healthBar.startShowBar();
        }
        if (other.tag == "arrow" || other.tag == "cannonball")
        {
            //Debug.Log("enemyStructure");
            //or how ever much damage we want it to take
            health = health - other.GetComponent<Projectile>().GetProjectileDamage();
            //StartCoroutine(DamageFlicker());
            healthBar.startShowBar();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ally")
        {
            StopCoroutine(Damage());
        }
    }

    public IEnumerator Damage()
    {
        health--;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Damage());
    }


}
