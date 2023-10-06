using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_UNIT_LIST : MonoBehaviour
{
    public static ENEMY_UNIT_LIST S;

    [System.Serializable]
    public class Enemy_Unit_Pool
    {
        public string tag;
        public GameObject enemyUnit;
        public int poolSize;
    }
    

    public List<Enemy_Unit_Pool> pools;
    public Dictionary<string, Queue<GameObject>> enemyUnitDictonary;


    public GameObject[] activeEnemyUnits;
    public GameObject[] enemyUnitsInCombat;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        enemyUnitDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach (Enemy_Unit_Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject newUnit = Instantiate(pool.enemyUnit);
                newUnit.GetComponent<ENEMY_UNIT>().createUnitInactive();
                newUnit.transform.parent = transform;
                newUnit.SetActive(false);
                objectPool.Enqueue(newUnit);
            }
            enemyUnitDictonary.Add(pool.tag, objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addToArray(GameObject newUnit,string arrayToAdd)
    {
        GameObject[] hold;
        if (arrayToAdd == "Active Enemies")
        {
            hold = activeEnemyUnits;
            activeEnemyUnits = new GameObject[hold.Length + 1];
            hold.CopyTo(activeEnemyUnits, 0);
            hold = null;
            activeEnemyUnits[activeEnemyUnits.Length - 1] = newUnit;
        }
        if(arrayToAdd =="Enemies in Combat")
        {
            hold = enemyUnitsInCombat;
            enemyUnitsInCombat = new GameObject[hold.Length + 1];
            hold.CopyTo(enemyUnitsInCombat, 0);
            hold = null;
            enemyUnitsInCombat[enemyUnitsInCombat.Length - 1] = newUnit;
            removeEnemyFromActive(newUnit);
        }

    }
    public void removeEnemyFromActive(GameObject _unit)
    {
        for (int i = 0; i < activeEnemyUnits.Length; i++)
        {
            if (activeEnemyUnits[i] == _unit)
            {
                activeEnemyUnits[i] = null;
                break;
            }  
        }
        int count;
        for (int i = 0; i < activeEnemyUnits.Length; i++)
        {
            count = i + 1;
            if(i< activeEnemyUnits.Length - 1)
            {
                if (activeEnemyUnits[i] == null)
                {
                    activeEnemyUnits[i] = activeEnemyUnits[i + 1];
                    activeEnemyUnits[i + 1] = null;
                }
            }
        }
        GameObject[] hold = new GameObject[activeEnemyUnits.Length-1];
        
        for (int i = 0; i < activeEnemyUnits.Length-1; i++)
        {
            hold[i]= activeEnemyUnits[i];
        }
        activeEnemyUnits = hold;

    }
    public void removeEnemyFromCombat(GameObject _unit)
    {
        for (int i = 0; i < enemyUnitsInCombat.Length; i++)
        {
            if (enemyUnitsInCombat[i] == _unit)
            {
                enemyUnitsInCombat[i] = null;
            }
        }
        int count;
        for (int i = 0; i < enemyUnitsInCombat.Length; i++)
        {
            count = i + 1;
            if (i < enemyUnitsInCombat.Length - 1)
            {
                if (enemyUnitsInCombat[i] == null && i < enemyUnitsInCombat.Length - 1)
                {
                    enemyUnitsInCombat[i] = enemyUnitsInCombat[i + 1];
                    enemyUnitsInCombat[i + 1] = null;
                }
            }
        }
        if (enemyUnitsInCombat.Length > 1)
        {
            GameObject[] hold = new GameObject[enemyUnitsInCombat.Length - 1];

            for (int i = 0; i < enemyUnitsInCombat.Length - 1; i++)
            {
                hold[i] = enemyUnitsInCombat[i];
            }
            enemyUnitsInCombat = hold;
        }

    }

}
