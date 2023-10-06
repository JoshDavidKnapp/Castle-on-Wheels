using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeBastilla : MonoBehaviour
{
    /// <summary>
    /// The bastilla turret is the only gameobject that needs a model, everything is either empty or used as a placeholder so i know where it is
    /// Bastilla must have line of sight before it can fire
    /// 
    /// Will be 2 criteria for when it can shoot at the castle
    /// 1. Must be in a range, sphere collider
    /// 2. A ghost obj must be able to have line of sight with a raycast before the physical turret can track the castle, 
    /// otherwise it could potentially have wall hacks
    /// 
    /// When castle is in range, ghost obj will be tracking castle shootign raycasts to see if it can actually t
    /// track and fire
    /// 
    /// POSSIBLE ISSUES
    /// what happens when it kills what is it currently tracking?
    /// - add fuction to reassign the targetObj in ghostTrackingTrigger if we kill the thing we are tracking
    /// 
    /// may need to adjust rotation of overall bastilla in gameview when we are dealing with one core gameobject the player sees
    /// 
    /// CURRENT BUG:
    /// (NOT IMPORTANT) firing coroutine is running to often cause its in update. 
    /// If the bastilla is constantly brought in and out of cover, it will stack firing methods
    /// caused by fast movement in and out of cover. Shouldnt be a problem if we keep speeds slow
    ///  
    /// </summary>

    [HideInInspector]
    //must be public so ghostTrackingTrigger can access it
    public GameObject _targetObj;

    //main movement obj
    public GameObject bastillaMovementObj;

    //projectile arrow prefab?
    public GameObject projectile;
    //ghost tracker obj (in bastilla obj)
    public GameObject ghostTracker;

    [Header("Bastilla Variables")]
    public float bastillaRange = 1000;
    public int bastillaDamage = 4;
    public float bastillaAttackRate = 2.5f;
    //public float bastillaArrowLingerTime = 3.0f;

    //to prevent callings fire on top of each other
    private bool _preventMisfire = false;

    [HideInInspector]
    //hide these in inspector
    //both need to be accessed from ghostTrackingRay script in bastillaObj
    //tracking is active
    public bool _ghostTracking;
    [HideInInspector]
    // [HideInInspector]
    //can attack if castle is in range and if we have line of sight (ray is hitting castle from ghost tracker)
    public bool _canAttack;

    // Start is called before the first frame update
    void Awake()
    {
        _canAttack = false;
        _ghostTracking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when we have line of sight, it will stop moving and attack
        if(_ghostTracking)
        {
            ghostTracker.transform.LookAt(_targetObj.transform);

            //launch raycast from transform.roation of ghost tracker
            Vector3 initDir = transform.forward;
            Quaternion angleQ = Quaternion.AngleAxis(ghostTracker.transform.rotation.y, Vector3.up);
            Vector3 newVector = angleQ * initDir;

            //line of sight has been established in ghostTrackingRay script and updates _canAttack
            if (_canAttack)
            {
                //stops moving when attacking
                this.transform.parent.gameObject.GetComponent<SiegeTracker>().isMoving = false;
                bastillaRotationMovement();

                if (_preventMisfire == false)
                {
                    //asssign the projectile target
                    projectile.GetComponent<Projectile>().target = _targetObj;
                    StartCoroutine(BastillaAttack());
                }
                //while bool is active, the IEnumerator will not be called again in FixedUpdate
                _preventMisfire = true;

            }
            else //if we can see the target and/or not in range
            {
                //if we have to swtich back, it will continue moving
                this.transform.parent.gameObject.GetComponent<SiegeTracker>().isMoving = true;
                _preventMisfire = false;
            }
        }

        if (bastillaMovementObj.GetComponent<SiegeTracker>().moved)
            bastillaRange = 60;
    }

    //WILL NEED TO ADD BETTER SMOTHING FOR WHEN THE BASTILLA TRACKS CASTLE. WILL NEED SLERP BETWEEN GHOST TRACKER
    //AND ACTUALLY OBJ FIRING PROJECTILES. This needs to be adjusted
    private void bastillaRotationMovement()
    {
        
        Quaternion tempRotate = new Quaternion(ghostTracker.transform.rotation.x, (ghostTracker.transform.rotation.y), ghostTracker.transform.rotation.z, 1);

        transform.rotation = Quaternion.Slerp(transform.rotation, ghostTracker.transform.rotation, Time.deltaTime * 1f);
    }

    //private void OnTriggerEnter(Collider other)
    //{
        //if(other.tag == "castle")
        //{
            //Debug.Log("found target");
            //move to side then attack
            //StartCoroutine(moveToSide(other.gameObject));
        //}
   /// }

    //once we are in range and can see them, bastilla will stop moving and start firing at castle 
    //until line of sight is broken or it is destoyed
    //to avoid this function being called on top of each other a great multitude of times,
    //
    IEnumerator BastillaAttack()
    {
        //Debug.Log("Commence firing...");
        yield return new WaitForSeconds(bastillaAttackRate);
        if (_canAttack)
        {
            //might still change when we fire
            //Debug.Log("Firing...");
            Instantiate(projectile, transform.position, transform.rotation);
            projectile.GetComponent<Projectile>().projectileDamage = bastillaDamage;
            //continueously fire while we can still attack

            StartCoroutine(BastillaAttack());
        }
    }

}
