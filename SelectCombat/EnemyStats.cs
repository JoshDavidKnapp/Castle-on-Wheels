using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyStats : MonoBehaviour
{
    //public GameData gameData;

    public int health;

    public HEALTH_BAR attachedBar;

    private Vector3 pos;

    public NavMeshAgent agent;
    public int arrowDamage;
    public int cannonDamage;

    private bool isDying;


    //private int health;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "grunt(Clone)")
        {
            GetComponent<EnemyMeleeAnimation>().RunAnimation();
        }
        else if (gameObject.name.Contains("Brute"))
        {
            GetComponent<EnemyBruteAnimation>().RunAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isDying)
        {
            if(gameObject.name.Contains("Slinger"))
            {
                agent.speed = 0;
            }
            else
            {
                agent.isStopped = true;
            }
            StartCoroutine(DeathAnim());


        }



    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);

    }

    public IEnumerator DeathAnim()
    {
        isDying = true;
        //Debug.Log("Death");
        if (gameObject.name == "grunt(Clone)")
        {
            GetComponent<EnemyMeleeAnimation>().DeathAnimation();
            yield return new WaitForSeconds(1.9f);
        }
        else if (gameObject.name.Contains("Brute"))
        {
            GetComponent<EnemyBruteAnimation>().DeathAnimation();
            yield return new WaitForSeconds(2.6f);
        }
        else if (gameObject.name.Contains("Slinger"))
        {
            
            GetComponent<EnemySlingerAnimation>().DeathAnimation();
            yield return new WaitForSeconds(2.6f);
        }

        transform.position = new Vector3(0, 0, 0);

        StartCoroutine("Wait");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ally")
        {
            if(gameObject.name == "grunt(Clone)")
            {
                GetComponent<EnemyMeleeAnimation>().AttackAnimation();
                agent.speed = 0;
            }
            else if (gameObject.name.Contains("Brute"))
            {
                GetComponent<EnemyBruteAnimation>().AttackAnimation();
                agent.speed = 0;
            }
            

            
        }
        if(other.tag == "arrow")
        {
            
            health -= arrowDamage;
            attachedBar.startShowBar();
            Destroy(other.gameObject);
            
        }
        if(other.tag == "cannonBall")
        {
            Debug.Log(other.name);
            health -= other.GetComponent<Projectile>().GetProjectileDamage();
            attachedBar.startShowBar();


        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ally")
        {
            if (gameObject.name == "grunt(Clone)")
            {
                GetComponent<EnemyMeleeAnimation>().RunAnimation();
            }
            else if(gameObject.name.Contains("Brute"))
            {
                GetComponent<EnemyBruteAnimation>().RunAnimation();
            }
            agent.speed = 3.5f;

        }

    }
}
