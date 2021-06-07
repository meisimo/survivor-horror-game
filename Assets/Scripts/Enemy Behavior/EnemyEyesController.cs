using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyesController : MonoBehaviour
{
    public float verticalAngleRange;
    public float horizontalAngleRange;
    public bool targetDetected;


    private SphereCollider sphereCollider;
    private GameObject     player;
    private EnemyWithEyes  enemyWithEyes;
    private bool playerInsideSphere;
    private bool targetOnSight;
    

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        enemyWithEyes  = GetComponentInParent<EnemyWithEyes>();
        targetDetected = false;
        targetOnSight  = false;
    }

    private void FixedUpdate()
    {
        targetOnSight = playerInsideSphere && PlayerInsideVisionRange();
        if (targetOnSight)
        {
            targetDetected = true;
            enemyWithEyes.TargetDetected(player);
        }
        else if (targetDetected)
        {
            StartCoroutine(RefreshTargetDetectedWithDelay());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideSphere = true;
            player             = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInsideSphere = false;
            player             = null;
        }
    }

    private bool PlayerInsideVisionRange()
    {
        float angle = Vector3.Angle(player.transform.position - transform.position, transform.forward);
        return Mathf.Abs(angle) < horizontalAngleRange;
    }

    private IEnumerator RefreshTargetDetectedWithDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        targetDetected = targetOnSight;
    }
    
    private void ShowCone()
    {
        Quaternion q_x1 = Quaternion.Euler(0,  horizontalAngleRange, 0);
        Quaternion q_x2 = Quaternion.Euler(0, -horizontalAngleRange, 0);
        Quaternion q_y1 = Quaternion.Euler( verticalAngleRange, 0, 0);
        Quaternion q_y2 = Quaternion.Euler(-verticalAngleRange, 0, 0);
        float duration = 0.1f;

        Debug.DrawLine( transform.position,
                        transform.position + sphereCollider.radius * transform.forward,
                        Color.blue,
                        duration);

        Debug.DrawLine( transform.position,
                        transform.position + sphereCollider.radius * ( q_x1 * transform.forward),
                        Color.magenta,
                        duration);
        Debug.DrawLine( transform.position,
                        transform.position + sphereCollider.radius * ( q_x2 * transform.forward),
                        Color.magenta,
                        duration);
        Debug.DrawLine( transform.position,
                        transform.position + sphereCollider.radius * ( q_y1 * transform.forward),
                        Color.magenta,
                        duration);
        Debug.DrawLine( transform.position,
                        transform.position + sphereCollider.radius * ( q_y2 * transform.forward),
                        Color.magenta,
                        duration);

    }

}
