using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingController : MonoBehaviour
{

    public float targetDistance    = .005f;
    public float patrollingSpeed   = 1f;
    public List<PatrollingPointController> targets;


    private ZombieController zombie;
    private Animator   animator;
    private NavMeshAgent nav;
    private PatrollingPointController currentTarget;
    private int currentTargetI;
    private bool isWalking;
    private bool isPatrolling;

    private void Awake()
    {
        nav      = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        zombie   = GetComponent<ZombieController>();
        isPatrolling = true;

        currentTargetI = 0;
        nav.speed = patrollingSpeed;
    }

    private void Start()
    {
        currentTarget = targets[0];
        WaitInPosition();
        StartCoroutine(StartWalkingWithDelay(1f));
    }

    void Update()
    {
        if(!isPatrolling)
            return;

        if (isWalking && zombie.IsAlive()) {
            if(isAwayFromTarget())
            {
                MoveToTarget();
            }
            else if(continueWalking())
            {
                SetNextTarget();
            }
            else
            {
                WaitInPosition();
                StartCoroutine(StartWalkingWithDelay(currentTarget.waitHereSecs));
                SetNextTarget();
            }
        }
        else if (isWalking)
        {
            WaitInPosition();
        }
    }

    private bool continueWalking()
    {
        return !currentTarget.waitHere;
    }

    private void MoveToTarget()
    {
        nav.SetDestination(currentTarget.transform.position);
    }

    private void SetNextTarget()
    {
        currentTargetI = (currentTargetI + 1) % targets.Count;
        currentTarget  = targets[currentTargetI];
    }

    private bool isAwayFromTarget()
    {
        return targetDistance < Vector3.Distance(transform.position, currentTarget.transform.position);
    }

    private void SetWalking(bool state)
    {
        animator.SetBool("Walking", isWalking = state);
    }

    private IEnumerator StartWalkingWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SetWalking(true);
    }

    private void WaitInPosition()
    {
        nav.SetDestination(transform.position);
        SetWalking(false);
    }

    private IEnumerator StartPatrollingWithDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        isPatrolling = true;
    }

    public void StopPatrollingForAWhile()
    {
        isPatrolling = false;
        StartPatrollingWithDelay();
    }

    public bool IsPAtrolling()
    {
        return isPatrolling;
    }
}
