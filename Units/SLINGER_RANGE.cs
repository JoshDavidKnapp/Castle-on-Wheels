using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLINGER_RANGE : MonoBehaviour
{
    public ENEMY_SLINGER slingerMove;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ally"||other.tag== "castlePlayer")
        {
            slingerMove.currentStatus = status.combat;
            slingerMove.ally = other.gameObject;
            gameObject.SetActive(false);
        }
        


    }
}
