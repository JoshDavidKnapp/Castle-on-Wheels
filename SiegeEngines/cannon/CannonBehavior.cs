using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehavior : MonoBehaviour
{
    /// <summary>
    /// Cannon fires based on whos in radius (including castle)
    /// attacks enemies and castle
    /// no line of sight required
    /// stops moving when engaged
    /// </summary>
    /// 

    bool singleTargetSystem = false;

    //all enemies that enter cannons radius are added to queue, when they leave radius = leave queue
    //public List<GameObject> enemyList;

    //reference to object handling movement
    [Header("Handles Movement")]
    public GameObject cannonMovementObj;
    [Header("Handles tracking")]
    public GameObject tracker;

    //current gameobject target cannon shoots at
    [Header("This Cannons POI")]
    public GameObject _poi;

    //shoots cannon balls at targets
    [Header("What we shootin")]
    public GameObject cannonBallPrefab;

    [Header("Fire rates, max and min")]
    public float fireRateMin = 1f;
    public float fireRateMax = 2.5f;

    //every time we are running fire IEnum, this will be active, will rerun if this ever turns false
    [Header("Can we fire?")]
    public bool _fireActive = false;
    private bool _avoidRepeatedFire = false;

    //when castle is in range, the cannon will move to the side and no longer move forward
    //public bool _castleInRange = false;
    [Header("Stops tracking")]
    public bool _stopTrackingTemp = false;

    private bool _waitToFire = false;

    //public Vector3 _moveLocation;

    [Header("Is cannon active on cart")]
    public bool cannonActive = false;
    [Header("Damage inherited from parent (CannonManagerV2")]
    public int cannonballDamage = 2;

    private void Awake()
    {
        //forwardFace = transform.rotation;
        cannonballDamage = this.transform.parent.gameObject.GetComponent<CannonManagerV2>().cannonballDamage;
    }

    // Update is called once per frame
    void Update()
    {
        //if we dont have a target, keep moving
        if(_poi == null)
        {

            _fireActive = false;
            
        }
        else
        {
            //initialize targeting sequence, always the first gameobject in list
            _fireActive = true;
        }

        if(_stopTrackingTemp == false)
        {
            Targeting();

            if (_fireActive && _avoidRepeatedFire == false && _waitToFire == false)
                StartCoroutine(firingSequence());
        }
            
        //if we take enough damage, it dies a sad, terrible death
       // if (cannonHealth <= 0)
            //Destroy(this.gameObject);

    }

    //poi will always be the first obj in queue
    void Targeting()
    {
        Quaternion temp;
        //is we not tracking, slerp so we face forward
        //if we have not moved over for the castle
        if(cannonMovementObj.GetComponent<SiegeTracker>().moved == false)
        {
            if (_fireActive)//findNextTarget(enemyList) != null) && cannonMovementObj.GetComponent<SiegeTracker>().isMoving == false
            {
                tracker.transform.LookAt(_poi.transform);
                temp = tracker.transform.rotation;
            }
            else
            {
                //Debug.Log(this.name);
                temp = cannonMovementObj.transform.rotation;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, temp, Time.deltaTime * 1f);
        }
        else
        {
            //if we have a target and can fire
            if(_fireActive)
            {
                tracker.transform.LookAt(_poi.transform);
                temp = tracker.transform.rotation;
            }
            else
            {
                temp = cannonMovementObj.transform.rotation;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, temp, Time.deltaTime * 1f);
        }
        
    }

    //will only fire if we are currently targeting something, if we loose the target, dont fire at current poi
    //or if we are already firing
    IEnumerator firingSequence()
    {
        _avoidRepeatedFire = true;
        float fireRate = Random.Range(fireRateMin, fireRateMax);
        yield return new WaitForSeconds(fireRate);
        //one last check to make sure we are on target
        _avoidRepeatedFire = false;
        if (cannonMovementObj.GetComponent<SiegeTracker>().isMoving == false && _waitToFire == false && _fireActive)
        {
            Vector3 pos = transform.position;
            pos.z += 2;
           GameObject cannonball = Instantiate(cannonBallPrefab, pos, transform.rotation);
            cannonball.GetComponent<cannonBallBehavior>().cannonBallDamage = cannonballDamage;
        }
            
    }

    GameObject findNextTarget(List<GameObject> list)
    {
        GameObject target = null;
        for(int count = 0; count <= list.Count - 1; count++)
        {
            if(list[count] != null)
            {
                target = list[count].gameObject;
                return target;
            }
        }
        return target;
    }

    //resume targeting and firing after given time
    IEnumerator MoveToSide()
    {
        Debug.Log("MOVING FOR CASTLE BOI");
        yield return new WaitForSeconds(1.0f);

        _stopTrackingTemp = false;
    }

    //fire cooldown
    IEnumerator waitToFire()
    {
        _waitToFire = true;
        yield return new WaitForSeconds(1.5f);
        _waitToFire = false;
    }


    //NOT BEING USED
    //remove from list
    List<GameObject> removeFromList(List<GameObject> list, GameObject remove)
    {
        //first copy to array
        //then remove selected object
        //then readd everything back to queue (which we return)
        List<GameObject> tempList = new List<GameObject>();
        GameObject[] tempArray = new GameObject[list.Count];
        list.CopyTo(tempArray, 0);

        for (int count = 0; count <= tempArray.Length; count++)
        {
            //if we find the object we trying to remove
            if (tempArray[count] != remove)
            {
                //add only if its not the same object
                tempList.Add(tempArray[count]);
            }
            else
                Debug.Log("found object in queue");
        }

        return tempList;
    }
}
