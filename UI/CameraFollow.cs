using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject castle;

    public float cameraHeight = 20.0f;


 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = castle.transform.position;
        pos.y += cameraHeight;
        transform.position = pos;
    }
}
