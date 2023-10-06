using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CASTLE_POS : MonoBehaviour
{
    public Castle castle;

    // Update is called once per frame
    void Update()
    {
        castle.currentPos = transform.position;
    }
}
