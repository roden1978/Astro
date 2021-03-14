using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private int bulletAmount;
    private int muzzleAmount;
    private Player player;

    private Dictionary<string, Queue<GameObject>> bulletsPoolDictionary;
    private Dictionary<string, Queue<GameObject>> muzzlePoolDictionary;

    private Queue<GameObject> objectPool;

    private GameObject bulletPrefab;
    private GameObject muzzlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        bulletAmount = 20;
        muzzleAmount = 5;
        
        bulletsPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        muzzlePoolDictionary = new Dictionary<string, Queue<GameObject>>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        var weaponSettings = player.CurrentWeapon.GetComponent<Weapon>().WeaponSettings;
        bulletPrefab = weaponSettings.BulletPrefab;
        muzzlePrefab = weaponSettings.MuzzleVFX;
        
        CreatePoolDictionary(player.PlayerSettings.Weapons, bulletsPoolDictionary);
        FillPool(player.PlayerSettings.CurrentWeaponName, bulletPrefab, bulletsPoolDictionary, bulletAmount);
        
        CreatePoolDictionary(player.PlayerSettings.Weapons, muzzlePoolDictionary);
        FillPool(player.PlayerSettings.CurrentWeaponName, muzzlePrefab, muzzlePoolDictionary, muzzleAmount);
    }

    public void FillPool(string weaponName, GameObject prefab, Dictionary<string, Queue<GameObject>> dictionary, int amount)
    {
        var objectsForDelete = (GameObject[]) FindObjectsOfType(typeof(GameObject), true);

        foreach (var obj in objectsForDelete) if (obj.CompareTag(prefab.tag)) Destroy(obj);
       
        foreach (var pool in dictionary) if (pool.Value.Count > 0) pool.Value.Clear();
       
        for (int i = 0; i < amount; i++)
        {
            var obj = Instantiate(prefab);
            obj.name = prefab.name;
            obj.SetActive(false);
            dictionary[weaponName].Enqueue(obj);
        }
    }

    private void CreatePoolDictionary(IEnumerable<GameObject> weapons, Dictionary<string, Queue<GameObject>> dictionary)
    {
        foreach (var weapon in weapons)
        {
            objectPool = new Queue<GameObject>();
            dictionary.Add(weapon.name, objectPool);
        }
    }

    public Dictionary<string, Queue<GameObject>> BulletPoolDictionary => bulletsPoolDictionary;
    public Dictionary<string, Queue<GameObject>> MuzzlePoolDictionary => muzzlePoolDictionary;

    public int BulletAmount => bulletAmount;

    public int MuzzleAmount => muzzleAmount;
}