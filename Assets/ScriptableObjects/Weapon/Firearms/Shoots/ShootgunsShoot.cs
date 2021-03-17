using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ShootgunsShoot", menuName = "Weapons/Shoots/ShootgunsShoot")]
public class ShootgunsShoot : AWeaponShoot
{
    private ObjectPooler objectPooler;
    private Queue<GameObject> currentPool;

    public override void Shoot(Vector3 shootPoint, Quaternion rotation)
    {
        if (!objectPooler) objectPooler = FindObjectOfType<ObjectPooler>();
        
        var currentDictionaryName = objectPooler.GetDictionaryNamesList[0];
        
        if(currentPool == null) currentPool = objectPooler.GetCurrentPool(currentDictionaryName);
        
        for (int i = 0; i < 3; i++)
        {
            var bulletGameObject = objectPooler.GetPooledObject(currentDictionaryName);

            bulletGameObject.transform.position = shootPoint;
            bulletGameObject.transform.rotation = rotation;
            bulletGameObject.SetActive(true);
            currentPool.Enqueue(bulletGameObject);
        }

    }
   
    public override void StopShoot()
    {
    }
}