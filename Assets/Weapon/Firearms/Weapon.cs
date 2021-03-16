using System.Collections.Generic;
using UnityEngine;

public class Weapon: MonoBehaviour
{
    #pragma warning disable 0649
	[SerializeField] [Tooltip("Точка выстрела")] Transform shootPoint; // shoot point
	
	[SerializeField] [Tooltip("Точка направления стрельбы")]Transform targetPoint; // target point

	[SerializeField] WeaponSettings weaponSettings;
	#pragma warning restore 0649
	
	private ObjectPooler objectPooler;
	private Queue<GameObject> currentPool;
	private GameObject obj;
	
    // Start is called before the first frame update
    void Start()
    {
	    weaponSettings.ShootPoint = shootPoint.position;
	    weaponSettings.TargetPoint = targetPoint.position;
	    weaponSettings.ShootPointRotation = shootPoint.rotation;
    }

    // Update is called once per frame
    void Update()
    {
	    weaponSettings.ShootPoint = shootPoint.position;
	    weaponSettings.TargetPoint = targetPoint.position;
	    weaponSettings.ShootPointRotation = shootPoint.rotation;
	  
	    if(shootPoint)
	    {
		    var position = shootPoint.position;
		    Debug.DrawRay(position, targetPoint.position - position, Color.red);
	    }
	    
    }

    public void Shoot()
    {
	    weaponSettings.WeaponShoot.Shoot(weaponSettings.ShootPoint, weaponSettings.ShootPointRotation);
	    
	    if (!objectPooler) objectPooler = GameObject.FindGameObjectWithTag("objectPooler").GetComponent<ObjectPooler>();
        
	    foreach (var pool in objectPooler.GetDictionary(objectPooler.GetDictionaryNamesList[1]))
	    {
		    if (pool.Value.Count != 0)
		    {
			    currentPool = pool.Value;
		    }
	    }
        
	    obj = currentPool.Dequeue();
            
	    if (obj && !obj.activeInHierarchy)
	    {
		    obj.transform.position = weaponSettings.ShootPoint;
		    obj.transform.rotation = weaponSettings.ShootPointRotation;
		    obj.SetActive(true);
	    }

	    currentPool.Enqueue(obj);
    }
    public void StopShoot()
    {
	    weaponSettings.WeaponShoot.StopShoot();
	    obj.SetActive(false);
    }
   
	public WeaponSettings WeaponSettings => weaponSettings;
}
