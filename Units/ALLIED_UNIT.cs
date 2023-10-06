using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ALLIED_UNIT : _UNIT
{
    public float combatDistance;
    private GameObject _closestUnit;

    

    private void Awake()
    {
        

    }
    private void FixedUpdate()
    {
        if (_currentState == unitState.moving && !_waitToRepath)
        {
            findEnemyUnit();
            if (Vector3.Distance(_closestUnit.GetComponent<_UNIT>().unitAverageVector(), unitAverageVector()) < combatDistance)
            {
                stopUnit();
                _currentState = unitState.combat;
                _closestUnit.GetComponent<_UNIT>().changeCurrentStateCombat();
                _closestUnit.GetComponent<_UNIT>().stopUnit();
                ENEMY_UNIT_LIST.S.addToArray(_closestUnit, "Enemies in Combat");
                enterCombat();
            }
            else
            {
                moveToPos(_closestUnit.GetComponent<_UNIT>().unitAverageVector());
                StartCoroutine(waitToRepath());
            }
        }
       


    }

    //finds the enemy unit to path to by distance
    public void findEnemyUnit()
    {
        GameObject[] activeEnemies = ENEMY_UNIT_LIST.S.activeEnemyUnits;
        Debug.Log(activeEnemies.Length);
        
        
            _closestUnit = activeEnemies[0];

            for (int i = 0; i < activeEnemies.Length; i++)
            {

                if (Vector3.Distance(unitAverageVector(), activeEnemies[i].GetComponent<_UNIT>().unitAverageVector()) < Vector3.Distance(unitAverageVector(), _closestUnit.GetComponent<_UNIT>().unitAverageVector()))
                {
                    _closestUnit = activeEnemies[i];

                }



            }
        
        
            

    }
    public void enterCombat()
    {
        GameObject[] enemyCharacters = _closestUnit.GetComponent<_UNIT>().characters;
        GameObject unitForCombat=enemyCharacters[0];
        for (int i = 0; i < characters.Length; i++)
        {
            for (int z = 0; z < enemyCharacters.Length; z++)
            {
                if (!enemyCharacters[z].GetComponent<_CHARACTERS>().inCombat)
                {
                    unitForCombat = enemyCharacters[z];
                    break;
                }
            }
            for (int o = 0; o < enemyCharacters.Length; o++)
            {
                if (!enemyCharacters[o].GetComponent<_CHARACTERS>().inCombat)
                {
                    if (Vector3.Distance(unitForCombat.transform.position, characters[i].transform.position) > Vector3.Distance(enemyCharacters[o].transform.position, characters[i].transform.position))
                    {
                        unitForCombat = enemyCharacters[o];

                    }
                }
                else
                {

                }
            }
            characters[i].GetComponent<_CHARACTERS>().inCombat = true;
            unitForCombat.GetComponent<_CHARACTERS>().inCombat = true;
            findAndSetDest(characters[i], unitForCombat);
        }

    }
               
    
    private void findAndSetDest(GameObject ally,GameObject enemy)
    {
      
        setDest(ally, enemy);
        setDest(enemy, ally);
        
    }
    private void setDest(GameObject attacker, GameObject defender )
    {

        attacker.GetComponent<_CHARACTERS>().enemy = defender;
        attacker.GetComponent<_CHARACTERS>().agent.isStopped = false;
        attacker.GetComponent<_CHARACTERS>().independentMovement = true;
    }


    
    


}
