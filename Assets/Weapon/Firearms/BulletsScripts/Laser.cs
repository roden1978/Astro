﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Laser : ABullet
{
#pragma warning disable 0649
    [FormerlySerializedAs("gun")] [SerializeField] WeaponSettings weapon;
    [SerializeField] float laserSpeed;
    [SerializeField] float maxLaserLenght;
    [SerializeField] GameObject vfxCollision;
#pragma warning restore 0649
    private Transform beam;
    private LineRenderer lineRenderer;
    private RaycastHit2D hit;
    private GameObject collisionEffect;
    private AEnemy enemy;

    private bool isCollision;

    private void Start()
    {
        isCollision = false;
        beam = transform.GetChild(0);
        lineRenderer = beam.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, Vector3.zero);
        lineRenderer.SetPosition(0, Vector3.zero);
        damage = weapon.Damage;
        showEffect.AddListener(Play);
    }

    private void FixedUpdate()
    {
        transform.position = weapon.ShootPoint;
        transform.rotation = weapon.ShootPointRotation;

        float distance = lineRenderer.GetPosition(1).x;
        Vector3 direction = weapon.ShootPoint.DirectionTo(weapon.TargetPoint);
        hit = Physics2D.Raycast(weapon.ShootPoint, direction, distance);
        
        if (hit.collider)
        {
            lineRenderer.SetPosition(1,new Vector3(hit.distance, 0, 0));
            if (!isCollision)
            {
                showEffect?.Invoke();
            }

            if (hit.collider.gameObject.CompareTag("enemie"))
            {
                if(!enemy) enemy = hit.collider.gameObject.GetComponent<AEnemy>();
                enemy.onDamage?.Invoke(damage);
            }
               
        }
        else if (lineRenderer.GetPosition(1).x < maxLaserLenght)
        {
            lineRenderer.SetPosition(1, new Vector3(lineRenderer.GetPosition(1).x + Time.deltaTime * laserSpeed, 0, 0));
            isCollision = false;
            Destroy(collisionEffect);
        }
    }

    /*private void OnDestroy()
    {
        Destroy(collisionEffect);
    }*/

    private void OnDisable()
    {
        Destroy(collisionEffect);
    }

    protected override void Play()
    {
        collisionEffect = Instantiate(vfxCollision, hit.point, Quaternion.identity);
        isCollision = true;
    }
}