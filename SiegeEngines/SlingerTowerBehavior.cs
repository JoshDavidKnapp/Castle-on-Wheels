using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingerTowerBehavior : MonoBehaviour
{
    /// <summary>
    /// variant of castle attack script originally made by jeremy
    /// </summary>
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

    [Header("How long arrows remain stuck in ground (if they miss)")]
    public float arrowDetonationDelay = 2f;

    public int health = 100;

    public Material indicatorMat;
    public Material originalMat;

    //array of materials on castle
    private Material[] _matsSlinger;

    
    //public GameObject slingerTower;

    private void Awake()
    {
        //_matsSlinger = this.gameObject.GetComponent<Renderer>().materials;
        //_matWheels = playerCastleWheels.GetComponent<Renderer>().materials;
    }
    void Start()
    {
        StartCoroutine(DoCheck());
    }

    private void Update()
    {
        if(health <= 0)
        {
            //if we have no health, it dies
            Destroy(this.gameObject);
        }
    }

    //find target
    Transform GetClosestEnemy()
    {
            _nearbyEnemies = collidersToTransforms(Physics.OverlapSphere(transform.position, checkRadius));
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Pow(checkRadius, 2);
            Vector3 currentPosition = transform.position;
            foreach (Transform potentialTarget in _nearbyEnemies)
            {
                if (potentialTarget.gameObject.tag == "Ally" || potentialTarget.gameObject.tag == "castlePlayer")
                {

                    float dSqrToTarget = getDistanceSqr(currentPosition, potentialTarget.position);
                    //Debug.Log("Slinger Found target: " + dSqrToTarget);
                    if (dSqrToTarget < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToTarget;
                        bestTarget = potentialTarget;
                    }
                }
            }
            //Debug.Log(closestDistanceSqr);
            return bestTarget;

    }

    //can we attack?
    IEnumerator DoCheck()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetween / 2);
            Vector3 currentPosition = transform.position;

            if ((currentTarget != null && currentTarget.gameObject.activeInHierarchy) && getDistanceSqr(currentPosition, currentTarget.transform.position) < Mathf.Pow(checkRadius, 2))
            {

                //Debug.Log(closestEnemy.name);
                projectile.GetComponent<Projectile>().target = currentTarget;
                projectile.GetComponent<Projectile>().SetProjectileDamage(projectileDamage);
                projectile.GetComponent<Projectile>().timeToDestory = arrowDetonationDelay;
                Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
            }
            else
            {
                currentTarget = null;
                closestEnemy = GetClosestEnemy();
                if (closestEnemy != null && closestEnemy.gameObject.activeInHierarchy)
                {

                    currentTarget = closestEnemy.gameObject;
                    //Debug.Log(closestEnemy.name + " 2");
                    projectile.GetComponent<Projectile>().target = currentTarget;
                    projectile.GetComponent<Projectile>().SetProjectileDamage(projectileDamage);
                    Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);

                }
                else
                {
                    currentTarget = null;
                }
            }

            yield return new WaitForSeconds(timeBetween / 2);
        }
    }

    private Transform[] collidersToTransforms(Collider[] colliders)
    {
        Transform[] transforms = new Transform[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
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

    //damage will now be done in ENEMY_STRUCUTRE
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "arrow" || other.tag == "cannonball")
        {
            //or how ever much damage we want it to take
            //Debug.Log("sligner script");
            //health = health - other.GetComponent<Projectile>().GetProjectileDamage();
            //StartCoroutine(DamageFlicker());
        }
    }

    IEnumerator DamageFlicker()
    {
        bool colorChange = false;
        for (int flicker = 0; flicker <= 5; flicker++)
        {
            yield return new WaitForSeconds(0.07f);
            if (colorChange == false)
            {
                //change to damange indicated mat (from default)
                _matsSlinger[0] = indicatorMat;
                //playerCastleBase.GetComponent<Renderer>().materials = _matsCastle1;
                //playerCastleWheels.GetComponent<Renderer>().materials = _matWheels;
                //Canon.gameObject.GetComponent<Renderer>().material = damagedMat;
                colorChange = true;
            }
            else
            {
                //from damage to default
                _matsSlinger[0] = originalMat;
                //playerCastleBase.GetComponent<Renderer>().materials = _matsCastle1;
                //playerCastleWheels.GetComponent<Renderer>().materials = _matWheels;
                //Canon.gameObject.GetComponent<Renderer>().material = cannonDefaut1;
                colorChange = false;
            }
        }
    }
}
