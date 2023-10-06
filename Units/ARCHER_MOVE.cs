using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCHER_MOVE : AllyMovement
{
    public status currentStatus;
    public GameObject enemy;
    public GameObject range;
    public GameObject arrow;
    public int maxDist;

    public int timeBetweenShots;
    private Vector3 arcVector;
    bool _waitForNext = false;




    public override void Update()
    {
        base.Update();
        if (currentStatus == status.moving)
        {
            range.SetActive(true);
            agent.isStopped = false;
            agent.speed = 10;

        }

        if (currentStatus == status.combat)
        {
            agent.isStopped = true;
            if (enemy == null)
            {


                currentStatus = status.moving;
                GetComponent<AllyArcherAnimation>().IdleAnimation();

            }
            else if ((Vector3.Distance(transform.position, enemy.transform.position) > maxDist))
            {


                currentStatus = status.moving;
                GetComponent<AllyArcherAnimation>().RunAnimation();

            }
            else if (!_waitForNext && currentStatus == status.combat)
            {
                GetComponent<AllyArcherAnimation>().AttackAnimation();
                StartCoroutine(shoot());
            }

        }

    }
    public void findVector()
    {
        if(enemy != null)
        {
            float xValue;
            float zValue;
            float yValue;

            xValue = (transform.position.x + enemy.transform.position.x) / 2;
            zValue = (transform.position.z + enemy.transform.position.z) / 2;
            yValue = transform.position.y + 8;
            arcVector = new Vector3(xValue, yValue, zValue);

        }

    }
    IEnumerator shoot()
    {
        _waitForNext = true;
        yield return new WaitForSeconds(1.3f);
        
        
        fireArrow();
        yield return new WaitForSeconds(timeBetweenShots);
        _waitForNext = false;
    }
    public void fireArrow()
    {

        
        GameObject newObj;
        newObj = Instantiate(arrow);
        newObj.transform.position = transform.position;
        newObj.GetComponent<ARROW>().shooter = gameObject;
        newObj.GetComponent<ARROW>().arcPos = arcVector;
        if (enemy != null)
        {
            findVector();
            newObj.GetComponent<ARROW>().arcPos = arcVector;
            newObj.GetComponent<ARROW>().enemy = enemy;
            newObj.GetComponent<ARROW>().fly = true;
        }
        else
            Destroy(newObj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy Unit" || other.gameObject.tag == "SiegeTower" || other.gameObject.tag == "SiegeCannon" || other.gameObject.tag == "SiegeBastilla")
        {
           
            print("COLLIDED");
            agent.speed = 0;
            StartCoroutine("Damage");

        }

        if (other.gameObject.tag == "Stronghold")
        {
          

            print("COLLIDED");
            agent.speed = 0;
            StartCoroutine("Damage");

        }
        if (other.gameObject.tag == "BigGuyAttack")
        {
            Debug.Log("OUCH");
            health -= bruteDamage;
        }

        //cannonballs damage handled in cannonball script
        if (other.gameObject.tag == "enemyProjectile")
        {
            //Debug.Log("OW AN ARROW HIT ME CRAP");
            health -= other.gameObject.GetComponent<Projectile>().projectileDamage;
        }
        if(other.gameObject.tag == "cannonBallCart")
        {
            health -= other.gameObject.GetComponent<cannonBallBehavior>().cannonBallDamage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy Unit")
        {
            print("EXITED");
            agent.speed = 10;
            clickScript.ClearSelection();
        }

        if (other.gameObject.tag == "Stronghold")
        {
            StopCoroutine(Damage());

        }

    }
}
