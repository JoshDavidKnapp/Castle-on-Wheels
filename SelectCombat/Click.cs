using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    //Layermask Reference
    [SerializeField]
    private LayerMask clickablesLayer;

    //List of Game Objects Reference
    public List<GameObject> selectedObjects;

    //List of Game Objects Reference
    [HideInInspector]
    public List<GameObject> selectableObjects;

    //Vector3 references for mouse positions
    private Vector3 mousePos1;
    private Vector3 mousePos2;

    //boolean for troop selection
    public static bool isSelected = false;

    //y value of mouse position
    private float mouse;

    //boolean for clicking
    public bool canClick = true;

    //Castle gameobject reference
    public GameObject castle;
    //Scriptable Object reference
    public GameData gamedata;

    // Start is called before the first frame update
    void Awake()
    {
        //Create 2 lists of game objects
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //If player hits space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //run Clear selection function
            ClearSelection();
        }

        //sets mouse to the y position of the mouse
        mouse = Input.mousePosition.y;

       
        //if the player is in the clickable zone
        if(mouse>Screen.height/4)
        {
            //the player can click
            canClick = true;
            
        }
        else
        {
            //if not, clear the selection and player can't click
            ClearSelection();
            canClick = false;
        }

        //if player clicks and can click
        if(Input.GetMouseButtonDown(0) && canClick)
        {
            //sets mousepos1 to where the player clicks
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            RaycastHit rayHit;

            //shotts a raycast
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out rayHit,Mathf.Infinity, clickablesLayer ))
            {
               
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();

                //If player hits ctrl
                if (Input.GetKey("left ctrl"))
                {
                    //adds one troop selection to another to control both troop selections at once
                    if (clickOnScript.currentlySelected == false)
                    {
                        selectedObjects.Add(rayHit.collider.gameObject);
                        clickOnScript.currentlySelected = true;
                        clickOnScript.ClickMe();

                    }
                    else
                    {
                        selectedObjects.Remove(rayHit.collider.gameObject);
                        clickOnScript.currentlySelected = false;
                        clickOnScript.ClickMe();
                    }
                }
                else
                {
                    //clear selection and deselects any troops
                    ClearSelection();

                    if (selectedObjects.Count>0)
                    {
                        foreach (GameObject obj in selectedObjects)
                        {
                            obj.GetComponent<ClickOn>().currentlySelected = false;
                            obj.GetComponent<ClickOn>().ClickMe();
                        }
                        
                    }

                    selectedObjects.Add(rayHit.collider.gameObject);
                    clickOnScript.currentlySelected = true;
                    clickOnScript.ClickMe();
                }
                
            }
        }

        //sets the end position of selection and run selected objects
        if(Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if(mousePos1 != mousePos2)
            {
                SelectObjects();
            }
        }
    }

    void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>();

        if(Input.GetKey("left ctrl") == false)
        {
            ClearSelection();
        }

        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        //for each troop, add to the list of selected troops
        foreach(GameObject selectObject in selectableObjects)
        {
            if(selectObject != null)
            {
                if(selectRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    selectedObjects.Add(selectObject);
                    selectObject.GetComponent<ClickOn>().currentlySelected = true;
                    selectObject.GetComponent<ClickOn>().ClickMe();
                }
            }
            else
            {
                remObjects.Add(selectObject);
            }
        }

        //removes objects from the selected objects list
        if(remObjects.Count>0)
        {
            foreach(GameObject rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }
            remObjects.Clear();
        }
    }

    public void Return()
    {
       //Sets the waypoint to the castle
        gamedata.Waypoint = castle.transform.position;

    }



    public void ClearSelection()
    {
        //clears the selected objects list
            foreach (GameObject obj in selectedObjects)
            {
            if (obj != null)
            {
                obj.GetComponent<ClickOn>().currentlySelected = false;
                obj.GetComponent<ClickOn>().ClickMe();
            }
            }
            selectedObjects.Clear();
        

    }
}
