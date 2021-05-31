using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public SimpleShoot gun;
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
        yield return new WaitForSecondsRealtime(0.05f);
        gun.AnimateShoot();
        shootController.Shoot();
    }

}
