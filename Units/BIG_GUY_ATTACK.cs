using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIG_GUY_ATTACK : MonoBehaviour
{
    public Collider search;
    public GameObject attack;
    public ENEMY brute;
    public int timeBetweenAttack;
    bool _waitForNextAttack;
    public EnemyData currentData;
    private void Start()
    {
        timeBetweenAttack = currentData.bTimeBetweenAttacks;
        attack.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ally"|| other.tag == "castlePlayer")
        {
            brute.currentStatus = status.combat;
            search.enabled = false;
            StartCoroutine(attackTimer());
        }


    }
    IEnumerator attackTimer()
    {
        _waitForNextAttack = true;
        GetComponentInParent<EnemyBruteAnimation>().AttackAnimation();
        StartCoroutine(attacklength());
        yield return new WaitForSeconds(1f);
        brute.currentStatus = status.moving;
        GetComponentInParent<EnemyBruteAnimation>().RunAnimation();
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
