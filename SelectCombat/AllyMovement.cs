using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyMovement : MonoBehaviour
{
    public EnemyData currentData;

    public HEALTH_BAR healthBar;

    public GameData gameData;
    //public Vector3 waypoint;

    public bool EnteredTrigger = false;

    public NavMeshAgent agent;

    //public Camera cam;

    public int health;

    public int bruteDamage;

    public GameObject waypoint;

    public Quaternion quart;

    public bool isWaypoint = false;

    public Click clickScript;

    //GameObject newWaypoint;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject camera = GameObject.Find("Main Camera");
        clickScript = camera.GetComponent<Click>();

        //clickScript.selectedObjects
        

        bruteDamage = currentData.bDamage;
        

        //gameData.Waypoint = transform.position;

        //waypoint = gameData.Waypoint;
    }

    // Update is called once per frame
    public virtual void Update()
    {

        
        
        if (health <= 0)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            StartCoroutine(DeathAnim());

            

        }

        if (Input.GetMouseButtonDown(1) && Input.mousePosition.y > 170)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            

            if (Physics.Raycast(ray, out hit))
            {
               foreach(GameObject thing in clickScript.selectedObjects)
                {
                    thing.GetComponent<NavMeshAgent>().SetDestination(hit.point);

                    if(thing.name == "Militia(Clone)")
                    {
                        thing.GetComponent<AllyMeleeAnimation>().RunAnimation();
                    }
                    else if(thing.name == "Archer(Clone)")
                    {
                        thing.GetComponent<AllyArcherAnimation>().RunAnimation();
                    }
                    else if(thing.name == "HeavyHorseman(Clone)")
                    {
                        thing.GetComponent<AllyHeavyAnimation>().RunAnimation();
                    }
                    
                    
                }
                //agent.SetDestination(hit.point);

                



                /*
                if(newWaypoint != null)
                {
                    Destroy(newWaypoint);
                }
                */
                //newWaypoint.transform.position = gameData.Waypoint;
                //newWaypoint = Instantiate(waypoint);

                //StartCoroutine("WPDestroy");
            }
        }
        //waypoint = gameData.Waypoint;
        //transform.position = Vector3.MoveTowards(transform.position, waypoint, 1f);
        
    }
    public IEnumerator Damage()
    {
        
        health--;
        healthBar.startShowBar();
        yield return new WaitForSeconds(0.5f);
    }
   

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);

    }

    public IEnumerator DeathAnim()
    {
        //Debug.Log("Death");
        if (gameObject.name == "Militia(Clone)")
        {
            GetComponent<AllyMeleeAnimation>().DeathAnimation();
            yield return new WaitForSeconds(1.6f);
        }
        else if (gameObject.name == "Archer(Clone)")
        {
            GetComponent<AllyArcherAnimation>().DeathAnimation();
            yield return new WaitForSeconds(1.6f);
        }
        else if (gameObject.name == "HeavyHorseman(Clone)")
        {
            
            GetComponent<AllyHeavyAnimation>().DeathAnimation();
            yield return new WaitForSeconds(2.2f);
        }
        
        transform.position = new Vector3(0, 0, 0);

        StartCoroutine("Wait");
    }
   
    /*
    
    public IEnumerator WPDestroy()
    {
         yield return new WaitForSeconds(1);
         //Destroy(new);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy Unit")
        {
            EnemyStats hp = other.gameObject.GetComponent<EnemyStats>();
            hp.health--;
            hp.attachedBar.startShowBar();
            print("COLLIDED");
            agent.speed = 0;
            StartCoroutine("Damage");
            
        }

        if (other.gameObject.tag == "Stronghold")
        {
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

        //cannonballs damage handled in cannonball script
    }

     private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy Unit")
        {
            print("EXITED");
            agent.speed = 10;
            clickScript.ClearSelection();
        }

        if(other.gameObject.tag == "Stronghold")
        {
            StopCoroutine(Damage());

        }

    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    public IEnumerator Damage()
    {

        health--;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);

    }
   
    */

}
