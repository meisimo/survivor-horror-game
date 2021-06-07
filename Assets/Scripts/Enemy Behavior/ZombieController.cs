using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour, Shootable, EnemyWithEyes
{
    private AttackingBehavior attackingBehavior;
    private PursuitBehavior   pursuitBehavior;
    private ParticleSystem fleshExplotion;
    private Animator animator;
    private ZombieDie die;
    private int lifePoints;
    private float fleshExplotionDuration = 0.3f;
    private float reactToShootDelay      = 0.01f;
    private EnemyEyesController enemyEyes;
    private GameObject          target;

    private void Awake() {
        lifePoints = 3;

        fleshExplotion = GetComponentInChildren<ParticleSystem>();
        enemyEyes      = GetComponentInChildren<EnemyEyesController>();

        attackingBehavior = GetComponent<AttackingBehavior>();
        pursuitBehavior   = GetComponent<PursuitBehavior>();

        animator          = GetComponent<Animator>();
        die               = GetComponent<ZombieDie>();

        fleshExplotion.Stop();
    }

    private void FixedUpdate()
    {
        if (enemyEyes.targetDetected)
        {
            TargetPursuit(target);
        }
        else if(pursuitBehavior.IsInPursuit())
        {
            pursuitBehavior.StopPursuit();
        }
    }

    private void TargetPursuit(GameObject target)
    {
        pursuitBehavior.Pursuit(target.transform);
    }

    private IEnumerator FleshExplotionAnimation(Vector3 fleshPoint)
    {
        fleshExplotion.transform.position = fleshPoint;
        fleshExplotion.Play();
        yield return new WaitForSecondsRealtime(fleshExplotionDuration);
        fleshExplotion.Stop();
    }

    private IEnumerator GetShootAnimation()
    {
        yield return new WaitForSecondsRealtime(reactToShootDelay);
        animator.SetTrigger("Get shoot");
    }

    private bool ShootImpactInFront(Vector3 shootPoint)
    {
        return 0 < Vector3.Dot(gameObject.transform.forward, gameObject.transform.InverseTransformPoint(shootPoint));
    }

    private bool TargetCloseToAttack()
    {
        return Vector3.Distance(transform.position, target.transform.position) < attackingBehavior.DistanceToAttack();
    }

    public void GetShoot(Vector3 impactPoint)
    {
        lifePoints--;
        StartCoroutine(FleshExplotionAnimation(impactPoint));
        StartCoroutine(GetShootAnimation());
        
        if (lifePoints <= 0){
            die.SetFrontShoot(ShootImpactInFront(impactPoint));
            die.Die();
        }
    }


    public void TargetDetected(GameObject target)
    {
        this.target = target;
    }

    public bool IsAlive()
    {
        return !die.IsDead();
    }

}
