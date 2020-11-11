using JetBrains.Annotations;
using UnityEngine;

namespace ScriptableObjects.Weapon.Firearms.Shoots
{
	[CreateAssetMenu(fileName = "New Stardust Shoot", menuName = "Weapons/Firearms/Shoot/Stardust Shoot")]
	public class StardustShoot : AWeaponShoot
	{
#pragma warning disable 0649
		[SerializeField]
		[Tooltip("Пуля")]
		private GameObject bullet;
	
		[SerializeField]
		[Tooltip("Оружие")]
		[CanBeNull]
		private global::Weapon weapon;
#pragma warning restore 0649
		public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
		{
			if (weapon && bullet) {
				return Instantiate(bullet, shootPoint, rotation);
			}

			Debug.Log("Оружие или пуля не найдены скрипт StardustShoot");

			return null;
		}
	
	}
}
