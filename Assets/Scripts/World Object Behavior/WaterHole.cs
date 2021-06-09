using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHole : MonoBehaviour, Playable
{
    public ParticleSystem holeImpact;
    public ParticleSystem water;

    public void Stop()
    {
        holeImpact.Stop();
        water.Stop();
    }

    public void Play()
    {
        holeImpact.Play();
        water.Play();
    }
}
