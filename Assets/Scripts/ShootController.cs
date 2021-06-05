using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public float range = 100f;
    
    private Ray ray;
    private RaycastHit raycastHit;
    private int shootableMask;
    private float timer;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        ray.origin    = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out raycastHit, range, shootableMask))
        {
            Debug.Log("SHOOT");
            raycastHit.collider.gameObject.GetComponent<Shootable>()?.GetShoot(raycastHit.point);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.black, 1f);
            Debug.Log("MISS");
        }
    }
}
