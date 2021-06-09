using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShootable : MonoBehaviour, Shootable
{
    public GameObject water;
    public float animationDuration;
    public float delayForImpact;
    private AudioSource sourece;

    private void Awake() {
        sourece = GetComponent<AudioSource>();
    }

    private IEnumerator Impact(Vector3 position)
    {
        yield return new WaitForSecondsRealtime(0.35f);
        Vector3 v    = position - transform.position;
        GameObject d = GameObject.Instantiate(water, position, Quaternion.LookRotation(v, -v));
        d.transform.SetParent(transform);
    }

    public void GetShoot(Vector3 impactPoint)
    {
        sourece.Play();
        StartCoroutine(Impact(impactPoint));
    }
}
