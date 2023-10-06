using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEAVY_HOURSEMAN_ATTACK : MonoBehaviour
{
    public Collider search;
    public GameObject attack;
    public HEAVY_HORSEMAN heavyHorse;
    public int timeBetweenAttack;
    bool _waitForNextAttack;
  
    private void Start()
    {
       
        attack.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Unit" || other.tag == "Structure" || other.tag == "SiegeTower")
        {
            heavyHorse.currentStatus = status.combat;
            search.enabled = false;
            StartCoroutine(attackTimer());
        }


    }
    IEnumerator attackTimer()
    {
        _waitForNextAttack = true;
        GetComponentInParent<AllyHeavyAnimation>().AttackAnimation();
        StartCoroutine(attacklength());
        yield return new WaitForSeconds(1f);
        heavyHorse.currentStatus = status.moving;
        GetComponentInParent<AllyHeavyAnimation>().RunAnimation();
        yield return new WaitForSeconds(timeBetweenAttack);
        search.enabled = true;
        _waitForNextAttack = false;

    }
    IEnumerator attacklength()
    {
        attack.SetActive(true);
        yield return new WaitForSeconds(.3f);
        attack.SetActive(false);
    }
}
