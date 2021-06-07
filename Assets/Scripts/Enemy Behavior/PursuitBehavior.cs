using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface PursuitBehavior
{
    void Pursuit(Transform target);
    void StopPursuit();
    bool IsInPursuit();
}