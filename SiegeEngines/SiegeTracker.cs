using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SiegeTracker : MonoBehaviour
{
    public HEALTH_BAR healthBar;

    [Header("Engine Type")]
    public engineType engineTypeVar;

    [Header("Path to follow")]
    public DrawPath pathToFollowS;

    [Header("Engine Specs")]
    public float speed;
    public float rotationSpeed = 5.0f;

    //how far it takes for us to find next path point
    private float _reachDistance = 1.0f;
    //current path point
    private int _currentIndex = 0;

    //are we moving
    [Header("Are we moving?")]
    public bool isMoving;
    //we are done moving
    private bool _isDone = false;

    //position cords
    private Vector3 _lastposition;
    private Vector3 _currentPosition;

    //parent tower
    private GameObject _parentSiege;
    //moving sequence active
    private bool _TowerMoveSequence = false;
    //which way we move when we reach engine
    private float _moveDirection;
    //new vector3 of engine
    [Header("New Move Location")]
    public Vector3 _moveLocation;
    //stop moving
    private bool _stopEngine = false;

    //two directions we can go when we reach engine
    [Header("Positions to move out of the way when we reach player")]
    [Header("Not active for Siege Tower")]
    public GameObject leftPos;
    public GameObject rightPos;
    //have we moved when reach castle
    [Header("We moved out of way of player")]
    public bool moved = false;
    //currently looking at castle
    [Header("Targeting POI active")]
    public bool targetingActive = false;
    //rotational tracker
    [Header("Rotational Tracing Obj")]
    public GameObject trackerEngine;

    //will need to update on build of game
    [Header("Player")]
    public GameObject castleObj;

    //only active during moving process
    [Header("We are moving out of way")]
    public bool movingProcess = true;

    [Header("Engine Health")]
    public int health = 100;

    //how close we can get before cannons move to side
    //public float CastleDetectionRange = 50;

    private bool _pauseEngine = true;

    public GameObject wheel1;
    public GameObject wheel2;
    public GameObject wheel3;
    public GameObject wheel4;
    public GameObject bastilaWheels1;
    public GameObject bastilaWheels2;
    public GameObject bastilaWheels3;
    public GameObject bastilaWheels4;

    // Start is called before the first frame update
    void Start()
    {
        _moveDirection = Random.Range(0.0f, 1.0f);
        _currentIndex = pathToFollowS.total - 2;

        //get child obj (each one has specific attributes but all will tell it to stop
        _parentSiege = this.transform.GetChild(0).gameObject;

        _lastposition = transform.position;
        isMoving = true;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving && _TowerMoveSequence == false && _stopEngine == false && targetingActive == false)
            doMovement();

        //some towers must move out of the way of the castle path when in range
        if(_TowerMoveSequence && moved == false && _pauseEngine == false)
        {
            //Debug.Log("moving side");
            transform.position = Vector3.MoveTowards(transform.position, _moveLocation, Time.deltaTime * speed);
            //Debug.Log(_moveLocation - transform.position);
            if (_moveLocation - transform.position != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(_moveLocation - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }

            if(transform.position == _moveLocation)
            {
                moved = true;
                //Debug.Log("new location: " + transform.position);
                _stopEngine = true;
                movingProcess = false;
                //when it reaches its side location, it will no longer run doMovement()
                //extend range of child obj
            }
        }

        if (health <= 0)
        {
            //if looses all its health, it dies
            StartCoroutine(towerDeath());
        }
    }

    //movement triggered from child engines
    public void startMovingSide()
    {

        Vector3 old = transform.position;
        //Debug.Log("Old pos: " + transform.position);
        _moveLocation = transform.position;

        //chooses a random side to move over to (left or right)
        if (_moveDirection <= 0.5f)
        {
            _moveLocation = rightPos.transform.position;
        }
        else
        {
            _moveLocation = leftPos.transform.position;
        }
        _moveLocation.y = old.y;
        StartCoroutine(towerMoveWait());
        //_TowerMoveSequence = true;
    }

    IEnumerator towerMoveWait()
    {
        //_stopEngine = false;
        //isMoving = false;
        _TowerMoveSequence = true;
        yield return new WaitForSeconds(1.0f);
        _pauseEngine = false;
    }

    private void doMovement()
    {
        //Debug.Log(_currentIndex);
        //sets next poi
        float distance = Vector3.Distance(pathToFollowS.pathPoints[_currentIndex].position, transform.position);
        //sets transform to move towards poi
        transform.position = Vector3.MoveTowards(transform.position, pathToFollowS.pathPoints[_currentIndex].position, Time.deltaTime * speed);

        //face towards poi
        if (pathToFollowS.pathPoints[_currentIndex].position - transform.position != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(pathToFollowS.pathPoints[_currentIndex].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        //if the tower gets withing a certain distance with poi and we still have targets to cycle to, cycle targets
        //moving in reverse, starts at end and stops running if _currentIndex is less than the count
        if (distance <= _reachDistance && _currentIndex < pathToFollowS.pathPoints.Count - 1)
        {
            if (!_isDone)
            {
                _currentIndex--;
                //Debug.Log(_currentIndex);
            }
            else
            {
                _isDone = true;
            }
        }

        //rotate wheels
        if(wheel1 != null)
        {
            wheel1.transform.Rotate(90 * Time.deltaTime, 0, 0, Space.Self);
        }
        if (wheel2 != null)
        {
            wheel2.transform.Rotate(90 * Time.deltaTime, 0, 0, Space.Self);
        }
        if (wheel3 != null)
        {
            wheel3.transform.Rotate(90 * Time.deltaTime, 0, 0, Space.Self);
        }
        if (wheel4 != null)
        {
            wheel4.transform.Rotate(90 * Time.deltaTime, 0, 0, Space.Self);
        }
        if (bastilaWheels1 != null)
        {
            bastilaWheels1.GetComponent<BastillaWheelsRotate>().Rotate();
        }
        if (bastilaWheels2 != null)
        {
            bastilaWheels2.GetComponent<BastillaWheelsRotate>().Rotate();
        }
        if (bastilaWheels3 != null)
        {
            bastilaWheels3.GetComponent<BastillaWheelsRotate>().Rotate();
        }
        if (bastilaWheels4 != null)
        {
            bastilaWheels4.GetComponent<BastillaWheelsRotate>().Rotate();
        }
    }

    private void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(castleObj.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    //if we need to target something, only target in x axis
    public void Target(GameObject target)
    {
        if (movingProcess == false)
        {
            float oldX = transform.rotation.x;
            Quaternion temp = transform.rotation;
            //is we not tracking, slerp so we face forward
            //if we have not moved over for the castle
            trackerEngine.transform.LookAt(target.transform);
            temp = trackerEngine.transform.rotation;
            temp.x = oldX;
            transform.rotation = Quaternion.Slerp(transform.rotation, temp, Time.deltaTime * 1f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("yeet");
        //other tags for damage
        if (other.tag == "cannonball")
        {
            Debug.Log("C owie: " + health);
            health = health - other.GetComponent<Projectile>().GetProjectileDamage();
            healthBar.startShowBar();
        }
        if (other.tag == "arrow")
        {
            Debug.Log("A ow: " + health);
            health = health - other.GetComponent<Projectile>().GetProjectileDamage();
            healthBar.startShowBar();
        }

        //Allied damage go here?
        if(other.tag == "Ally")
        {
            health--;
            healthBar.startShowBar();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Ally")
        {
            health--;
            healthBar.startShowBar();

        }
    }

    IEnumerator towerDeath()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Engine down");
        Destroy(this.gameObject);
    }
}
