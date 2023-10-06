using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject castle;
    public GameObject focusPoint;

    [Header("Shown In Scene View With Purple Border Lines")]
    public float mapLimitX1 = 450f; //x limit of map
    public float mapLimitX2 = 450f; //x limit of map

    public float mapLimitY1 = -450f; //z limit of map
    public float mapLimitY2 = -450f; //z limit of map
    [Header("Speed at Which the Player Moves")]
    public float cameraMoveSpeed = 40f;
    [Header("Speed at which the Player Can turn the Camera")]
    public float cameraRotationSpeed = 40f;

    [Header("Tutorial Needs to take control of cam at certain times")]
    public GameObject scriptedEventsHub;
    public bool overrideCam;


    private float defaultRotateSpeed;
    private float defaultHeight;

    private Transform _mainTransform;
    private GameObject temp;
    
    private bool _isFocusing = false;
    private bool _isRotating = false;

    private void Awake()
    {
        _mainTransform = this.transform;
        defaultRotateSpeed = cameraRotationSpeed;
        defaultHeight = _mainTransform.position.y;
        Debug.Log(defaultHeight);

        temp = new GameObject("FocusPointer");
        
    }

    // Update is called once per frame
    void Update()
    {

        if (scriptedEventsHub != null)
            overrideCam = scriptedEventsHub.GetComponent<SCRIPTEDEVENTSHUB>().camTutorialActive;
        //players wont have control of cam if tutorial cam stuff is active
       if (!overrideCam)
       {
            doMovement();
       }
    }

    #region Zooming
    private string _zoomAxis = "Mouse ScrollWheel";//Zoom Axis Name
    private float _zoomPos = 0f; //value from 0 to 1, used in Math.Lerp as t


    [Header("Sensitivity of The Scroll Wheel")]
    public float zoomSensitivity = 25f;//Zoom Sensitivity
    [Header("Maximum Zoom Distance")]
    public float maxZoomOut = 75f; //maximum zoom out
    [Header("Minimum Zoom Distance")]
    public float minZoomOut = 30f; //minimnal zoom out



    /// <summary>
    /// scrollWheel holds the value of the Scroll Wheel Input Axis
    /// </summary>
    private float scrollWheel
    {
        get { return -Input.GetAxis(_zoomAxis); }

    }

    


    /// <summary>
    /// Allows User to Zoom when funtion is in Update. Zoom is Clamped by maxHeight and minHeight
    /// </summary>
    private void doZoom()
    {
        
            _zoomPos += scrollWheel * Time.deltaTime * zoomSensitivity;
            _zoomPos = Mathf.Clamp01(_zoomPos);
        
        

        float targetZoom = Mathf.Lerp(minZoomOut, maxZoomOut, _zoomPos);


        gameObject.GetComponent<Camera>().orthographicSize = targetZoom;
        gameObject.GetComponent<Camera>().fieldOfView = targetZoom;
    }

    #endregion



    /// <summary>
    /// Draw Lines in Editor for Level Designers
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(mapLimitX2, 290, mapLimitY2), new Vector3(mapLimitX1, 290, mapLimitY2));
        Gizmos.DrawLine(new Vector3(mapLimitX2, 290, mapLimitY2), new Vector3(mapLimitX2, 290, mapLimitY1));
        Gizmos.DrawLine(new Vector3(mapLimitX2, 290, mapLimitY1), new Vector3(mapLimitX1, 290, mapLimitY1));
        Gizmos.DrawLine(new Vector3(mapLimitX1, 290, mapLimitY1), new Vector3(mapLimitX1, 290, mapLimitY2));
    }

    void doMovement()
    {

        if (!_isFocusing)
        {
             
            move();
            focus();
            rotate();

        }
        else
        {
            focus();
            rotate();
        }

        doZoom();

        LimitCamPos();
        LimitCamRotation();

    }

    /// <summary>
    /// Moves the camera using the WASD keys (W and S = Vertical , A and D = Horizontal)
    /// </summary>
    private void move()
    {
        Vector3 desiredMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        desiredMove *= cameraMoveSpeed * Time.deltaTime;
        desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;

        _mainTransform.Translate(desiredMove, Space.World);
    }

    /// <summary>
    /// Rotates Camera Position Using the "Rotation" axis (Q and E keys)
    /// </summary>
    private void rotate()
    {
        
        if(_isFocusing)
        {
            //_mainTransform.LookAt(castle.transform);
            _mainTransform.RotateAround(castle.transform.position, Vector3.up, -Input.GetAxis("Rotation") * cameraRotationSpeed);
            
            
        }
        else
        {
            _mainTransform.Rotate(Vector3.up, Time.deltaTime * cameraRotationSpeed * Input.GetAxis("Rotation"), Space.World);
        }
         
                
            
            
        
        
    }

    /// <summary>
    /// Focuses and Unfocuses the Castle In the Camera View. 
    /// If focused, the camera will follow the castle, if not focused the camera will rotate normally
    /// </summary>
    private void focus()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("yeet");
            if(_isFocusing)
            {
                cameraRotationSpeed = defaultRotateSpeed;
                _isFocusing = false;
                _mainTransform.SetParent(null);
                _mainTransform.position = new Vector3(_mainTransform.position.x, defaultHeight, _mainTransform.position.z);
                _mainTransform.rotation = Quaternion.Euler(45f, _mainTransform.rotation.eulerAngles.y, _mainTransform.rotation.eulerAngles.z);
            }
            else
            {
                
                cameraRotationSpeed = 1f;
                temp.transform.position = _mainTransform.position;
                temp.transform.rotation = _mainTransform.rotation;

                Transform desiredTransform = temp.transform;
                desiredTransform.position = new Vector3(focusPoint.transform.position.x, focusPoint.transform.position.y, focusPoint.transform.position.z);

                desiredTransform.LookAt(castle.transform);
                desiredTransform.rotation = Quaternion.Euler(45f, desiredTransform.rotation.eulerAngles.y, _mainTransform.rotation.eulerAngles.z);

                StartCoroutine(FocusCastle(desiredTransform));
            }

        }
    }

    /// <summary>
    /// Limits The Camera's Position to within mapLimitX to -mapLimitX, and mapLimitY to -mapLimitY
    /// </summary>
    private void LimitCamPos()
    {
        _mainTransform.position = new Vector3(Mathf.Clamp(_mainTransform.position.x, mapLimitX2, mapLimitX1), _mainTransform.position.y, Mathf.Clamp(_mainTransform.position.z, mapLimitY2, mapLimitY1));
    }

    private void LimitCamRotation()
    {
        _mainTransform.rotation = Quaternion.Euler(45f, _mainTransform.rotation.eulerAngles.y, Mathf.Clamp(_mainTransform.rotation.eulerAngles.y, 0f,0f));
    }

    /// <summary>
    /// Brings the Castle Into Focus
    /// </summary>
    /// <param name="focus"></param>
    /// <returns></returns>
    private IEnumerator FocusCastle(Transform focus)
    {
        _mainTransform.SetParent(castle.transform, true);
        _isFocusing = true;
        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime;
            _mainTransform.position = Vector3.Lerp(_mainTransform.position, focus.position, t);

            Vector3 currentAngle = new Vector3(Mathf.LerpAngle(_mainTransform.rotation.eulerAngles.x, focus.transform.rotation.eulerAngles.x, t),
            Mathf.LerpAngle(_mainTransform.rotation.eulerAngles.y, focus.transform.rotation.eulerAngles.y, t),
            Mathf.LerpAngle(_mainTransform.rotation.eulerAngles.z, focus.transform.rotation.eulerAngles.z, t));

            _zoomPos = Mathf.Lerp(_zoomPos, 0.8f, t);
            _mainTransform.eulerAngles = currentAngle;

            yield return null;
        }
        
        //_mainTransform.LookAt(castle.transform);


    }

    
    
}


