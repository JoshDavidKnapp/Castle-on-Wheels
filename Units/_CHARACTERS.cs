using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class _CHARACTERS : MonoBehaviour
{
    public NavMeshAgent agent;
    public string otherTag;
    public bool inCombat;
    public bool notFighting;
    public bool independentMovement;
    public Vector3 moveVector;
    public float speed;
    public GameObject enemy;
    private bool _waitToRepath;
    private bool _foundEnemy;
    protected RaycastHit _hit;

    //needs to be added to main game
    protected float level;
    protected float health;
    protected float damage;

    // Update is called once per frame


    public virtual void FixedUpdate()
    {
        
        if ((inCombat&&independentMovement)&&!_waitToRepath&&!_foundEnemy)
        {
            StartCoroutine(waitToRepath());
            agent.SetDestination(enemy.transform.position);
            if (Vector3.Distance(transform.position, enemy.transform.position) < 1f)
            {
                agent.isStopped = true;
                _foundEnemy = true;
            }
        }
        
    }
   



    protected IEnumerator waitToRepath()
    {
        _waitToRepath = true;
        yield return new WaitForSeconds(.5f);
        _waitToRepath = false;
    }

    public void subHealth(float damage)
    {
        health -= damage;
    }
    public float retHealth()
    {
        return health;
    }
    public float retDamage()
    {
        return damage;
    }
    public bool retFoundEnemy()
    {
        return _foundEnemy;
        
    }
    public void foundEnemyChange(bool change)
    {
        _foundEnemy = change;
    }
}
