using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePursuit : MonoBehaviour, PursuitBehavior
{
    public float pursuitSpeed;
    public float minimalDistanceToTarget;

    private bool isInPursuit;
    private float speedBeforePursuit;
    private ZombieController zombie;
    private NavMeshAgent nav;
    private Vector3      targetBeforePursuit;
    private Transform    target;
    private Animator     animator;
    private AttackingBehavior attackingBehavior;
    
    private void Awake()
    {
        isInPursuit = false;

        nav      = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackingBehavior = GetComponent<AttackingBehavior>();
        zombie   = GetComponent<ZombieController>();
    }

    void Update()
    {
        if(isInPursuit && zombie.IsAlive())
        {
            if (DistanceToPursuit())
            {
                nav.SetDestination(target.position);
                SetPreparedToAttackAnimation(false);
            }
            else
            {
                nav.SetDestination(transform.position);
                SetPreparedToAttackAnimation(true);
                attackingBehavior.PerformAttack(target.gameObject);
            }
        }
        else if(isInPursuit)
        {
            StopPursuit();
        }
    }

    private bool DistanceToPursuit()
    {
        return minimalDistanceToTarget < Vector3.Distance(target.position, transform.position);
    }

    private void SetPreparedToAttackAnimation(bool state)
    {
        animator.SetBool("Prepared To Attack", state);
    }
    
    private void SetPursuitAnimation(bool state)
    {
        animator.SetBool("On Pursuit", state);
    }

    public void Pursuit(Transform target)
    {
        if(!isInPursuit)
        {
            targetBeforePursuit = nav.destination;
            speedBeforePursuit  = nav.speed;

            isInPursuit = true;
            nav.speed   = pursuitSpeed;

            this.target = target;
            SetPursuitAnimation(true);
        }
    }

    public void StopPursuit()
    {
        this.target = null;
        isInPursuit = false;

        nav.speed = speedBeforePursuit;
        nav.SetDestination(targetBeforePursuit);
        SetPursuitAnimation(false);
    }

    public bool IsInPursuit()
    {
        return isInPursuit;
    }
    
}
