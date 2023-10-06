using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEBBLE : MonoBehaviour
{
    public GameData castleData;
    public GameObject shooter;
    public Vector3 arcPos;
    public GameObject ally;
    public float timeStart;
    public bool fly = false;
    public int damage;
    public EnemyData currentData;
    // Start is called before the first frame update
    void Start()
    {
        damage = currentData.sDamage;
        timeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ally == null && fly)
        {
            Destroy(gameObject);
            shooter.GetComponent<ENEMY>().currentStatus = status.moving;
            shooter.GetComponent<ENEMY>().agent.speed = shooter.GetComponent<ENEMY>().speed;
        }
        else if (fly)
        {
            float u = (Time.time - timeStart) / 0.5f;
            Vector3 p1, p2, p3;
            p1 = (1 - u) * transform.position + u * arcPos;
            p2 = (1 - u) * arcPos + u * ally.transform.position;

            p3 = (1 - u) * p1 + u * p2;
            transform.position = p3;
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ally")
        {
            fly = false;
            other.GetComponent<AllyMovement>().health -= damage;
            other.GetComponent<AllyMovement>().healthBar.startShowBar();
            if (other.GetComponent<AllyMovement>().health <= 0)
            {
               
                shooter.GetComponent<ENEMY>().currentStatus = status.moving;
                shooter.GetComponent<ENEMY>().agent.speed = shooter.GetComponent<ENEMY>().speed;

            }
            Destroy(gameObject);
        }
        if(other.tag == "castlePlayer")
        {
           castleData.castleHealth -= damage;
            shooter.GetComponent<ENEMY>().currentStatus = status.moving;
            shooter.GetComponent<ENEMY>().agent.speed = shooter.GetComponent<ENEMY>().speed;
            Destroy(gameObject);
        }
       

    }



}
