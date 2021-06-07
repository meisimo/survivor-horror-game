using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface Attackable
{
    void RecieveAttack(int damage, float animationDelay);
}