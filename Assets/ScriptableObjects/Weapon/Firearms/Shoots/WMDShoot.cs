using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New WMDShoot", menuName = "Weapons/Shoots/WMDShoot")]
public class WMDShoot : AWeaponShoot
{
    private ObjectPooler objectPooler;
    private Queue<GameObject> currentPool;
    private GameObject bulletGameObject;

    private GameObject prevGameObject;

    public override void Shoot(Vector3 shootPoint, Quaternion rotation)
    {
        if (!objectPooler) objectPooler = FindObjectOfType<ObjectPooler>();

        var currentDictionaryName = objectPooler.GetDictionaryNamesList[0];

        if (currentPool == null) currentPool = objectPooler.GetCurrentPool(currentDictionaryName);

        bulletGameObject = prevGameObject = objectPooler.GetPooledObject(currentDictionaryName);
        bulletGameObject.transform.position = shootPoint;
        bulletGameObject.transform.rotation = rotation;
        bulletGameObject.SetActive(true);
        
        currentPool.Enqueue(bulletGameObject);
    }

    public override void StopShoot()
    {
    }
}