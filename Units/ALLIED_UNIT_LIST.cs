using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALLIED_UNIT_LIST : MonoBehaviour
{
    public static ALLIED_UNIT_LIST S;

    [System.Serializable]
    public class Allied_Unit_Pool
    {
        public string tag;
        public GameObject alliedUnit;
        public int poolSize;
    }

    private int _currentTier;

    public List<Allied_Unit_Pool> pools;
    public Dictionary<string, Queue<GameObject>> allyUnitDictonary;
    // Start is called before the first frame update
    void Start()
    {
        S = this;
        allyUnitDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach (Allied_Unit_Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject newUnit = Instantiate(pool.alliedUnit);
                newUnit.GetComponent<ALLIED_UNIT>().createUnitInactive();
                newUnit.transform.parent = transform;
                newUnit.SetActive(false);
                objectPool.Enqueue(newUnit);
            }
            allyUnitDictonary.Add(pool.tag, objectPool);
        }
    }
    public void spawnUnit(Vector3 spawnPos)
    {


    }
    public void raiseTier()
    {
        _currentTier++;
    }
    public int returnTier()
    {
        return _currentTier;
    }

}
