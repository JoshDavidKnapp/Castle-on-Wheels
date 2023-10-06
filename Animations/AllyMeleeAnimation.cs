using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyMeleeAnimation : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.SetBool("isIdling", true);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) < 2f && GetComponent<Animator>().GetBool("isRunning"))
        {
            
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isIdling", true);
            agent.isStopped = true;
        }
    }

    public void RunAnimation()
    {
        animator.SetBool("isIdling", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", true);
        agent.isStopped = false;
        agent.speed = 10;
        
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
