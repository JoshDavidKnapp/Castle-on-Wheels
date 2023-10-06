using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonBallBehavior : MonoBehaviour
{
    /// <summary>
    /// will fly forward until it hits something
    /// will explode if it makes contact with anything in a very small controlled explosing
    /// </summary>

     [Header("Cannonball sped")]
    public float speed = 5;

    [Header("Damage value inherited from parent")]
    public int cannonBallDamage = 2;

    [Header("Halo behavior for detonation")]
    public Behaviour halo;

    [Header("Explosion Effect")]
    public ParticleSystem explosionEffect;

    private bool onHit = false;

    private void Start()
    {
        //cannonBallDamage = this.transform.parent.gameObject.GetComponent<CannonManagerV2>().cannonballDamage;
        StartCoroutine(detonationCountdown());
    }

    private void FixedUpdate()
    {
        if(onHit == false)
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "SiegeCannon" && other.tag != "arrow" && other.tag != "cannonball" && other.tag != "Enemy Unit" && other.tag != "Ignore")
        {
            StartCoroutine(Detonation());
            onHit = true;
            //Debug.Log(other.name);
        }
            
        if(other.tag == "Ally")
        {
            //Debug.Log("hit");
            other.gameObject.GetComponent<AllyMovement>().health -= (int)cannonBallDamage;
        }

    }

    void Explode()
    {
        //show effect
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionEffect.Play();
    }

    IEnumerator Detonation()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        //this.GetComponent<Rigidbody>().isKinematic = true;
        //bool change = false;
        //explode, wait, destroy
        Explode();
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    IEnumerator detonationCountdown()
    {
        yield return new WaitForSeconds(7.0f);
        Detonation();
    }
}
