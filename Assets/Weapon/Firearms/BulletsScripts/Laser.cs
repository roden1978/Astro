using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Laser : MonoBehaviour
{
#pragma warning disable 0649
    [FormerlySerializedAs("gun")] [SerializeField] Weapon weapon;
    [SerializeField] float laserSpeed;
    [SerializeField] float maxLaserLenght;
    [SerializeField] GameObject vfxCollision;
#pragma warning restore 0649
    private Transform beam;
    private LineRenderer lineRenderer;
    private RaycastHit2D hit;
    private GameObject collisionEffect;

    private bool isCollision;

    private void Start()
    {
        isCollision = false;
        beam = transform.GetChild(0);
        lineRenderer = beam.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, Vector3.zero);
        lineRenderer.SetPosition(0, Vector3.zero);
    }

    private void FixedUpdate()
    {
        transform.position = weapon.ShootPoint;
        transform.rotation = weapon.ShootPointRotation;

        float distance = lineRenderer.GetPosition(1).x;
        Vector3 direction = weapon.TargetPoint.DirectionTo(weapon.ShootPoint);
        hit = Physics2D.Raycast(weapon.ShootPoint, direction, distance);
        
        if (hit.collider)
        {
            lineRenderer.SetPosition(1,new Vector3(hit.distance, 0, 0));
            if (!isCollision)
            {
                collisionEffect = Instantiate(vfxCollision, hit.point, Quaternion.identity);
                isCollision = true;
            }
            
        }
        else if (lineRenderer.GetPosition(1).x < maxLaserLenght)
        {
            lineRenderer.SetPosition(1, new Vector3(lineRenderer.GetPosition(1).x + Time.deltaTime * laserSpeed, 0, 0));
            isCollision = false;
            Destroy(collisionEffect);
        }
    }

    private void OnDestroy()
    {
        Destroy(collisionEffect);
    }
}