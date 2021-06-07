using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour, Attackable
{
    public SimpleShoot gun;

    private int life;
    private Animator animator;
    private ShootController shootController;

    private void Awake() {
        animator        = GetComponent<Animator>();
        shootController = GetComponentInChildren<ShootController>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("Fire1") && gun.IsAvailableToShoot())
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

    public void RecieveAttack(int damage, float animationDelay)
    {
        life -= damage;
        StartCoroutine(DamageAnimation(animationDelay));
    }


}
