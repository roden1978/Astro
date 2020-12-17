using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorBullet : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Weapon gun;
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
        //Debug.Log($"Beam = {beam}  {beam.name}");
        lineRenderer = beam.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, Vector3.zero);
        lineRenderer.SetPosition(0, Vector3.zero);
    }

    private void FixedUpdate()
    {
        transform.position = gun.ShootPoint;
        transform.rotation = gun.ShootPointRotation;

        float distance = lineRenderer.GetPosition(1).x;
        Vector3 direction = gun.TargetPoint - gun.ShootPoint;
        hit = Physics2D.Raycast(gun.ShootPoint, direction, distance);
        
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

        //Debug.Log(lineRenderer.GetPosition(1).x);
    }

    private void OnDestroy()
    {
        Destroy(collisionEffect);
    }
}