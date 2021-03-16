using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GunsShoot", menuName = "Weapons/Shoots/GunsShoot")]
public class GunsShoot : AWeaponShoot
{
    private ObjectPooler objectPooler;
    private Queue<GameObject> currentPool;
    
    public override void Shoot(Vector3 shootPoint, Quaternion rotation)
    {
        if (!objectPooler) objectPooler = FindObjectOfType<ObjectPooler>();
        
        var currentDictionaryName = objectPooler.GetDictionaryNamesList[0];
        
        if(currentPool == null) currentPool = objectPooler.GetCurrentPool(currentDictionaryName);
        var bulletGameObject = objectPooler.GetPooledObject(currentDictionaryName);
        
        bulletGameObject.transform.position = shootPoint;
        bulletGameObject.transform.rotation = rotation;
        bulletGameObject.SetActive(true);
        currentPool.Enqueue(bulletGameObject);
       
    }
   
    public override void StopShoot()
    {
    }
}