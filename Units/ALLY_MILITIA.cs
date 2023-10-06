using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALLY_MILITIA : AllyMovement
{
    private void Start()
    {
        health = gameData.militiaHealth;
    }
    public IEnumerator WPDestroy()
    {
        yield return new WaitForSeconds(1);
        //Destroy(new);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy Unit" || other.gameObject.tag == "SiegeTower")
        {

            GetComponent<AllyMeleeAnimation>().AttackAnimation();
            EnemyStats hp = other.gameObject.GetComponent<EnemyStats>();
            hp.health--;
            hp.attachedBar.startShowBar();
            print("COLLIDED");
            agent.speed = 0;
            StartCoroutine("Damage");

        }

        if (other.gameObject.tag == "Stronghold")
        {
            GetComponent<AllyMeleeAnimation>().AttackAnimation();
            Stronghold hp = other.gameObject.GetComponent<Stronghold>();
            hp.health--;
            
            print("COLLIDED");
            agent.speed = 0;
            StartCoroutine("Damage");

        }
        if (other.gameObject.tag == "BigGuyAttack")
        {
            Debug.Log("OUCH");
            health -= bruteDamage;
        }
        if(other.gameObject.tag == "Structure")
        {
            GetComponent<AllyMeleeAnimation>().AttackAnimation();
            other.gameObject.GetComponent<ENEMY_STRUCTURE>().health--;
            other.gameObject.GetComponent<ENEMY_STRUCTURE>().structureHealth.startShowBar();
            StartCoroutine("Damage");
        }

        //cannonballs damage handled in cannonball script
        if(other.gameObject.tag == "enemyProjectile")
        {
            //Debug.Log("OW AN ARROW HIT ME CRAP");
            health -= other.gameObject.GetComponent<Projectile>().projectileDamage;
        }
        if (other.gameObject.tag == "cannonBallCart")
        {
            health -= other.gameObject.GetComponent<cannonBallBehavior>().cannonBallDamage;
        }

        if(other == null)
        {
            if(agent.speed >= 0 && agent.isStopped == false)
            {
                GetComponent<AllyMeleeAnimation>().RunAnimation();
                agent.speed = 10;
            }
            else
            {
                GetComponent<AllyMeleeAnimation>().IdleAnimation();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy Unit")
        {
           GetComponent<AllyMeleeAnimation>().RunAnimation();
            

            print("EXITED");
            agent.speed = 10;
            clickScript.ClearSelection();
        }

        if (other.gameObject.tag == "Stronghold")
        {
            StopCoroutine(Damage());
            GetComponent<AllyMeleeAnimation>().IdleAnimation();

        }
        if (other.gameObject.tag == "Structure")
        {
            StopCoroutine(Damage());
            GetComponent<AllyMeleeAnimation>().IdleAnimation();
        }

    }

    private void OnTriggerStay(Collider other)
    {

    }

   
}
