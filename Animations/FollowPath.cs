using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FollowPath : MonoBehaviour
{
    public AudioSource castleWheels;
    [Header("Must be The Path to Be Followed")]
    public DrawPath pathToFollow;

    public float speed = 5; //changed for testing
    public float rotationSpeed = 5.0f;

    private float _reachDistance = 1.0f;

    private int _currentIndex = 0;

    [SerializeField]
    public bool _isMoving;
    private bool _isDone = false;

    private Vector3 _lastPosition;
    private Vector3 _currentPosition;

    public GameData gameData;

    public GameObject cannon;

    //if an enemy or object stands in the castles way, we cannot continue forward
    public bool _blocked = false;

    //castle will stop while it collides with tag from barrier
    public GameObject tutorial;
    private bool _stopped = false;

    // Start is called before the first frame update
    void Awake()
    {
        //pathToFollow = GameObject.Find(pathName).GetComponent<DrawPath>();
        _lastPosition = transform.position;

        //change to start game with castle in stopped position
        _isMoving = false;
        speed = gameData.castleSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (_isMoving)
        {
            doMovement();
        }
    }

    /// <summary>
    /// sets the _isMoving variable to true
    /// </summary>
    public void continueMoving()
    {
        if(!_blocked)
        {
            _isMoving = true;
        }
        
    }

    /// <summary>
    /// Sets the _isMoving variable to false
    /// </summary>
    public void stopMoving()
    {
        _isMoving = false;
        castleWheels.Stop();
    }


    /// <summary>
    /// Moves the Object along the given Path, pathToFollow
    /// </summary>
    private void doMovement()
    {
        float distance = Vector3.Distance(pathToFollow.pathPoints[_currentIndex].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, pathToFollow.pathPoints[_currentIndex].position, Time.deltaTime * speed);


        if (pathToFollow.pathPoints[_currentIndex].position - transform.position != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(pathToFollow.pathPoints[_currentIndex].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }



        if (distance <= _reachDistance && _currentIndex < pathToFollow.pathPoints.Count - 1)
        {
           if (!_isDone)
           {
               _currentIndex++;
              // Debug.Log(_currentIndex);
           }
           else
           {
               _isDone = true;
           }

       }
  
   }
  
   private void OnTriggerEnter(Collider other)
   {
       if (other.tag == "barrierCollider")
       {
           //other.GetComponent<SCRIPTED_CAM>()._begin = true;
           stopMoving();
       }
      // else
      //     continueMoving();
  
       //will stop at the end of each level in front of enemy castle
       if(other.tag == "enemyCastle")
       {
           stopMoving();
       }

       //siege towers
       if(other.tag == "SiegeTower")
        {
            cannon.GetComponent<CastleAttack>().SetSiegeMode();
            cannon.GetComponent<CastleAttack>().siegeTowerInFront = true;
        }
       
  
    }

    //if the blocked object leaves our collider
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "SiegeTower")
        {
            
            cannon.GetComponent<CastleAttack>().siegeTowerInFront = false;
        }
    }
}
