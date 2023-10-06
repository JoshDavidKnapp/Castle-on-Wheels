using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySiegeTowerAI : MonoBehaviour
{
    /// <summary>
    /// DAMAGE TO TOWER STILL NEEDS TO BE IMPLEMENTED
    /// This will always be the parent for the siege gameobjects
    /// 
    /// each one will have this as parent with specifc Siege attributes attached at as child
    /// 
    /// damage done in SiegeTracker script
    /// </summary>

    //[Header("Path to follow")]
    //public DrawPath pathToFollowS;

    //we are moving
    private bool _isMoving;

    //this obj
    private GameObject _childTracker;

   // [Header("Tower variables")]
    //public int health = 100;

    [Header("Player obj")]
    //siege will stop moving and begin attacking when it reaches and collides with castle
    public GameObject castle;

    //will do the tracking, while siege will follow x and z cords (Not to mess with y axis)
    //public GameObject targetingObj;

    [Header("Tower Bridge setup")]
    //when tower makes contact with the castle, it will lower barrier
    public GameObject barrierObj;
    public GameObject barrierTarget;

    //how far we lower brigge
    public float barrierSpeed = 5.0f;

    [HideInInspector]
    public bool _moveBarrier = false;

    //cant repeat the lowering process
    private bool _avoidRepeatBarrier = true;

    [Header("Explosive Barrels")]
    public GameObject barrelsPrefab;
    public GameObject barrelsSpawner;

    //wont lower bridge one
    private bool _avoidRepeatRot = false;

    //how fast we spawn barrels
    public float barrelSpawnSpeed = 2f;
    //path barrels follow
    public DrawPath barrelPath;

    [Header("MovementObj Parent")]
    public GameObject movementObj;

    // Start is called before the first frame update
    void Start()
    {
        _childTracker = this.transform.parent.gameObject;
        castle = movementObj.GetComponent<SiegeTracker>().castleObj;

        
        //for testing this will always be true for launching tower
        _isMoving = true;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move in x and z direction not y so we dont fly up and down
        Vector3 cords = new Vector3(_childTracker.transform.position.x, 6.7f, _childTracker.transform.position.z);

        //this.transform.position = Vector3.MoveTowards(this.transform.position, temp, (speed + 50 * Time.deltaTime));//new Vector3(_childTracker.transform.position.x, 6.7f, _childTracker.transform.position.z);

        //Barrier and attack stuff (activates when tower makes contact with castle
        if (barrierObj.transform.position != barrierTarget.transform.position && _moveBarrier && _avoidRepeatRot == false)
        {
            //only runs once
            barrierObj.GetComponent<rotateDrawBridge>().rotateBridge = true;
            _avoidRepeatRot = true;
        }

        if(_childTracker.GetComponent<SiegeTracker>().health <= 0)
        {
            Debug.Log("turn back on");
            //castle.GetComponent<FollowPath>()._isMoving = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "castlePlayer")
        {
            //Debug.Log("Tower hit castle");            //tracking stop when we reach castle
           _childTracker.gameObject.GetComponent<SiegeTracker>().isMoving = false;
            other.GetComponent<FollowPath>()._isMoving = false;
            //Debug.Log(other.name);
            //begin attack phase
            StartCoroutine(AttackPhase1());
        }
        
    }

    IEnumerator AttackPhase1()
    {
        yield return new WaitForSeconds(1.0f);
        _moveBarrier = true;
    }

    //can be triggered from bridge
    public void StartPhase2()
    {
        Debug.Log("starting attack");
        StartCoroutine(AttackPhase2());
    }

    //actually start damage, will keep attacking until it dies
    public IEnumerator AttackPhase2()
    {
        yield return new WaitForSeconds(barrelSpawnSpeed);
        SpawnBarrels();
        
        //start coroutine to spawn barrels
        StartCoroutine(AttackPhase2());
    }

    private void SpawnBarrels()
    {
        if(movementObj.GetComponent<SiegeTracker>().health > 0)
        {
            GameObject barrel = Instantiate(barrelsPrefab, barrelsSpawner.transform.position, barrelsSpawner.transform.rotation);
            barrel.GetComponent<SiegeTowerBarrels>().pathToFollow = barrelPath;
            //can choose  to have the damage done here or when the barrel prefab collides with the tower
        }

    }


    //UNUSED
    IEnumerator towerDeath()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Tower down");
        //castle.GetComponent<FollowPath>()._isMoving = true;
        Destroy(this.gameObject);
    }


}
