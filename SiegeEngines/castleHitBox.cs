using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castleHitBox : MonoBehaviour
{
    //siege engines to damage to parts of castle
    public GameObject castleMain;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ExplosiveBarrels")
        {
            castleMain.GetComponent<CastleHealth>().gameData.castleHealth = castleMain.GetComponent<CastleHealth>().gameData.castleHealth - other.gameObject.GetComponent<SiegeTowerBarrels>().barrelDamage;
            //trigger detonation sequence
            other.gameObject.GetComponent<SiegeTowerBarrels>().BeginDetonation();
            castleMain.GetComponent<CastleHealth>().damageIndication();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ExplosiveBarrels")
        {
            //Debug.Log("hit");
            castleMain.GetComponent<CastleHealth>().gameData.castleHealth = castleMain.GetComponent<CastleHealth>().gameData.castleHealth - other.GetComponent<SiegeTowerBarrels>().barrelDamage;
            //trigger detonation sequence
            other.gameObject.GetComponent<SiegeTowerBarrels>().BeginDetonation();
            castleMain.GetComponent<CastleHealth>().damageIndication();
        }
    }
}
