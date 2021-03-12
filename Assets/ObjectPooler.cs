using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private int bulletAmount;
    private Player player;

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private Queue<GameObject> objectPool;

    private GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        bulletAmount = 20;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    private void Update()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            bulletPrefab = player.CurrentWeapon.GetComponent<Weapon>().WeaponSettings.BulletPrefab;
            CreatePoolDictionary(player.PlayerSettings.Weapons);
            FillPool(player.PlayerSettings.CurrentWeaponName, bulletPrefab);
            /*foreach (var dic in poolDictionary)
            {
                Debug.Log($"Name {dic.Key}, Queue {dic.Value.Count}");
            }*/
        }
    }

    public void FillPool(string weaponName, GameObject bullet)
    {
        //bulletPrefab = GameObject.FindGameObjectWithTag("weapon").GetComponent<Weapon>().WeaponSettings.BulletPrefab;
        var objectsForDelete = (GameObject[]) FindObjectsOfType(typeof(GameObject), true);

        foreach (var obj in objectsForDelete)
        {
            if (obj.CompareTag(bulletPrefab.tag) && bulletPrefab.tag != null)
              Destroy(obj);
              //Debug.Log($"Obj {obj}");
            
        }
        
        foreach (var pool in poolDictionary)
        {
           // Debug.Log(pool.Value.Count);
            if (pool.Value.Count > 0) pool.Value.Clear();
        }

        
        for (int i = 0; i < bulletAmount; i++)
        {
            var obj = Instantiate(bullet);
            obj.name = bullet.name;
            obj.SetActive(false);
            poolDictionary[weaponName].Enqueue(obj);
        }
    }

    public void CreatePoolDictionary(IEnumerable<GameObject> weapons)
    {
        foreach (var weapon in weapons)
        {
            objectPool = new Queue<GameObject>();
            poolDictionary.Add(weapon.name, objectPool);
        }

        /*foreach (var dic in poolDictionary)
        {
            Debug.Log($"Name {dic.Key}, Queue {dic.Value.Count}");
        }*/
    }

    public Dictionary<string, Queue<GameObject>> PoolDictionary => poolDictionary;
}