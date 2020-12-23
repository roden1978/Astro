using UnityEngine;

//[CreateAssetMenu(fileName = "New Agressor Shoot", menuName = "Weapons/Firearms/Shoot/Agressor Shoot")]
public class AgressorShoot : AWeaponShoot
{
#pragma warning disable 0649
	[SerializeField]
	[Tooltip("Пуля")]
	private GameObject bullet;
	
	[SerializeField]
	[Tooltip("Оружие")]
	private Weapon weapon;
#pragma warning restore 0649
	private GameObject bulletGameObject;
	private GameObject muzzleGameObject;
	
	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		if (!bulletGameObject) CreateFlame(shootPoint, rotation);
		return bulletGameObject;
	}

	private void CreateFlame(Vector3 _shootPoint, Quaternion _rotation)
	{
		if (weapon && bullet) {
			muzzleGameObject = Instantiate(weapon.VFXShoot, _shootPoint, weapon.ShootPointRotation);
			bulletGameObject =  Instantiate(bullet, _shootPoint, _rotation);
		}else Debug.Log("Оружие или пуля не найдены скрипт AgressorShoot");
	}
	
	public override void StopShoot()
	{
		//bulletGameObject.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);
		Destroy(bulletGameObject);
		Destroy(muzzleGameObject);
	}
}
