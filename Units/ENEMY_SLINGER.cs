using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_SLINGER : ENEMY
{

    public GameObject ally;
    public GameObject pebble;
    public GameObject range;
    private Vector3 arcVector;
    public int timeBetweenShots;
    public int maxDist;
 
    private bool _waitForNext = false;


    private void Start()
    {
        speed = currentData.sSpeed;
        enemyStats.health = currentData.sHealth;
        timeBetweenShots = currentData.sTimeBetweenAttacks;
    }
    public override void Update()
    {
        base.Update();
        if(currentStatus== status.moving )
        {
            range.SetActive(true);
            //agent.isStopped = false;
            //agent.speed = speed;
            GetComponent<EnemySlingerAnimation>().RunAnimation();
        }

        if(currentStatus == status.combat)
        {
            agent.isStopped = true;
            if (ally == null)
            {
                
                
                currentStatus = status.moving;
                GetComponent<EnemySlingerAnimation>().RunAnimation();

            }
            else if ((Vector3.Distance(transform.position, ally.transform.position) > maxDist))
            {
                
                
                currentStatus = status.moving;
                GetComponent<EnemySlingerAnimation>().RunAnimation();


            }
            else if (!_waitForNext && currentStatus == status.combat)
            {
                GetComponent<EnemySlingerAnimation>().AttackAnimation();
                StartCoroutine(shoot());
            }
            
        }
    }
    public void findVector()
    {
        float xValue;
        float zValue;
        float yValue;
        
        xValue = (transform.position.x + ally.transform.position.x)/2;
        zValue = (transform.position.z + ally.transform.position.z)/2;
        yValue = transform.position.y + 8;
        arcVector = new Vector3(xValue, yValue, zValue);

    }
    IEnumerator shoot()
    {
        _waitForNext = true;
        yield return new WaitForSeconds(1.3f);

        firePebble();
        yield return new WaitForSeconds(timeBetweenShots);
        _waitForNext = false;
    }
    public void firePebble()
    {
        GameObject newObj;
        newObj = Instantiate(pebble);
        newObj.transform.position = transform.position;
        newObj.GetComponent<PEBBLE>().shooter = gameObject;
        if (ally != null)
        {
            findVector();
            newObj.GetComponent<PEBBLE>().arcPos = arcVector;
            newObj.GetComponent<PEBBLE>().ally = ally;
            newObj.GetComponent<PEBBLE>().fly = true;
        }
        else
            Destroy(newObj);
    }



}
