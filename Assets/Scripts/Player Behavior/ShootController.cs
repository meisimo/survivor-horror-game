﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform camera;
    public Transform sightPoint;
    public GameObject sightInTarget;

    private float range;
    private Ray ray;
    private RaycastHit raycastHit;
    private GameObject targetOnSight;
    Vector3 sightOnTargetPosition;
    private int shootableMask;
    private float timer;

    private void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        sightInTarget.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        range = Vector3.Distance(transform.position, sightPoint.position) + 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        (targetOnSight, sightOnTargetPosition) = TargetOnSight();
        if (targetOnSight)
        {
            ShowSightToTarget(sightOnTargetPosition);
        }
        else 
        {
            ShowSigthNoTarget();
        }
    }

    private void ShowSigthNoTarget()
    {
        sightPoint.gameObject.SetActive(true);
        sightInTarget.gameObject.SetActive(false);
    }

    private void ShowSightToTarget(Vector3 animingPosition)
    {
        sightPoint.gameObject.SetActive(false);
        sightInTarget.gameObject.SetActive(true);
        sightInTarget.gameObject.transform.position = animingPosition;
        sightInTarget.gameObject.transform.forward  = camera.forward;
    }

    private (GameObject, Vector3) TargetOnSight()
    {
        ray.origin    = transform.position;
        ray.direction = sightPoint.position - transform.position;

        if(Physics.Raycast(ray, out raycastHit, range, shootableMask))
            return (raycastHit.collider.gameObject, raycastHit.point);
        return (null, Vector3.zero);
    }

    public void Shoot()
    {
        if (targetOnSight)
        {
            targetOnSight.GetComponent<Shootable>()?.GetShoot(sightOnTargetPosition);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.black, 1f);
        }
    }
}
