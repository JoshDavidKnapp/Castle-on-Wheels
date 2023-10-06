using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIER_TRIGGER : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "castlePlayer")
        {
            ALLIED_UNIT_LIST.S.raiseTier();
        }
    }


}
