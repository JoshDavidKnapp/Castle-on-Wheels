using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dylan Loe
/// 
/// Will have some health and will be one of the nodes required to destroy the wall.
/// Multiple Nodes are required to destory barrier
/// 
/// </summary>
public class BARRIER_NODE : ENEMY_STRUCTURE
{
    //when requirements are met, set objectiveMet in breakableBarriers class to true

    [Header("Click this to deal damage and destroy nodes")]
    public bool objectiveMet = false;

    public GameObject camNode2;

    //[Header("Cursor")]
    private GameObject cursor;
    private bool yDir = false;

    [Header("Time from Pathblocking obj")]
    public float timeDetonation = 1.0f;

    [Header("Number of nodes for pathblockng")]
    public int nodeNum;
    [Header("Parent")]
    public GameObject pathblock;

    [Header("If you want something other than tnt barrels for nodes")]
    public bool enemyStructureHookUp = false;
    public GameObject enemyStructure;

    private void Start()
    {
        if (enemyStructureHookUp && enemyStructure != null)
            health = enemyStructure.GetComponent<ENEMY_STRUCTURE>().health;

        if (cursor != null)
        {
            StartCoroutine(ymove());
        }
        
    }

    private void Update()
    {

        if (enemyStructureHookUp && enemyStructure != null)
            health = enemyStructure.GetComponent<ENEMY_STRUCTURE>().health;

        if (enemyStructureHookUp && enemyStructure.GetComponent<ENEMY_STRUCTURE>().health <= 0)
            OnDeath();

        if (!enemyStructureHookUp && health <= 0)
        {
            OnDeath();
        }

        if (objectiveMet)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.green;
            transform.gameObject.tag = "Inactive";
            this.gameObject.GetComponent<Collider>().enabled = false;
        }

        if(cursor != null)
        {
            //constantly rotates
            if (yDir)
                cursor.transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
            else
                cursor.transform.position += new Vector3(0, -1 * Time.deltaTime, 0);

            cursor.transform.Rotate(0, 1, 0, Space.World);
        }
        
    }

    public override void OnDeath()
    {
        //Debug.Log("Dead");
        objectiveMet = true;
        //this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        transform.gameObject.tag = "Inactive";
        //this.gameObject.GetComponent<Collider>().enabled = false;
        
        pathblock.GetComponent<BREAKABLE_BARRIERS>().DestroyNode(nodeNum, timeDetonation);
        //Debug.Log(nodeNum);
        this.gameObject.SetActive(false);
    }

    IEnumerator ymove()
    {
        yield return new WaitForSeconds(1.0f);
        if (yDir)
            yDir = false;
        else
            yDir = true;

        StartCoroutine(ymove());

    }

    private void OnDestroy()
    {
        if(pathblock.GetComponent<BREAKABLE_BARRIERS>().wallActive)
            OnDeath();
    }
}
