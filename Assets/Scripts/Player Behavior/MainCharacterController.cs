using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour, Attackable, Dieble
{
    public SimpleShoot gun;
    public float delayToTransformCollider = 2.5f;
    public LifePoints lifePointsText;

    private bool isAlive;
    private int life = 100;
    private Animator animator;
    private ShootController shootController;

    private void Awake() {
        isAlive         = true;
        animator        = GetComponent<Animator>();
        shootController = GetComponentInChildren<ShootController>();
        lifePointsText.InitPoints(life);
    }

    // Update is called once per frame
    void Update()
    {
        if( isAlive && Input.GetButtonDown("Fire1") && gun.IsAvailableToShoot())
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        animator.SetTrigger("Fire");
        yield return new WaitForSecondsRealtime(0.001f);
        gun.AnimateShoot();
        shootController.Shoot();
    }

    private IEnumerator DamageAnimation(float animationDelay)
    {
        yield return new WaitForSecondsRealtime(animationDelay);
        animator.SetTrigger("Recieve Damage");
    }

    private IEnumerator TransformCollider()
    {
        yield return new WaitForSecondsRealtime(delayToTransformCollider);
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();
        capsule.direction = 2;
        capsule.radius    = 0.375f * capsule.radius;
    }

    public IEnumerator DesapearAfterDie(float delay){
        yield return new WaitForSecondsRealtime(delay);
        gameObject.SetActive(false);
    }

    public bool IsDead()
    {
        return !isAlive;
    }

    public void LoadMunition(int munition)
    {
        gun.IncreaseMunition(munition);
    }

    public void Die()
    {
        StartCoroutine(TransformCollider());
        isAlive = false;
        animator.SetTrigger("Die");
    }

    public void RecieveAttack(int damage, float animationDelay)
    {
        lifePointsText.DecreaseLifePointsText(damage);
        life -= damage;
        if ( 0 < life)
        {
            StartCoroutine(DamageAnimation(animationDelay));
        }
        else 
        {
            Die();
        }
    }

}
