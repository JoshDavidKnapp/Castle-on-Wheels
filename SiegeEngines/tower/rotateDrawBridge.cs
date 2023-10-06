using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateDrawBridge : MonoBehaviour
{
    //while bool is on, rotate bridge until it hits the obj, then turn off
    //bool is turned on from enemySiegeTowerAI

    public bool rotateBridge = false;
    public float rotateSpeed = 10f;

    public GameObject towerObj;
        

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rotateBridge)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "BridgeMovePoint")
        {
            rotateBridge = false;
            towerObj.GetComponent<EnemySiegeTowerAI>()._moveBarrier = false;
            towerObj.GetComponent<EnemySiegeTowerAI>().StartPhase2();

        }
    }
}
