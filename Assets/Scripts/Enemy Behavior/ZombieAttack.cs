using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour, AttackingBehavior
{
    public float timeBeetweenAttacks;
    public float distanceToAttack;
    public EnemyWeapon enemyWeapon;

    private Animator animator;
    private float lastAttackSec;
    
    private void Awake() {
        animator      = GetComponent<Animator>();
        lastAttackSec = 0f;
    }

    public void PerformAttack(GameObject target)
    {
        if (lastAttackSec + timeBeetweenAttacks < Time.time){
            lastAttackSec = Time.time;
            enemyWeapon.ActivateDamage();
            animator.SetTrigger("Attack");
        }
    }

    public float DistanceToAttack()
    {
        return distanceToAttack;
    }
    
}
