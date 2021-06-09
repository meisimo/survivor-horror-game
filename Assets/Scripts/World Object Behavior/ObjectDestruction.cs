using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour, Shootable
{
    public int pointsByDestruction;
    public int resistancePoints;
    public float delayForDestruction;
    public float destructionAnimationDuration;
    public float delayForImpact;
    public float impactAnimationDuration;
    public ParticleSystem impactParticleSystem;
    public ParticleSystem destructionParticleSystem;
    public AudioClip explotion;

    private int remainingResistance;
    private bool exists;

    private AudioSource source;

    private void Awake() {
        source = GetComponent<AudioSource>();
        remainingResistance = resistancePoints;
        impactParticleSystem.Stop();
        destructionParticleSystem.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        exists = true;
    }

    private IEnumerator Destruction()
    {
        LevelManagerController.IncreasePoints(pointsByDestruction);
        source.PlayOneShot(explotion);
        yield return new WaitForSecondsRealtime(delayForDestruction);
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        destructionParticleSystem.Play();
        yield return new WaitForSecondsRealtime(destructionAnimationDuration);
        destructionParticleSystem.Stop();
        Destroy(gameObject);
    }

    private IEnumerator Impact(Vector3 shootPoint)
    {
        source.Play();
        yield return new WaitForSecondsRealtime(delayForImpact);
        impactParticleSystem.transform.position = shootPoint;
        impactParticleSystem.transform.forward  = (shootPoint - transform.position).normalized;
        impactParticleSystem.Play();
        yield return new WaitForSecondsRealtime(impactAnimationDuration);
        impactParticleSystem.Stop();
    }

    public void GetShoot(Vector3 shootPoint)
    {
        if (exists)
        {
            remainingResistance--;
            if(0 < remainingResistance)
            {
                StartCoroutine(Impact(shootPoint));
            }
            else
            {
                exists = false;
                StartCoroutine(Destruction());
            }
        }
    }
}
