using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GunsShoot", menuName = "Weapons/Shoots/GunsShoot")]
public class GunsShoot : AWeaponShoot
{
    private ObjectPooler objectPooler;
    private Queue<GameObject> currentPool;
    
   
    public override void Shoot(GameObject muzzleVFXPrefab, GameObject bullet, Vector3 shootPoint, Quaternion rotation)
    {
        if (!objectPooler) objectPooler = GameObject.FindGameObjectWithTag("objectPooler").GetComponent<ObjectPooler>();
        
        foreach (var pool in objectPooler.BulletPoolDictionary)
            {
                if (pool.Value.Count != 0)
                {
                    currentPool = pool.Value;
                }
            }
        
            var obj = currentPool.Dequeue();
            
                if (obj && !obj.activeInHierarchy)
                {
                    obj.transform.position = shootPoint;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                }

                currentPool.Enqueue(obj);

        //Instantiate(bullet, shootPoint, rotation);
        //Instantiate(muzzleVFXPrefab, shootPoint, rotation);
    }

    public override void StopShoot()
    {
    }
}