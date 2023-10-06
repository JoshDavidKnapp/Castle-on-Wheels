using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ENEMY_BIG_GUY_SPAWN : MonoBehaviour
{
    public GameObject enemy, firstSpawn;
    
    [Header("Time Between Spawns")]
    public float timeBetweenSpawns;

    public TextMesh timerText;

    [Header("Can Spawn")]
    [Tooltip("keeps the spawner from spawning unless true")]
    public bool canSpawn;
    bool _posBlocked;
    int _spawnPos = 0;


    //prox spawner stuff
    private GameObject castlePlayer;
    public Vector3 distanceFromPlayer;
    public float distance;

    public bool proxActive = true;

    [Header("Poximity Ranges")]
    [Tooltip("Turns off after FarestRange")]
    public int closerRange = 350;
    public int farestRange = 450;

    Transform[] _nearbyEnemies;

    public bool inRange = false;

    private void Awake()
    {
        //locate player for distance check
        castlePlayer = GameObject.Find("Castle Complete");
    }

    private void FixedUpdate()
    {

        if (!_posBlocked && canSpawn)
        {

            StartCoroutine(spawn());


        }
        //timerText.transform.LookAt(Camera.main.transform);
        //timerText.transform.Rotate(new Vector3(0, 180, 0));

        //if (proxActive)
        //CheckCastleDistance();

        inRange = CheckAlliedDistance();

        if (proxActive && inRange)
        {
            canSpawn = true;
        }
        else
            canSpawn = false;
    }


    IEnumerator spawn()
    {
        _posBlocked = true;
        GameObject NewEnemy = Instantiate(enemy);
        NewEnemy.GetComponent<NavMeshAgent>().Warp(firstSpawn.transform.position);
        NewEnemy.GetComponent<ENEMY>().currentStatus = status.moving;

        yield return new WaitForSeconds(timeBetweenSpawns);
       
        _posBlocked = false;

    }

    IEnumerator timer()
    {
        float counter = timeBetweenSpawns;
        timerText.text = counter.ToString();
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            int cast = (int)counter;
            if (cast >= 0)
                timerText.text = cast.ToString();
        }
    }

    bool CheckAlliedDistance()
    {
        bool spawn = false;

        _nearbyEnemies = collidersToTransforms(Physics.OverlapSphere(transform.position, farestRange));
        foreach (Transform potentialTarget in _nearbyEnemies)
        {
            if (potentialTarget.gameObject.tag == "Ally" || potentialTarget.gameObject.tag == "castlePlayer")
            {
                //Debug.Log(this.gameObject.name + " Farest: " + farestRange);
                spawn = true;
                //buffer of 10 seconds for good measure
                timeBetweenSpawns = (distanceFromPlayer.magnitude / 10) + 10;
                break;
            }
        }

        _nearbyEnemies = collidersToTransforms(Physics.OverlapSphere(transform.position, closerRange));
        foreach (Transform potentialTarget in _nearbyEnemies)
        {
            if (potentialTarget.gameObject.tag == "Ally" || potentialTarget.gameObject.tag == "castlePlayer")
            {
                //Debug.Log(this.gameObject.name + "Closest " + closerRange);
                spawn = true;
                timeBetweenSpawns = (distanceFromPlayer.magnitude / 10) + 5;
                break;
            }
        }

        return spawn;
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

    //not in use
    void CheckCastleDistance()
    {
        //determine distance from castle
        distanceFromPlayer = this.transform.position - castlePlayer.transform.position;
        distance = distanceFromPlayer.magnitude;

        if (distanceFromPlayer.magnitude > 0 && distanceFromPlayer.magnitude < closerRange)
        {
            canSpawn = true;
            timeBetweenSpawns = distanceFromPlayer.magnitude / 10;
        }
        else if (distanceFromPlayer.magnitude > closerRange && distanceFromPlayer.magnitude < farestRange)
        {
            canSpawn = true;
            //buffer of 10 seconds for good measure
            timeBetweenSpawns = (distanceFromPlayer.magnitude / 10) + 10;
        }
        else if (distanceFromPlayer.magnitude > farestRange)
        {
            //else //otherwise turn spawner off
            canSpawn = false;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, closerRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, farestRange);
    }
}
