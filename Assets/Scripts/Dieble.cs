using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Dieble
{
    IEnumerator Die();
    IEnumerator DesapearAfterDie(float delay);
}