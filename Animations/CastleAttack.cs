using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleAttack : MonoBehaviour
{
    //public GameObject setPanel;

    public GameObject castle;
    Transform[] _nearbyEnemies;
    private Transform closestEnemy;
    [Header("Input the Projectile Prefab Here")]
    public GameObject projectile;
    [Header("This is Point at Which Projectiles are Spawned From")]
    public GameObject projectileSpawnPoint;
    public GameObject currentTarget;

    [Header("The Radius of The Attack Range")]
    public float checkRadius = 10f;

    [Header("Amount of Damage for Projectile to Do")]
    public int projectileDamage = 1;

    [Header("Time Between Projectile Spawns")]
    public float timeBetween = 3f;

    public bool isCannon;
    [Header("Cannon prefab for rotation")]
    public GameObject cannonPrefab;
    public GameObject cannonYRotPref;
    public GameObject cannonGhostTracker;
    public float cannonRotateSpeed = 1f;
    private Quaternion _idle;
    private Quaternion _idleY;

    public bool siegeMode = false;
    public bool siegeTowerInFront = false;

    [Header("Check this bool if this object is a SM_Tower")]
    public bool isDefenseTower = false;

    void Start()
    {
        StartCoroutine(DoCheck());
        projectileSpawnPoint = this.gameObject;

            
    }

    private void Update()
    {
        if (siegeMode == true)
        {
            //setPanel.SetActive(true);
        }
        else
        {
           // setPanel.SetActive(false);
        }


        
    }

    public void SetSiegeMode()
    {
        if(siegeMode)
        {
            if(!siegeTowerInFront)
            {
                castle.GetComponent<FollowPath>()._blocked = false;
                castle.GetComponent<FollowPath>()._isMoving = true;
                siegeMode = false;
            }
        }
        else
        {
            castle.GetComponent<FollowPath>()._blocked = true;
            castle.GetComponent<FollowPath>()._isMoving = false;
            siegeMode = true;

            //StartCoroutine(SiegeSetup());
        }
        
        
        
    }

    Transform GetClosestEnemy()
    {
        if(isCannon)
        {
            _nearbyEnemies = collidersToTransforms(Physics.OverlapSphere(transform.position, checkRadius));
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Pow(checkRadius, 2);
            Vector3 currentPosition = transform.position;
            foreach (Transform potentialTarget in _nearbyEnemies)
            {
                if(isDefenseTower) //dont target structures, if this script is not on castle player
                {
                    if ((potentialTarget.gameObject.tag == "SiegeTower" || potentialTarget.gameObject.tag == "Stronghold" || potentialTarget.gameObject.tag == "TEST" || potentialTarget.gameObject.tag == "enemySpawner" || potentialTarget.gameObject.tag == "SiegeCannon" || potentialTarget.gameObject.tag == "SiegeBastilla") && siegeMode)
                    {

                        float dSqrToTarget = getDistanceSqr(currentPosition, potentialTarget.position);
                        //Debug.Log(dSqrToTarget);
                        if (dSqrToTarget < closestDistanceSqr)
                        {
                            closestDistanceSqr = dSqrToTarget;
                            bestTarget = potentialTarget;
                        }
                    }
                }
                else
                {
                    //this is castle's attacks, so target structures
                    if ((potentialTarget.gameObject.tag == "SiegeTower" || potentialTarget.gameObject.tag == "Stronghold" || potentialTarget.gameObject.tag == "TEST" || potentialTarget.gameObject.tag == "enemySpawner" || potentialTarget.gameObject.tag == "Structure" || potentialTarget.gameObject.tag == "SiegeCannon" || potentialTarget.gameObject.tag == "SiegeBastilla") && siegeMode)
                    {

                        float dSqrToTarget = getDistanceSqr(currentPosition, potentialTarget.position);
                        //Debug.Log(dSqrToTarget);
                        if (dSqrToTarget < closestDistanceSqr)
                        {
                            closestDistanceSqr = dSqrToTarget;
                            bestTarget = potentialTarget;
                        }
                    }
                }
                
                

            }
            return bestTarget;
        }
        else
        {
            _nearbyEnemies = collidersToTransforms(Physics.OverlapSphere(transform.position, checkRadius));
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Pow(checkRadius, 2);
            Vector3 currentPosition = transform.position;
            foreach (Transform potentialTarget in _nearbyEnemies)
            {
                if(isDefenseTower)
                {
                    if (potentialTarget.gameObject.tag == "Enemy Unit" || potentialTarget.gameObject.tag == "SiegeTower" || potentialTarget.gameObject.tag == "Stronghold" || potentialTarget.gameObject.tag == "TEST" || potentialTarget.gameObject.tag == "SiegeCannon" || potentialTarget.gameObject.tag == "SiegeBastilla")
                    {

                        float dSqrToTarget = getDistanceSqr(currentPosition, potentialTarget.position);
                        //Debug.Log(dSqrToTarget);
                        if (dSqrToTarget < closestDistanceSqr)
                        {
                            closestDistanceSqr = dSqrToTarget;
                            bestTarget = potentialTarget;
                        }
                    }
                }
                else
                {
                    if (potentialTarget.gameObject.tag == "Enemy Unit" || potentialTarget.gameObject.tag == "SiegeTower" || potentialTarget.gameObject.tag == "Stronghold" || potentialTarget.gameObject.tag == "TEST" || potentialTarget.gameObject.tag == "Structure" || potentialTarget.gameObject.tag == "SiegeCannon" || potentialTarget.gameObject.tag == "SiegeBastilla")
                    {

                        float dSqrToTarget = getDistanceSqr(currentPosition, potentialTarget.position);
                        //Debug.Log(dSqrToTarget);
                        if (dSqrToTarget < closestDistanceSqr)
                        {
                            closestDistanceSqr = dSqrToTarget;
                            bestTarget = potentialTarget;
                        }
                    }
                }
                
            }
            //Debug.Log(closestDistanceSqr);
            return bestTarget;
        }
        
    }

    IEnumerator DoCheck()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetween/2);
            Vector3 currentPosition = transform.position;
            
            if ((currentTarget != null && currentTarget.gameObject.activeInHierarchy) && getDistanceSqr(currentPosition, currentTarget.transform.position) < Mathf.Pow(checkRadius, 2) && siegeMode)
            {

                //Debug.Log(closestEnemy.name);
                projectile.GetComponent<Projectile>().target = currentTarget;
                projectile.GetComponent<Projectile>().SetProjectileDamage(projectileDamage);
                Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
            }
            else
            {
                currentTarget = null;
                closestEnemy = GetClosestEnemy();
                if (closestEnemy != null && closestEnemy.gameObject.activeInHierarchy)
                {
                    
                    currentTarget = closestEnemy.gameObject;
                    //Debug.Log(closestEnemy.name);
                    projectile.GetComponent<Projectile>().target = currentTarget;
                    projectile.GetComponent<Projectile>().SetProjectileDamage(projectileDamage);
                    Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);


                }
                else
                {
                    currentTarget = null;
                }
            }

            yield return new WaitForSeconds(timeBetween/2);
        }
    }


    IEnumerator SiegeSetup()
    {
        
        castle.GetComponent<FollowPath>()._blocked = true;
        castle.GetComponent<FollowPath>()._isMoving = false;
        yield return new WaitForSeconds(1.5f);
        siegeMode = true;
    }

    private Transform[] collidersToTransforms(Collider[] colliders)
    {
        Transform[] transforms = new Transform[colliders.Length];
        for(int i = 0; i < colliders.Length; i++)
        {
            transforms[i] = colliders[i].transform;
        }
        return transforms;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    private float getDistanceSqr(Vector3 initialPoint, Vector3 targetPoint)
    {
        Vector3 directionToTarget = targetPoint - initialPoint;
        return directionToTarget.sqrMagnitude;
    }
}
