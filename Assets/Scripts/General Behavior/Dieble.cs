using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Dieble
{
    void Die();
    bool IsDead();
    IEnumerator DesapearAfterDie(float delay);
}