using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New LasersAndFlamethrowersShoot", menuName = "Weapons/Shoots/LasersAndFlamethrowersShoot")]
public class LasersAndFlamethrowersShoot : AWeaponShoot
{
    private ObjectPooler objectPooler;
    private Queue<GameObject> currentPool;

    private GameObject bulletGameObject;
    //private GameObject muzzleGameObject;

    
    public override void Shoot(GameObject muzzleVFXPrefab, GameObject bullet, Vector3 shootPoint, Quaternion rotation)
    {
        if (!objectPooler) objectPooler = GameObject.FindGameObjectWithTag("objectPooler").GetComponent<ObjectPooler>();
        
        foreach (var pool in objectPooler.BulletPoolDictionary)
        {
            if (pool.Value.Count != 0)
            {
                currentPool = pool.Value;
            }

            Debug.Log($"pool name= {pool.Key} pool Value count = {pool.Value.Count}");
        }

        bulletGameObject = currentPool.Dequeue();
        if (bulletGameObject && !bullet.activeInHierarchy)
        {
            bulletGameObject.transform.position = shootPoint;
            bulletGameObject.transform.rotation = rotation;
            bulletGameObject.SetActive(true);
        }

        currentPool.Enqueue(bulletGameObject);
        /*if (!muzzleGameObject)
        {
            muzzleGameObject = Instantiate(muzzleVFXPrefab, shootPoint, rotation);
        }*/
    }

    public override void StopShoot()
    {
        bulletGameObject.SetActive(false);
        //Destroy(muzzleGameObject);
    }
}