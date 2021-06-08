﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDie : MonoBehaviour, Dieble
{
    static int pointByDying = 5;

    private NavMeshAgent nav;
    private bool frontShootImpact;
    private float desapearDelay = 10f;

    private bool isDead;
    private Animator animator;
    private void Awake() {
        nav      = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isDead   = false;
    }
    
    public void SetFrontShoot(bool front)
    {
        frontShootImpact = front;
    }

    public void Die()
    {
        if(!IsDead())
        {
            isDead = true;
            nav.SetDestination(transform.position);
            animator.SetBool("Front shoot", frontShootImpact);
            animator.SetBool("Die", true);
            StartCoroutine(DesapearAfterDie(desapearDelay));
            LevelManagerController.IncreasePoints(5);
        }
    }

    public IEnumerator DesapearAfterDie(float delay){
        yield return new WaitForSecondsRealtime(delay);
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return isDead;
    }
}
