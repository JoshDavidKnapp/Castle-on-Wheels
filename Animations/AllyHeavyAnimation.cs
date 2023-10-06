using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyHeavyAnimation : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 4f && animator.GetBool("isRunning") && agent.isStopped == false)
        {

            animator.SetBool("isRunning", false);
            animator.SetBool("isIdling", true);
            agent.isStopped = true;
        }
    }

    public void RunAnimation()
    {
        // animator.SetBool("isIdle", false);
        // animator.SetBool("isBreathing", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isIdling", false);
        animator.SetBool("isRunning", true);
        agent.isStopped = false;
        agent.speed = 12;

    }

    public void AttackAnimation()
    {
        // animator.SetBool("isIdle", false);
        // animator.SetBool("isBreathing", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
    }

    public void IdleAnimation()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", false);
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
