using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ALLIED_SPAWNING : MonoBehaviour
{

    Castle castlePos;

    RaycastHit hit;
    public int amountPerRow;

    public string dicTag;
    private GameObject[] _soldiers;
    private GameObject _unit;

    

   
    public void Spawn(Vector3 point)
    {
        setUnitForSpawn();

        GameObject spawnPoint = new GameObject();
        Vector3 offset = new Vector3(amountPerRow/2, 0,0);
        int holdAmountPerRow = amountPerRow;
        int rowNum=0;
        int spawnPosX =0;
        _soldiers=_unit.GetComponent<_UNIT>().characters; 
        spawnPoint.transform.position = point+offset;
        for (int i = 0; i < _soldiers.Length; i++)
        {
            if (holdAmountPerRow > i)
            {
                rowNum++;
                spawnPosX = 0;
                holdAmountPerRow += amountPerRow;
            }
            _soldiers[i].SetActive(true);
            _soldiers[i].GetComponent<NavMeshAgent>().enabled = true;
            _soldiers[i].GetComponent<NavMeshAgent>().Warp(spawnPoint.transform.position + new Vector3(spawnPosX, 0, -rowNum));
            spawnPosX++;
        }
        Destroy(spawnPoint);
        _unit.GetComponent<_UNIT>().changeCurrentStateMove();

    }
    public void setUnitForSpawn()
    {
        _unit = ALLIED_UNIT_LIST.S.allyUnitDictonary[dicTag].Dequeue();
        _unit.SetActive(true);
        _unit.GetComponent<_UNIT>().setPosAndRank(amountPerRow);


    }

}
