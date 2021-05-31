using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour, Shootable
{
    private ParticleSystem fleshExplotion;
    private Animator animator;
    private ZombieDie die;
    private int lifePoints;
    private float fleshExplotionDuration = 0.3f;
    private float reactToShootDelay      = 0.01f;

    private void Awake() {
        lifePoints = 3;

        fleshExplotion = GetComponentInChildren<ParticleSystem>();
        animator       = GetComponent<Animator>();
        die            = GetComponent<ZombieDie>();

        fleshExplotion.Stop();
    }

    private void Die()
    {
        Destroy(gameObject);
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

    public void GetShoot(Vector3 impactPoint)
    {
        lifePoints--;
        StartCoroutine(FleshExplotionAnimation(impactPoint));

        if ( 0 < lifePoints)
        {
            StartCoroutine(GetShootAnimation());
        }
        else{
            die.SetFrontShoot(ShootImpactInFront(impactPoint));
            StartCoroutine(die.Die());
        }
    }

}
