using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySlingerAnimation : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void RunAnimation()
    {
        animator.SetBool("isIdling", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", true);


    }

    public void AttackAnimation()
    {
        animator.SetBool("isIdling", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);


    }

    public void IdleAnimation()
    {

        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isIdling", true);
    }

    public void DeathAnimation()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdling", false);
        animator.SetBool("isDying", true);
    }
}
