using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTracking : MonoBehaviour
{
    /// <summary>
    /// THIS IS FOR CASTLE
    /// </summary>
    /// 

    public GameObject cannon;
    public GameObject cannonY;
    
    public GameObject cannonTracker;
    private Quaternion _idle;
    private Quaternion _idleY;

    // Start is called before the first frame update
    void Start()
    {
        _idle = new Quaternion(0, 0, 0, 0);
        _idleY = cannonY.transform.rotation;
        Debug.Log(_idleY);
    }

    // Update is called once per frame
    void Update()
    {
        if(cannon.GetComponent<CastleAttack>().isCannon)
        {
            if(cannon.GetComponent<CastleAttack>().siegeMode && cannon.GetComponent<CastleAttack>().currentTarget)
            {
                //Debug.Log("test");
                //cannon will rotate
                cannonTracker.transform.LookAt(cannon.GetComponent<CastleAttack>().currentTarget.transform);
                //Debug.Log("Tracker: " + cannonTracker.transform.rotation.y);
                //transform.eulerAngles.x = Mathf.Clamp(transform.eulerAngles.y, 0, 0);
                Quaternion tempRotate = new Quaternion(transform.rotation.x, (cannonTracker.transform.rotation.y + 0.3f), transform.rotation.z, 1);
                

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, tempRotate, Time.deltaTime * 1f);
                //Debug.Log("This; " + (transform.rotation.y - 0.1));
                //Quaternion yRot = new Quaternion(cannonTracker.transform.rotation.x, 0, 0, 1);
                //cannonY.transform.rotation = Quaternion.Slerp(cannonY.transform.rotation, yRot, Time.deltaTime * 1f);
            }
            else
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, _idle, Time.deltaTime * 1f);

                //cannonY.transform.rotation = Quaternion.Slerp(cannonY.transform.rotation, _idleY, Time.deltaTime * 1f);
            }
        }
    }
}
