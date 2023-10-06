using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CASTLETUTORIALEVENT : MonoBehaviour
{
    /// <summary>
    /// Used just to get collider for when it collides with player to send to scripted events hub
    /// 
    /// Array will start with the nodeSelection.size - 1 = final castle
    /// 
    /// </summary>
    /// 
    //public GameObject node;
    public GameObject scriptedEventsHub;
    [Header ("Determine which node this is attatched to")]
    public bool[] nodeSelection = new bool[3];
    //private int _nodeCount;

    //private bool
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "castlePlayer" && nodeSelection[0])
        {
            //final castle
            scriptedEventsHub.GetComponent<SCRIPTEDEVENTSHUB>().stoppedAtCastle = true;
        }
        if(other.tag == "castlePlayer" && nodeSelection[1])
        {
            //node 1
///Debug.Log("Node1");
            scriptedEventsHub.GetComponent<SCRIPTEDEVENTSHUB>().finalNode1 = true;
        }
        if (other.tag == "castlePlayer" && nodeSelection[2])
        {
            //node 2
            //Debug.Log("Node2");
            scriptedEventsHub.GetComponent<SCRIPTEDEVENTSHUB>().finalNode2 = true;
        }
    }
}
