using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    [Header("This is How fast the Projectile will Travel")]
    public float speed = 10f;
    Vector3 enemyPos;
    Vector3 moveTowardsPos;
    Vector3 startPos;
    Vector3 nextPos;
    [Header("The Height of the Furthest point of the Arc")]
    public float arcHeight = 2f;

    [Header("Explosion Effect")]
    public ParticleSystem explosionEffect;

    public int projectileDamage;

    public bool isCannonBall;
    public bool isBastillaShot = false;

    public float timeToDestory = 10f;
    // Start is called before the first frame update
    void Start()
    {
        enemyPos = target.transform.position;
        startPos = transform.position;
        Destroy(gameObject, timeToDestory);
        moveTowardsPos = target.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
       // if (target == null)
        //{
            //Debug.Log("Destroy");
         //   Destroy(this.gameObject);
        //}
        if (target !=null)
        {
            moveTowardsPos = target.transform.position;
        }
        float x0 = startPos.x;
        float x1 = moveTowardsPos.x;
        float dist = x1 - x0;

        if (target != null)
        {
            moveTowardsPos = target.transform.position;

        }

            Vector3 desired = Vector3.MoveTowards(transform.position, moveTowardsPos, speed * Time.deltaTime);
            float nextX = desired.x;
            float nextZ = desired.z;
            //float nextZ = Mathf.MoveTowards(transform.position.z, z1, (speed-2.5f) * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, enemyPos.y, (nextX - x0) / dist);
            float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            nextPos = new Vector3(nextX, baseY + arc, nextZ);
        
        // Rotate to face the next position, and then move there
        transform.LookAt(nextPos);
        transform.position = nextPos;
                
        // Do something when we reach the target
        
        
    }

    static Quaternion LookAt2D(Vector3 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    public void SetProjectileDamage(int damage)
    {
        projectileDamage = damage;
    }

    public int GetProjectileDamage()
    {
        return projectileDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBastillaShot && this.tag != "enemyProjectile")
        {
            if (other.gameObject.tag == "Structure" || other.gameObject.tag == "SiegeBastilla" || other.gameObject.tag == "SiegeTower" || other.gameObject.tag == "SiegeCannon")
            {
                if (isCannonBall)
                {
                    //Debug.Log(other.name);
                    StartCoroutine(cannonballExplosion());
                }
                else
                    Destroy(gameObject);
            }
        }

        if(target != null)
        {
            if (target.tag == "Ally")
            {
                target.GetComponent<AllyMovement>().health -= projectileDamage;
                target.GetComponent<AllyMovement>().healthBar.startShowBar();
                Destroy(gameObject);
            }
        }
        

    }

    public void Explode()
    {
        //show effect
        StartCoroutine(cannonballExplosion());
    }

    IEnumerator cannonballExplosion()
    {
        //explode, wait, destroy
        //Explode();
        explosionEffect.Play();
        //Debug.Log("Play");
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(3.0f);
        //Debug.Log("stop");
        Destroy(this.gameObject);
    }
    
}
