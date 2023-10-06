using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManagerV2 : MonoBehaviour
{
    /// <summary>
    /// will only use 1 target
    /// </summary>
    

    /// <summary>
    /// - old version had if a cannon detected an enemy, stop tower
    /// - this new one has if any cannon detects an enemy stop tower
    /// </summary>

    //all enemies in range will go in a list and the cannons will take targets from list
    [Header("List of our enemies we can target")]
    public List<GameObject> enemyList;
    [Header("List of all our cannons")]
    //all cannons we are accessing go here
    public List<GameObject> cannonsList;

    [Header("Handles Movement")]
    public GameObject cannonMovementObj;

    //stop all cannons tracking temporarly
    bool _stopTrackingTemp = false;
    bool targetingActive = false;
    bool _assignEnemy = false;

    [Header("Can we add a poi to smaller cannons")]
    public bool canAddPOI = false;
    [Header("Can we assign POI")]
    public bool hasUnassignedEnemy = false;
    [Header("We are targeting POI")]
    public bool cannonsNotTargeting = true;

    [Header("Current Target")]
    public GameObject TargetEnemy;
    
    [Header("Controls Cannon Cart Rotation")]
    public GameObject trackerEngine;

    [Header("Cannon Ball Damage - This overrides the children cannonball dam var")]
    public int cannonballDamage = 2;

    // Update is called once per frame
    void Update()
    {
        cannonsNotTargeting = CheckForMove();
        canAddPOI = CheckForUnassignedCannon();
        //Debug.Log("check2");
        hasUnassignedEnemy = CheckForUnassignedEnemy();

        //Debug.Log("before");
        //can we move?
        if (cannonsNotTargeting && !hasUnassignedEnemy && canAddPOI)
        {
            cannonMovementObj.GetComponent<SiegeTracker>().isMoving = true;
            targetingActive = false;
            cannonMovementObj.GetComponent<SiegeTracker>().targetingActive = false;
        }
        else
        {
            cannonMovementObj.GetComponent<SiegeTracker>().isMoving = false;
            Targeting();
            targetingActive = true;
        }
            
        //we no longer disable targeting based on if we simply stopped, only if we have no targets
        if (cannonMovementObj.GetComponent<SiegeTracker>().moved)
        {
            _stopTrackingTemp = false;


            //cannons can fire now
            AllTargetingTemp(false);
        }

        if (hasUnassignedEnemy && canAddPOI && _assignEnemy)
        {
            //Debug.Log("can assign");
            //if we can assign and unassigned enemy to a cannon that has no current target
            AssignEnemies(TargetEnemy);
        }

        if(cannonMovementObj.GetComponent<SiegeTracker>().movingProcess == false && cannonMovementObj.GetComponent<SiegeTracker>().moved && cannonMovementObj.GetComponent<SiegeTracker>().moved)
        {
            //extend range of cannon
            this.GetComponent<SphereCollider>().radius = 35;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if cannon detects certain tags, will add them to queue
        if (other.tag == "Ally")
        {
            //Debug.Log("found");
            //will need to instead assign other object to first avalible element instead of just adding to end (more optimal)
            enemyList.Add(other.gameObject);
            //Debug.Log("added");
        }

        if (other.tag == "castlePlayer")
        {
            enemyList.Add(other.gameObject);
            //MoveToSide();
            cannonMovementObj.GetComponent<SiegeTracker>().startMovingSide();
            if (cannonMovementObj.GetComponent<SiegeTracker>().moved == false)
            {
                //_stopTrackingTemp = true;
                AllTargetingTemp(true);
                cannonMovementObj.GetComponent<SiegeTracker>().movingProcess = true;
            }
            else
            {
                // _stopTrackingTemp = false;
                AllTargetingTemp(false);
                cannonMovementObj.GetComponent<SiegeTracker>().movingProcess = false;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if this object is in the queue and leaves the target area
        //remove it from queue
        if (enemyList.Contains(other.gameObject))
        {
            enemyList.RemoveAt(enemyList.IndexOf(other.gameObject));
            //wait a sec before resuming fire method
            //StartCoroutine(waitToFire());
        }
    }

    private void AllTargetingTemp(bool tracking)
    {
        for (int count = 0; count <= cannonsList.Count - 1; count++)
        {
            cannonsList[count].GetComponent<CannonBehavior>()._stopTrackingTemp = tracking;
        }
    }

    //will go though list
    //when it finds an element that is not null (has an enemy), it will assign it to a random cannon
    private void AssignEnemies(GameObject enemy)
    {
        //Debug.Log(enemyList.Count);
        if(enemyList.Count > 0)
        {
            for (int element = 0; element <= enemyList.Count - 1; element++)
            {
                //Debug.Log(element);
                if (enemyList[element] != null && TargetEnemy != null)
                {
                    //each cannon gets this target
                    for (int count = 0; count <= cannonsList.Count - 1; count++)
                    {
                        cannonsList[count].GetComponent<CannonBehavior>()._poi = TargetEnemy;
                    }
                    enemyList.RemoveAt(element);
                    _assignEnemy = false;
                    return;
                }
            }
        }
    }

    //poi will always be the first obj in queue
    void Targeting()
    {
        //Debug.Log("test");
        cannonMovementObj.GetComponent<SiegeTracker>().targetingActive = true;
        cannonMovementObj.GetComponent<SiegeTracker>().Target(TargetEnemy);
    }

    //do we have a cannon we can give a target to?
    private bool CheckForUnassignedCannon()
    {
        //bool opening = false;
        for (int element = 0; element <= cannonsList.Count - 1; element++)
        {
            if (cannonsList[element].GetComponent<CannonBehavior>()._poi == null)
            {
                return true;
            }
        }
        return false;
    }

    //do we have a target we can assign to cannons?
    private bool CheckForUnassignedEnemy()
    {
        //Debug.Log(enemyList.Count);
        if (enemyList.Count > 0)
        {
            for (int element = 0; element <= enemyList.Count - 1; element++)
            {
                //Debug.Log(element);
                //if we have to still assign, return true
                //only assigns a new target if we dont have one currently
                if (enemyList[element] != null && TargetEnemy == null)
                {
                    //Debug.Log(element);
                    TargetEnemy = enemyList[element];
                    _assignEnemy = true;
                    return true;
                }
            }
        }
        return false;
    }

    //if none of the cannons have a target they are trying to shoot at, then the siege engine can move
    private bool CheckForMove()
    {
        //Debug.Log(cannonsList.Count);
        for (int element = 0; element <= cannonsList.Count - 1; element++)
        {
            if (cannonsList[element].GetComponent<CannonBehavior>()._poi != null)
                return false;
        }
        return true;
    }

}
