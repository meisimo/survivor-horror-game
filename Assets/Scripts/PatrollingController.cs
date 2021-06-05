using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingController : MonoBehaviour
{

    public float targetDistance    = .005f;
    public float patrollingSpeed   = 1f;
    public List<PatrollingPointController> targets;

    private Animator   animator;
    private NavMeshAgent nav;
    private PatrollingPointController currentTarget;
    private int currentTargetI;
    private bool isWalking;

    private void Awake()
    {
        nav            = GetComponent<NavMeshAgent>();
        animator       = GetComponent<Animator>();
        currentTargetI = 0;
    }

    private void Start()
    {
        currentTarget = targets[0];
        WaitInPosition();
        StartCoroutine(StartWalkingWithDelay(1f));
    }

    void Update()
    {
        if(isWalking && isAwayFromTarget())
        {
            MoveToTarget();
        }
        else if(isWalking && continueWalking())
        {
            SetNextTarget();
        }
        else if(isWalking)
        {
            WaitInPosition();
            StartCoroutine(StartWalkingWithDelay(currentTarget.waitHereSecs));
            SetNextTarget();
        }
    }

    private bool continueWalking()
    {
        return !currentTarget.waitHere;
    }

    private void MoveToTarget()
    {
        nav.speed = patrollingSpeed;
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
}
