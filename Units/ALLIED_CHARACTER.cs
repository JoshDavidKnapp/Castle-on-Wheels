using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALLIED_CHARACTER : _CHARACTERS
{
    public GameData TroopData;
    public bool waitForNextCycle;
    public bool waitForAnim;
   
    // Start is called before the first frame update
    void Awake()
    {
        level = TroopData.militiaLevel;
       health = TroopData.militiaHealth;
        damage = TroopData.militiaDamage;

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!waitForNextCycle && retFoundEnemy())
        {
            if (enemy != null)
            {
                StartCoroutine(nextAttackCycle());
            }
            else
            {
                foundEnemyChange(false);
                inCombat = false;
                GetComponentInParent<_UNIT>().checkIfAllDone();
            }
        }


    }


    private void combat()
    {
        enemy.GetComponent<_CHARACTERS>().subHealth(Random.Range(damage/2,damage));
        subHealth(Random.Range(enemy.GetComponent<_CHARACTERS>().retDamage() / 2, enemy.GetComponent<_CHARACTERS>().retDamage()));


        if (enemy.GetComponent<_CHARACTERS>().retHealth() <= 0)
        {
            if (enemy != null)
            {
                enemy.GetComponentInParent<_UNIT>().resizeArrayAfterDeath(enemy);
            }
            independentMovement = false;
            inCombat = false;
            
            enemy.SetActive(false);
          
            
            enemy.GetComponentInParent<_UNIT>().checkIfAllDone();
            GetComponentInParent<_UNIT>().checkIfAllDone();
            foundEnemyChange(false);
            //play ally attack
            //play enemy death anim
        }
        if (health <= 0)
        {
            Debug.Log(health);
            GetComponentInParent<_UNIT>().resizeArrayAfterDeath(gameObject);
           
            enemy.GetComponent<_CHARACTERS>().independentMovement = false;
            enemy.GetComponent<_CHARACTERS>().inCombat = false;
            enemy.GetComponent<_CHARACTERS>().foundEnemyChange(false);
            
            GetComponentInParent<_UNIT>().checkIfAllDone();
            if (enemy.activeSelf == true)
            {
                enemy.GetComponentInParent<_UNIT>().checkIfAllDone();
            }
            gameObject.SetActive(false);
            //play enemy attack
            //play death anim
        }
        else
        {
            int randomAnim;

            randomAnim = Random.Range(0, 2);

            if (randomAnim == 0)
            {

                //play ally first
                //play enemy last
            }
            else
            {
               //play enemy first
               //play ally last
            }

        }
    }
    IEnumerator nextAttackCycle()
    {
        waitForNextCycle = true;
        combat();
        yield return new WaitForSeconds(2);
        waitForNextCycle = false;
    }
    IEnumerator nextAttackAnim(Animation ani1,Animation ani2)
    {
        ani1.Play();
        yield return new WaitForSeconds(0.5f);
        ani2.Play();
    }



}
