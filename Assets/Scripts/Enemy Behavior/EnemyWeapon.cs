using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int damage;
    public float damageDelay;

    private bool damageActive;

    private void OnTriggerExit(Collider other) {
        if (damageActive && other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Attackable>().RecieveAttack(damage, damageDelay);
            damageActive = false;
        }
    }


    public void ActivateDamage()
    {
        damageActive = true;
    }
}
