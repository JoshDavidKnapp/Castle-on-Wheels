using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ENEMY_UNIT : _UNIT
{
    public Castle castlePos;
    private bool _enterCombat;
    
    // Start is called before the first frame update
    


    // Update is called once per frame
    private void FixedUpdate()
    {
        if(_currentState == unitState.moving&&!_waitToRepath )
        {
           // unitMesh.SetDestination(castlePos.castlePos);
            _waitToRepath = true;
            moveToPos(castlePos.currentPos);
            StartCoroutine(waitToRepath());
            
        }
        
        
    }

}
