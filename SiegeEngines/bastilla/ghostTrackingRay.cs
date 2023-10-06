using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostTrackingRay : MonoBehaviour
{
    /// <summary>
    /// Since the ghost tracker will have its own rotation, when ghost tracking is active, it will shoot out raycast at target.
    /// 
    /// if the ghost tracker's ray makes contract with the castle, and there is nothing obscuring the view to the obj,
    /// it will allow the bastilla to actually start firing since it is not in range and in sight
    /// 
    /// 
    /// </summary>
    
        //will need to drag in bastillaTurretObj
    public GameObject bastillaMain;

    private float _fireRange;

    // Start is called before the first frame update
    void Start()
    {
        _fireRange = bastillaMain.GetComponent<SiegeBastilla>().bastillaRange;
    }

    //raycasts have physics so use fixed
    void FixedUpdate()
    {
        RaycastHit lineOfSight;
        //while active we will shoot ray at castle to establish line of sight
        if (bastillaMain.GetComponent<SiegeBastilla>()._ghostTracking)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * _fireRange;
            //to see it in scene for debugging
            Debug.DrawRay(transform.position, forward, Color.green);
            //if we hit something with raycast. 
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out lineOfSight, Mathf.Infinity))// && lineOfSight.collider.tag != "wall") //tags are causing problems
            {
                //check we are not looking at a wall
                if (lineOfSight.collider.tag != "wall")
                {
                    //begin attack sequence
                    //Debug.Log("ATTACK");
                    bastillaMain.GetComponent<SiegeBastilla>()._canAttack = true;
                }
                else
                    bastillaMain.GetComponent<SiegeBastilla>()._canAttack = false;
                //if we loose line of sight, it needs to swtich back
            }
        }
    }
}
