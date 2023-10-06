using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum status {
spawning,
moving,
combat,
siege
}


public class ENEMY : MonoBehaviour
{
    public Castle castle;
    public NavMeshAgent agent;
    public status currentStatus;
    public int speed;
    public bool _waitToRepath=false;
    public EnemyData currentData;
    public EnemyStats enemyStats;
    public int horseDamage;
    




    // Start is called before the first frame update
    void Awake()
    {
        currentStatus = status.spawning;
        horseDamage = currentData.hDamage;
    }

    // Update is called once per frame
    virtual public void Update()
    {

        if (currentStatus == status.moving)
        {
            if (agent.isStopped == true)
            {
                agent.isStopped = false;
                _waitToRepath = false;
                agent.speed = speed;
            }

            if (!_waitToRepath)
            {
                agent.SetDestination(castle.currentPos);
                StartCoroutine(waitForRepath());



                
            }



        }
        if(currentStatus == status.combat)
        {
            agent.isStopped = true;

        }
        
    }


    private void OnTriggerEnter(Collider other)
 
    {
        if(other.gameObject.tag == "Ally" )
        {
            currentStatus = status.combat;
           
        }
        if(other.tag == "Horse Attack")
        {
           
            enemyStats.health -= horseDamage;
            enemyStats.attachedBar.startShowBar();
        }


    }



    IEnumerator waitForRepath()
    {
        _waitToRepath = true;
        yield return new WaitForSeconds(2f);
        _waitToRepath = false;
    }



}
