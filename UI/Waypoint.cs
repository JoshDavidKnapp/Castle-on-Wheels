using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    RaycastHit hit;
    public GameObject waypointIcon;
    public GameData gameData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectWaypoint();
        }


    }


    public void SelectWaypoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            

            var hitPoint = hit.point;

            Vector3 clickedPosition = hitPoint;

            if(DeleteAfterSpawn.canSet == true)
            {
                gameData.Waypoint = clickedPosition;

                Instantiate(waypointIcon, hitPoint, Quaternion.identity);

                DeleteAfterSpawn.canSet = false;
            }
           

            
            
            

            print(clickedPosition);
            print(gameData.Waypoint);


        }

    }


    
}
