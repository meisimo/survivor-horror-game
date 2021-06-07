using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface AttackingBehavior
{
    void PerformAttack(GameObject target);
    float DistanceToAttack();
}