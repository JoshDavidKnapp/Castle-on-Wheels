using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCHER_RANGE : MonoBehaviour
{
    public ARCHER_MOVE archerMove;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Unit"||other.tag == "Structure" )
        {
            archerMove.currentStatus = status.combat;
            archerMove.enemy = other.gameObject;
            gameObject.SetActive(false);
        }
        



    }
}
