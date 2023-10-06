using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARROW : MonoBehaviour
{

    public GameObject shooter;
    public Vector3 arcPos;
    public GameObject enemy;
    public float timeStart;
    public bool fly = false;
    public int damage;
   
    // Start is called before the first frame update
    void Start()
    {
        
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null && fly)
        {
            Destroy(gameObject);
            shooter.GetComponent<ARCHER_MOVE>().currentStatus = status.moving;
            shooter.GetComponent<ARCHER_MOVE>().agent.speed = 10;
        }
        else if (fly && enemy != null)
        {
            float u = (Time.time - timeStart) / .5f;
            Vector3 p1, p2, p3;
            p1 = (1 - u) * transform.position + u * arcPos;
            p2 = (1 - u) * arcPos + u * enemy.transform.position;

            p3 = (1 - u) * p1 + u * p2;
            transform.LookAt(p3);
            transform.position = p3;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Unit")
        {
            fly = false;
            other.GetComponent<EnemyStats>().health -= damage;
            if (other.GetComponent<EnemyStats>().health <= 0)
            {
                Destroy(other.gameObject);
                shooter.GetComponent<ARCHER_MOVE>().currentStatus = status.moving;
                shooter.GetComponent<ARCHER_MOVE>().agent.speed = 10;

            }
            Destroy(gameObject);
        }
        if(other.tag == "Structure")
        {
            fly = false;
            other.GetComponent<ENEMY_STRUCTURE>().health -= damage;
            other.GetComponent<ENEMY_STRUCTURE>().structureHealth.startShowBar();
            if (other.GetComponent<ENEMY_STRUCTURE>().health <= 0)
            {
                //if(!other.GetComponent<ENEMY_STRUCTURE>().pathBlockingObj)
                    Destroy(other.gameObject);
                shooter.GetComponent<ARCHER_MOVE>().currentStatus = status.moving;
                shooter.GetComponent<ARCHER_MOVE>().agent.speed = 10;

            }
            Destroy(gameObject);
        }
      

    }
}
