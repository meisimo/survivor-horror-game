using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDie : MonoBehaviour, Dieble
{
    private bool frontShootImpact;
    private float desapearDelay = 10f;

    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    
    public void SetFrontShoot(bool front)
    {
        frontShootImpact = front;
    }

    public IEnumerator Die()
    {
        animator.SetBool("Front shoot", frontShootImpact);
        yield return new WaitForSecondsRealtime(0.0f);
        animator.SetTrigger("Die");
        StartCoroutine(DesapearAfterDie(desapearDelay));
    }

    public IEnumerator DesapearAfterDie(float delay){
        yield return new WaitForSecondsRealtime(delay);
        Destroy(gameObject);
    }
}
