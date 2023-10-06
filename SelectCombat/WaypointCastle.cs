using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCastle : MonoBehaviour
{

    public GameData gamedata;
    public GameObject castle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        gamedata.Waypoint = castle.transform.position;
    }
}
