using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeTowerBarrels : MonoBehaviour
{
    [Header ("Explosive Barrels stats")]
    public int barrelDamage = 7;
    public float detonationTime = 8f;
    private bool _isDone = false;

    public Behaviour halo;

    [Header("Must be The Path to Be Followed")]
    public DrawPath pathToFollow;

    public float speed = 5; //changed for testing
    public float rotationSpeed = 5.0f;

    private float _reachDistance = 1.0f;

    private int _currentIndex = 0;

    public void Awake()
    {
        //set path

    }

    public void Start()
    {
        halo.enabled = false;
        //will also detonate if it doesnt hit anything
        StartCoroutine(Countdown());
    }

    public void FixedUpdate()
    {
        //slowly moves forward and down to similate gravity
        //transform.Translate(Vector3.left * Time.deltaTime * 2f);

        //get downward vector3 from towerObj


        //instead of gravity, it moves downward (always downward, even on slopes)
        //down (local scale, left axis)
        //this.GetComponent<Rigidbody>().AddForce(-transform.right * 5f);
        //slight force forward by world scale
        //this.GetComponent<Rigidbody>().AddForce(-Vector3.forward);
        transform.Rotate(Vector3.forward * Time.deltaTime);

        doMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "castleHitBox")
        {
            //take health from castle then destory itself
            //other.GetComponent<Castle>().health -= barrelDamage;
            //Debug.Log("barrel detonated");
        }
        if(collision.gameObject.name == "Castle")
        {
            //Debug.Log("barrel detonated");
        }
        //Debug.Log("hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "castleHitBox")
        {
            //take health from castle then destory itself
            //other.GetComponent<Castle>().health -= barrelDamage;
            //Debug.Log("barrel detonated");
        }
        //Debug.Log("hit " + other.name);
    }

    //called from castle script
    public void BeginDetonation()
    {
        StartCoroutine(Detonation());
    }

    private void doMovement()
    {
        float distance = Vector3.Distance(pathToFollow.pathPoints[_currentIndex].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.pathPoints[_currentIndex].position, Time.deltaTime * speed);


        if (pathToFollow.pathPoints[_currentIndex].position - transform.position != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(pathToFollow.pathPoints[_currentIndex].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }



        if (distance <= _reachDistance && _currentIndex < pathToFollow.pathPoints.Count - 1)
        {
            if (!_isDone)
            {
                _currentIndex++;
                // Debug.Log(_currentIndex);
            }
            else
            {
                _isDone = true;


            }

        }

    }

    //if for some reason we miss the castle and the barrles land on the floor or something,
    //they will stay alive momentarly before destroying themselves
    IEnumerator Detonation()
    {
        bool change = false;
        //switches on and off many times
        for(int timesBlink = 0; timesBlink < 5; timesBlink++)
        {
            yield return new WaitForSeconds(0.06f);
            if(change)
            {
                halo.enabled = true;
                change = false;
                //yield return new WaitForSeconds(0.03f);
            }
            else
            {
                halo.enabled = false;
                change = true;
            }
        }
        Destroy(this.gameObject);
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(detonationTime);
        StartCoroutine(Detonation());
    }

    //when we hit something, the barrel will detonate in a cool explosion. We will activate and deactive
    //the halo component on the barrel

   
}
