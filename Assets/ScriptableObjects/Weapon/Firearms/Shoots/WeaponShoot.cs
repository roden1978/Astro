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
                weapon.IsShooting = true;
                break;
            case "Agressor":
            case "Alligator":
            case "BloodThorn":
                if (!bulletGameObject)
                {
                    Create(shootPoint, rotation);
                    weapon.IsShooting = true;
                }
                return bulletGameObject;
            case "SoulBreaker":
                Instantiate(weapon.VFXShoot, shootPoint, weapon.ShootPointRotation);
                for (int i = 0; i <= 2; i++)
                {
                    Instantiate(bullet, shootPoint, rotation );
                }

                weapon.IsShooting = true;
                break;
            case "WidowMaker":
                if (!bulletGameObject && weapon.IsReady)
                {
                    Create(shootPoint, rotation);
                    weapon.IsReady = false;
                    weapon.IsShooting = true;
                }
                return bulletGameObject;
            default: return null;
        }

        return null;
    }

    public override void StopShoot()
    {
        if (weapon.IsShooting) weapon.IsShooting = false;
        Destroy(bulletGameObject);
        Destroy(muzzleGameObject);
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