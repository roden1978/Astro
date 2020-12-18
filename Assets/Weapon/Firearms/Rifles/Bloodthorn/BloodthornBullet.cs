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

    private bool isCollision;

    private float flameLifeTime;
    private float speed;
    private float collisionEffectTime;
    private float time;
    
    private void Start()
    {
        isCollision = false;
        
        flameBeam = transform.GetChild(0);
        smoke = transform.GetChild(1);
        
        flameBeamParticleSystem = flameBeam.GetComponent<ParticleSystem>();
        smokeParticleSystem = smoke.GetComponent<ParticleSystem>();
        
        flameMain = flameBeamParticleSystem.main;
        smokeMain = smokeParticleSystem.main;

        collisionEffectTime = vfxCollision.GetComponent<DestroyVFX>().Delay / 3;

    }

    private void FixedUpdate()
    {
        transform.position = gun.ShootPoint;
        transform.rotation = gun.ShootPointRotation;

        flameLifeTime = Random.Range(minFlameLifeTime, maxFlameLifeTime);

        speed = Random.Range(minSpeed, maxSpeed);

        float distance = flameLifeTime * speed;
        
        Vector3 direction = gun.TargetPoint - gun.ShootPoint;
        hit = Physics2D.Raycast(gun.ShootPoint, direction, distance + 0.1f);
        
        if (hit.collider)
        {
            flameMain.startLifetime = smokeMain.startLifetime = hit.distance / speed;
            if (!isCollision) 
            {
                Instantiate(vfxCollision, hit.point, Quaternion.identity);
                isCollision = true;
                time = Time.time;
            }
        }
        else
        {
            flameLifeTime = Random.Range(minFlameLifeTime, maxFlameLifeTime);
            isCollision = false;
        }

        if (isCollision && Time.time > time + collisionEffectTime)
        {
            isCollision = false;
            time = 0;
        }

    }
}