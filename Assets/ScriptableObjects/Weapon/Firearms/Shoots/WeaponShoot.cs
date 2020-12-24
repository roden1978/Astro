using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Shoot", menuName = "Weapons/Firearms/Shoot")]
public class WeaponShoot : AWeaponShoot
{
#pragma warning disable 0649
    [SerializeField] [Tooltip("Пуля")] private GameObject bullet;

    [SerializeField] [Tooltip("Оружие")] private Weapon weapon;
#pragma warning restore 0649
    
    private GameObject bulletGameObject;
    private GameObject muzzleGameObject;
    
    public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
    {
        if (!weapon || !bullet)
        {
            Debug.Log("Оружие или пуля не найдены скрипт WeaponShoot");
            return null;
        }

        switch (weapon.Name)
        {
            case "DesertEagle":
            case "StarDust":
            case "Axe":
            case "LoudCricket":
            case "LightStone":
                Instantiate(weapon.VFXShoot, shootPoint, weapon.ShootPointRotation);
                Instantiate(bullet, shootPoint, rotation);
                weapon.IsFire = true;
                return null;
            case "Agressor":
            case "Alligator":
            case "BloodThorn":
                if (!bulletGameObject)
                {
                    Create(shootPoint, rotation);
                    weapon.IsFire = true;
                }
                return bulletGameObject;
            case "SoulBreaker":
                Instantiate(weapon.VFXShoot, shootPoint, weapon.ShootPointRotation);
                for (int i = 0; i <= 2; i++)
                {
                    Instantiate(bullet, shootPoint, rotation );
                }

                weapon.IsFire = true;
                return null;
            case "WidowMaker":
                if (!bulletGameObject && weapon.IsReady)
                {
                    Create(shootPoint, rotation);
                    weapon.IsReady = false;
                    weapon.IsFire = true;
                }
                return bulletGameObject;
            default: return null;
        }

    }

    public override void StopShoot()
    {
        weapon.IsFire = false;
        if (weapon.IsReady)
        {
            Destroy(bulletGameObject);
            Destroy(muzzleGameObject);
        }
        
    }
    
    private void Create(Vector3 _shootPoint, Quaternion _rotation)
    { 
        muzzleGameObject = Instantiate(weapon.VFXShoot, _shootPoint, weapon.ShootPointRotation);
        bulletGameObject =  Instantiate(bullet, _shootPoint, _rotation);
    }
}

/*
None,
	Agressor,
	Alligator,
	Axe,
	BloodThorn,
	DesertEagle,
	LightStone,
	LoudCricket,
	SoulBreaker,
	StarDust,
	WidowMaker
	*/