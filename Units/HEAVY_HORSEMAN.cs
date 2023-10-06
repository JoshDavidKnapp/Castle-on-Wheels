using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEAVY_HORSEMAN : AllyMovement
{
    public status currentStatus;
    public int speed;
    

    public override void Update()
    {
        base.Update();
        if (currentStatus == status.moving)
        {
            agent.isStopped = false;
            agent.speed = speed;

            
        }
        if(currentStatus == status.combat)
        {
            agent.isStopped = true;
            
        }
       
    }



}
