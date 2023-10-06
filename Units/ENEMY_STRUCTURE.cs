using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_STRUCTURE : MonoBehaviour
{
    //inherited by BARRIER_NODE script

    public int health;
    public HEALTH_BAR structureHealth;
    [Header("how much damage structure takes from horsemen")]
    public int horseDamage;
    public bool pathBlockingObj;
    public GameData gamedata;

    private void Update()
    {
        if (health <= 0)
        {
            OnDeath();
        }


    }

    //defaults to destroy object when it runs out of health
    public virtual void OnDeath()
    {
        //Debug.Log("dead");
        gamedata.coins += 50;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "arrow" || other.tag == "cannonball")
        {
            //Debug.Log("enemyStructure");
            //or how ever much damage we want it to take
            health = health - other.GetComponent<Projectile>().GetProjectileDamage();
            //StartCoroutine(DamageFlicker());
            structureHealth.startShowBar();


            if(other.tag == "cannonball")
            {
                //Debug.Log("check");
                other.GetComponent<Projectile>().Explode();
            }
        }
        if(other.tag =="Horse Attack")
        {
            health -= horseDamage;
            structureHealth.startShowBar();
        }
        
    }
    
    


}
