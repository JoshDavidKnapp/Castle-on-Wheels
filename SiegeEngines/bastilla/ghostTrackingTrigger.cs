using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostTrackingTrigger : MonoBehaviour
{
    /// <summary>
    /// OLD: Big collider for ranged tracking. Sets the target when something gets in range, can be units or castle,
    /// whatever is first will be priority - castle
    /// 
    /// originally in an ontrigger enter but could potentially be all in siegebastilla script with new detection (ontriggerenter has been removed from this script)
    /// </summary>
    public GameObject bastilla;
    public float checkRadius = 10f;
    Transform[] _nearbyEnemies;

    private void Update()
    {
        CheckCastle();
    }

    //slight delay so we can move over before engaging
    IEnumerator moveToSide(GameObject target)
    {
        //Debug.Log("moving");
        //i didnt know we can access a script in an object in another script in an object
        bastilla.GetComponent<SiegeBastilla>().bastillaMovementObj.GetComponent<SiegeTracker>().startMovingSide();
        yield return new WaitForSeconds(3.0f);
        bastilla.GetComponent<SiegeBastilla>()._targetObj = target.gameObject;
        bastilla.GetComponent<SiegeBastilla>()._ghostTracking = true;
    }

    void CheckCastle()
    {
        _nearbyEnemies = collidersToTransforms(Physics.OverlapSphere(transform.position, checkRadius));
        foreach (Transform potentialTarget in _nearbyEnemies)
        {
            if (potentialTarget.gameObject.tag == "castlePlayer")
            {
                StartCoroutine(moveToSide(potentialTarget.gameObject));
                break;
            }
        }
    }

    private Transform[] collidersToTransforms(Collider[] colliders)
    {
        Transform[] transforms = new Transform[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            transforms[i] = colliders[i].transform;
        }
        return transforms;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
