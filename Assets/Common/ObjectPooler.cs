using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private List<string> dictionaryNames;
    private int bulletAmount;
    private int muzzleAmount;
    private Player player;

    private GameObject pooledObject;

    private WeaponSettings weaponSettings;
    //private Queue<GameObject> currentPool;
    private Dictionary<string, Dictionary<string, Queue<GameObject>>> dictionaries;

    void Start()
    {
        bulletAmount = 20;
        muzzleAmount = 5;

        dictionaries = new Dictionary<string, Dictionary<string, Queue<GameObject>>>();

        player = FindObjectOfType<Player>();

        weaponSettings = player.CurrentWeapon.GetComponent<Weapon>().WeaponSettings;
        var bulletPrefab = weaponSettings.BulletPrefab;
        var muzzlePrefab = weaponSettings.MuzzleVFX;

        foreach (var dicName in dictionaryNames)
        {
            AddNewDictionary(dicName);
        }

        // Add pools 
        foreach (var dicName in dictionaryNames)
        {
            AddPoolToDictionary(dicName, player.PlayerSettings.Weapons);
        }

        FillPool(dictionaryNames[0], player.PlayerSettings.CurrentWeaponName, bulletPrefab);
        FillPool(dictionaryNames[1], player.PlayerSettings.CurrentWeaponName, muzzlePrefab);
    }

    public void FillPool(string dictionaryName, string weaponName, GameObject prefab)
    {
        var objectsForDelete = (GameObject[]) FindObjectsOfType(typeof(GameObject), true);

        foreach (var obj in objectsForDelete)
            if (obj.CompareTag(prefab.tag))
                Destroy(obj);

        foreach (var dictionary in dictionaries)
        {
            if (dictionary.Key.Contains(dictionaryName))
            {
                foreach (var pool in dictionary.Value)
                {
                    if (pool.Value.Count > 0) pool.Value.Clear();
                }

                for (int i = 0; i < 50; i++)
                {
                    var obj = Instantiate(prefab);
                    obj.name = prefab.name;
                    obj.SetActive(false);
                    dictionary.Value[weaponName].Enqueue(obj);
                }
            }
        }

    }

    private void AddPoolToDictionary(string dictionaryName, IEnumerable<GameObject> weapons)
    {
        foreach (var dictionary in dictionaries)
        {
            var gameObjects = weapons.ToList();
            if (dictionary.Key.Contains(dictionaryName))
            {
                foreach (var weapon in gameObjects)
                {
                    dictionary.Value.Add(weapon.name, new Queue<GameObject>());
                }
            }
        }
    }

    private void AddNewDictionary(string dictionaryName)
    {
        dictionaries.Add(dictionaryName, new Dictionary<string, Queue<GameObject>>());
    }

    public Dictionary<string, Queue<GameObject>> GetDictionary(string dictionaryName)
    {
        foreach (var dictionary in dictionaries)
        {
            if (dictionary.Key.Contains(dictionaryName))
            {
                return dictionary.Value;
            }
        }

        return null;
    }

    public Queue<GameObject> GetCurrentPool(string dictionaryName)
    {
        foreach (var dictionary in dictionaries)
        {
            if (dictionary.Key.Contains(dictionaryName))
            {
                var dic = dictionary.Value;
                foreach (var pool in dic)
                {
                    if (pool.Value.Count != 0)
                    {
                        return pool.Value;
                    }
                }
            }
        }

        return null;
    }

    public GameObject GetPooledObject(string dictionaryName)
    {
        foreach (var dictionary in dictionaries)
        {
            if (dictionary.Key.Contains(dictionaryName))
            {
                var dic = dictionary.Value;
                foreach (var pool in dic)
                {
                    if (pool.Value.Count != 0)
                    {
                       pooledObject = pool.Value.Dequeue();

                            if (pooledObject && !pooledObject.activeInHierarchy)
                            {
                                return pooledObject;
                            }
                    }
                    
                }
               
            }
        }

        return null;
    }

    /*
    public GameObject GetPooledObject()
    {
        pooledObject = currentPool.Dequeue();

        if (pooledObject && !pooledObject.activeInHierarchy)
        {
            return pooledObject;
        }

        return null;
    }
    */

    public List<string> GetDictionaryNamesList => dictionaryNames;

    public int BulletAmount => bulletAmount;

    public int MuzzleAmount => muzzleAmount;
}