using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BloodthornBullet : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Weapon gun;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float maxFlameLifeTime;
    [SerializeField] float minFlameLifeTime;
    [SerializeField] float maxSmokeLifeTime;
    [SerializeField] float minSmokeLifeTime;
    [SerializeField] GameObject vfxCollision;
#pragma warning restore 0649
    
    private Transform flameBeam;
    private Transform smoke;
    private ParticleSystem flameBeamParticleSystem;
    private ParticleSystem smokeParticleSystem;
    private ParticleSystem.MainModule flameMain;
    private ParticleSystem.MainModule smokeMain;
    private RaycastHit2D hit;
    private RaycastHit2D lastHitPoint;
    //private GameObject collisionEffect;

    private bool isCollision;

    private float flameLifeTime;
    private float smokeLifeTime;
    private float speed;
    private float collisionEffectTime;
    
    private IEnumerator coroutine;

    private void Start()
    {
        isCollision = false;
        
        flameBeam = transform.GetChild(0);
        smoke = transform.GetChild(1);
        
        flameBeamParticleSystem = flameBeam.GetComponent<ParticleSystem>();
        smokeParticleSystem = smoke.GetComponent<ParticleSystem>();
        
        flameMain = flameBeamParticleSystem.main;
        smokeMain = smokeParticleSystem.main;

        collisionEffectTime = vfxCollision.GetComponent<DestroyVFX>().Delay;

        coroutine = HitPointReset(collisionEffectTime);
    }

    private void FixedUpdate()
    {
        transform.position = gun.ShootPoint;
        transform.rotation = gun.ShootPointRotation;

        flameLifeTime = Random.Range(minFlameLifeTime, maxFlameLifeTime);
        smokeLifeTime = Random.Range(minSmokeLifeTime, maxSmokeLifeTime);

        speed = Random.Range(minSpeed, maxSpeed);

        float flameDistance = flameLifeTime * speed;
        float smokeDistance = smokeLifeTime * speed;
        
        Vector3 direction = gun.TargetPoint - gun.ShootPoint;
        hit = Physics2D.Raycast(gun.ShootPoint, direction, flameDistance);
        
        if (hit.collider)
        {
            flameMain.startLifetime = smokeMain.startLifetime = hit.distance / speed;
            if (lastHitPoint.point != hit.point) //!isCollision && !Vector2.Equals(lastHitPoint.point, hit.point)
            {
                
                lastHitPoint.point = hit.point;
                Instantiate(vfxCollision, hit.point, Quaternion.identity);
                isCollision = true;
                StartCoroutine(coroutine);
            }
            
        }
        else
        {
            flameLifeTime = Random.Range(minFlameLifeTime, maxFlameLifeTime);
            smokeLifeTime = Random.Range(minSmokeLifeTime, maxSmokeLifeTime);
            isCollision = false;
        }

    }

    private IEnumerator HitPointReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        lastHitPoint.point = Vector2.zero;
        isCollision = false;
        Debug.Log(lastHitPoint.point);
    }
}