using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ENEMY_UNIT_SPAWN : MonoBehaviour
{
    [Header("Spawn Points for Unit (do not touch)")]
    public GameObject unitSpawn;
    public GameObject characterSpawn;

    [Header("Spawner Tier")]
    [Tooltip("keeps spawner from spawing until a certain tier is hit on the map 0 will make the spawners spawn on entry" +
        "use the spawn_tier_trigger prefab to dictate when the spawners start spawning based on castle location")]
    public int tier;
    

    private GameObject[] _soldierList;
    private GameObject _unit;

   [Header("Unit Type Tag")]
    public string dicTag;

    private bool _spawning;
    private bool _waitingFor;

    [Header("amount of Soldiers per Unit Rows")]
    [Tooltip("recomended value between 3 and 5")]
    public int soldierPerRow;
    [Header("Time Between Spawning Units")]
    public float timeBetweenSpawns;
    private int _soldierPerRow;
    private int _soldierAmout;
    private int _characterNum;
    private int _rowNum;
    private int _currentSoldier;

    private float[] _spawnPos;

    private float _spawnOffset;

    private void Awake()
    {

    }

    private void FixedUpdate()
    {

    
    
        if(tier<=ALLIED_UNIT_LIST.S.returnTier())
        {
            if (!_spawning)
            {
                SetForSpawn();

            }
            else if (_spawning && !_waitingFor)
            {
                StartCoroutine(spawnUnitOverTime());
            }
         }

    }

    /// <summary>
    /// Sets the spawn pos for each enemy in the enemy unit 
    /// </summary>
    public void SetForSpawn()
    {
        GameObject spawnUnit = ENEMY_UNIT_LIST.S.enemyUnitDictonary[dicTag].Dequeue();
        float savedSpawnOffset;

        _unit = spawnUnit;
        _unit.transform.position = unitSpawn.transform.position;

        spawnUnit.SetActive(true);
        _soldierList = spawnUnit.GetComponent<ENEMY_UNIT>().characters;
        _spawnPos = new float[_soldierList.Length];


        _soldierAmout = _soldierList.Length;
        if (soldierPerRow % 2 == 1)
            _spawnOffset = 0.5f;
        else
            _spawnOffset = 0;

        _spawnOffset += soldierPerRow / 2;
        savedSpawnOffset = _spawnOffset;
        int numRows = _soldierList.Length / soldierPerRow;
        if (_soldierList.Length % soldierPerRow != 0)
            numRows++;
        for (int rowCount = 0; rowCount < numRows; rowCount++)
        {
            for (int i = 0; i < soldierPerRow; i++)
            {
                if (_currentSoldier > _soldierList.Length - 1)
                    break;
                _spawnPos[_currentSoldier] = _spawnOffset-i;
                _currentSoldier++;

            }
            _spawnOffset = savedSpawnOffset;
        }
        spawnUnit.GetComponent<_UNIT>().setPosAndRank(soldierPerRow);
       _unit.transform.position -= new Vector3(0, _unit.transform.localPosition.y, numRows / 2);
        _unit.transform.rotation = unitSpawn.transform.rotation;
        // _unit.transform.localPosition = new Vector3(_unit.transform.localPosition.x, _unit.transform.localPosition.y, _unit.transform.localPosition.z *- 1);

        
        _soldierPerRow = soldierPerRow;
        _rowNum = 0;
        _waitingFor = false;
        _spawning = true;
        _currentSoldier = 0;
    }
    public void spawnSoldier()
    {
        GameObject soldier;
        //Debug.Log(_characterNum);
        GameObject unitPos = new GameObject();
        unitPos.transform.position = unitSpawn.transform.position;
        unitPos.transform.rotation = unitSpawn.transform.rotation;
        unitPos.transform.parent = this.transform;
        unitPos.transform.localPosition +=new Vector3(_spawnPos[_characterNum],0,_rowNum);
        soldier = _soldierList[_characterNum];
        soldier.SetActive(true);
        soldier.transform.position=characterSpawn.transform.position;
        
        soldier.GetComponent<NavMeshAgent>().enabled=true;
        soldier.GetComponent<NavMeshAgent>().Warp(characterSpawn.transform.position);
        soldier.GetComponent<NavMeshAgent>().SetDestination(unitPos.transform.position);
        _characterNum++;
        if (_characterNum >= _soldierPerRow)
        {
            _rowNum += 1;
            _soldierPerRow += soldierPerRow;
        }
        Destroy(unitPos);
    }
    IEnumerator spawnUnitOverTime()
    {
        spawnSoldier();
        
            _waitingFor = true;
       
        yield return new WaitForSeconds(2);
        if (_characterNum < _soldierAmout)
            _waitingFor = false;
        else
        {
           
            _characterNum = 0;
            
            StartCoroutine(waitForNextUnit(timeBetweenSpawns));
        }

    }
    IEnumerator waitForNextUnit(float wait)
    {
        ENEMY_UNIT_LIST.S.addToArray(_unit, "Active Enemies");
        _unit.GetComponent<_UNIT>().changeCurrentStateMove();
        yield return new WaitForSeconds(wait);
        _spawning = false;

    }
    //FUNCTIONS NOT BEING USED
    private void setSoldierNavMeshesInactive()
    {
        for (int i = 0; i < _soldierList.Length; i++)
        {
            _soldierList[i].GetComponent<Rigidbody>().isKinematic = true;
            _soldierList[i].GetComponent<NavMeshAgent>().enabled = false;
           

        }

    }
    //FUNCTIONS NOT BEING USED
    private void setUnitNavMesh()
    {
        setSoldierNavMeshesInactive();
        _unit.GetComponent<NavMeshAgent>().enabled = true;
        _unit.GetComponent<NavMeshAgent>().Warp(_unit.transform.position);
        _unit.GetComponent<NavMeshAgent>().radius = soldierPerRow / 2;


    }


    }



    





