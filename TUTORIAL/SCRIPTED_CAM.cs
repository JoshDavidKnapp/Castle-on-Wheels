using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRIPTED_CAM : MonoBehaviour
{

    /// <summary>
    /// 
    /// 
    /// CURRENT NOT IN USE
    /// 
    /// 
    /// Will be attacted to a collider that is connected to each barrier.
    /// 
    /// This will be used for each Barrier. When the castle gets to a barrier. The game must show player where the nodes are that they are going to destory.
    /// Camera will first look at main barrier. Then go and move to each node.
    /// 
    /// will activate when player gets in a closer proximity of specific barrier
    /// 
    /// Will require an empty game object attached to cam
    /// </summary>

    [Header("Barrier Obj")]
    public GameObject barrierBase;
    //array of the nodes from barrier base gO
    public GameObject[] barrierNodesC;

    [Header("Camera being moved")]
    public GameObject cameraObj;
    [Header("Main game camera we switch back to")]
    public GameObject mainCamObj;

    [Header("Cameras being manipulated")]
    //should be set to child of cameraObj
    public Camera mainCamera;
    public Camera tutorialCamera;

    [Header("Castle player obj")]
    public GameObject castleObj;
    private GameObject _target;
    private GameObject _lookingTarget;

    [Header("Nodes we move around to")]
    public bool[] _nodeArray;
    [Header("Amount of nodes")]
    public int _nodeCount = 0;

    //temp public
    [Header("While tutorial is moving cam, this is on")]
    public bool _targetingActive = false;
    private bool _started = false;

    [Header("Camera movement speed")]
    public float speed = 10.0f;
    private Transform _targetTrans;

    //test
    private Vector3 _oldCordss2;

    private GameObject _tempTarget;

    //save old cam spot to return to it later
    private Vector3 _oldCords;   

    private bool rotateTarget = false;

    //public string text;
    //public bool _begin = false;
    private bool _preventRepeat1 = false;
    private bool _preventRepeat2 = false;
    private bool _preventRepeatCamSwitch = false;
    private bool _preventCamAutoLock = false;

    private bool _preventRepeatRotate = false;
    private bool _reachedDesiredRotation = false;

    private void Start()
    {

        //due to the fact that the info for the array needs to be pulled from an array that is inizialized on start, we need a slight delay in order to pull that info
        StartCoroutine(LateStart());

        //THIS IS FOR TESTING GET RID OF THIS SHIT
        //_oldCords = cameraObj.gameObject.transform.position;
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.25f);
        _started = true;
        //will need access to each Node from the barrierNode array of gameobjects from barrierObj
        barrierNodesC = barrierBase.GetComponent<BREAKABLE_BARRIERS>().barrierNodes;

        //movement will be base on what bool is active, first one is alway the barrier itself and then go down array to go to each node for cam movement
        int size = barrierBase.GetComponent<BREAKABLE_BARRIERS>().barrierNodes.Length + 1;
        _nodeArray = new bool[size];
        //_nodeCount = size;

        //set all values in array to false, since none have started yet
        for (int count = 0; count < _nodeArray.Length - 1; count++)
        {
            _nodeArray[count] = false;
        }
        _nodeArray[0] = true;

        
    }

    private void FixedUpdate()
    {
        if (_started && _targetingActive)
        { 

            //oly runs once
            if (_preventRepeat2 == false)
            {
                _preventRepeat2 = true;
                //disable main cham and switch to our cool camera
                cameraObj.transform.position = mainCamObj.transform.position;
                _oldCords = cameraObj.gameObject.transform.position;
                
                //Debug.Log("main to t");
                mainCamera.enabled = false;
                tutorialCamera.enabled = true;
            }
        
            //assignt the cam node to be the current target (its a child of the node)
            //cam will be fixed and look at _lookingTarget
            if (_nodeCount == 0)
            {
                //Debug.Log(_nodeCount);
                _lookingTarget = barrierBase;
                _target = barrierBase.transform.GetChild(0).gameObject;
                //temp target will be used for a smooth rotation (always set to first node)
                _tempTarget = barrierNodesC[_nodeCount];
            }
            else
            {
                //set target gameobject
                //Debug.Log(_nodeCount - 1);
                //Debug.Log(_nodeCount);
                _lookingTarget = barrierNodesC[_nodeCount - 1];
                
                _target = barrierNodesC[_nodeCount - 1].transform.GetChild(0).gameObject;
                //temp target will be the next gameobject in array (if present), cant go outside of array
                if(_nodeCount <= barrierNodesC.Length - 1)
                    _tempTarget = barrierNodesC[_nodeCount];
            }

            //physical movement to target
            targeting(_target);

             //it it reaches its destination, 
             if (_target.transform.position == cameraObj.transform.position)
             {
                 //StartCoroutine(switchTargets());

                 //switch targets
                 if (_nodeCount <= _nodeArray.Length - 1 && rotateTarget == false) //will only run when not rotating and when we have enough nodes to target
                 {
                    _nodeCount++;
                    //set next bool active
                    _nodeArray = setNextBool(_nodeArray);
                 }
             } 
        }
        else
        {
            //tutorialCamera.enabled = false;
            //mainCamera.enabled = true;
        }

        

        //will == when cam has cycled through all the nodes
        if(_nodeCount == _nodeArray.Length)
        {
            _targetingActive = false;

            //move us back to where we started
            moveUsToStart(_oldCords);

            //formal end of everything
            if(cameraObj.transform.position == _oldCords && _preventRepeatCamSwitch == false)
            {
                //switch cams back
                //Debug.Log("t to main");
                //Debug.Log("End of bar1 tutorial");
                tutorialCamera.enabled = false;
                mainCamera.enabled = true;
                _preventRepeatCamSwitch = true;
                _preventCamAutoLock = true;
            }
            //also deactivate tag so we can continue
            //this.tag = "InactiveTag";
        }

        //will rotate cam so rotation is not crazy, desired rotation is found by

        //when mainCma reaches target, the second cam will be moved to same location with same rotate
        //then second cam will look at next target before it becomes main target
        //we will then run slerp method to move cameras rotate to meet that of second cam (From point a to point b)
        

        if(rotateTarget) //&& _reachedDesiredRotation != true)
        {

            //run this multiple time to rotate towards target
            Transform rotateStart = cameraObj.transform;
            //end target will be an instance on cam

            //cameraObj.transform.rotation = Quaternion.Lerp(rotateStart.rotation, rotateEnd.rotation, Time.deltaTime * speed);
            Vector3 dir = cameraObj.transform.position - _tempTarget.transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);

            //over time********* THIS ONE
            //transform.rotation = Quaternion.Slerp(_tempTarget.transform.rotation, _lookRotation, Time.deltaTime * turn_speed);



            // protected void rotateTowards(Vector3 to)
            //{

            //  Quaternion _lookRotation = Quaternion.LookRotation((to - transform.position).normalized);


        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "castlePlayer" && barrierBase.GetComponent<BREAKABLE_BARRIERS>().cameraShowsNodes == true && _preventRepeat1 == false)
        {
            _preventRepeat1 = true;
            _oldCords = cameraObj.gameObject.transform.position;
            //begin cam showing of each node
            CameraTrackToBarrierSetup();
        }
    }

    void CameraTrackToBarrierSetup()
    {
        _oldCords = cameraObj.gameObject.transform.position;

        _targetingActive = true;
        //first pan to main barrier
        _nodeArray[0] = true; //locks onto first camNode (main node)

        //display text and so forth
        _oldCords = cameraObj.gameObject.transform.position;
        //move cam to be 
        //Debug.Log("Mark location on camMovement");


        //move us to above castle
        //cameraObj.gameObject.transform.position = castleObj.transform.position;
    }

    //sets transform to slowly move towards target
    void targeting(GameObject target)
    {
        if (cameraObj.transform.position != target.transform.position)
        {
            _targetTrans = target.transform;
            float step = speed * Time.deltaTime; //cal distance to move          
            cameraObj.transform.position = Vector3.MoveTowards(cameraObj.transform.position, _targetTrans.position, step); //moves towards target
            //camera also targets objective
            cameraObj.transform.LookAt(_lookingTarget.transform);
        }
    }

    void moveUsToStart(Vector3 target)
    {

        if (cameraObj.transform.position != target && _preventCamAutoLock == false)
        {
            float step = (speed + 10) * Time.deltaTime; //cal distance to move          
            cameraObj.transform.position = Vector3.MoveTowards(cameraObj.transform.position, target, step); //moves towards target
            //camera looks at main barrier object as it moves back
            cameraObj.transform.LookAt(barrierBase.transform);
        }
    }

    bool[] setNextBool(bool[] array)
    {
        for(int count = 0; count <= array.Length; count++)
        {
            if(array[count])
            {
                array[count] = false;
                if(count <= array.Length - 2)
                {
                    array[count + 1] = true;
                }
                break;
            }
        }
        return array;
    }

    
    IEnumerator switchTargets()
    {
        yield return new WaitForSeconds(1.0f);
        rotateTarget = true;
        yield return new WaitForSeconds(2.0f);
        rotateTarget = false;
    }

    
}
