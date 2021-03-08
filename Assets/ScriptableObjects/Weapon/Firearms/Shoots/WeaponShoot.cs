// using UnityEngine;
//
//
// [CreateAssetMenu(fileName = "New Weapon Shoot", menuName = "Weapons/Firearms/Shoot")]
// public class WeaponShoot : AWeaponShoot
// {
// #pragma warning disable 0649
//     [SerializeField] [Tooltip("Пуля")] private GameObject bullet;
//
//     [SerializeField] [Tooltip("Оружие")] private WeaponSettings weapon;
// #pragma warning restore 0649
//     
//     private GameObject bulletGameObject;
//     private GameObject muzzleGameObject;
//     
//     public override void Shoot(GameObject bulletPrefab, Vector3 shootPoint, Quaternion rotation)
//     {
//         if (!weapon || !bullet) return;
//         //Shooting(weapon.WeaponTypes, shootPoint, rotation);
//     }
//
//     public override void StopShoot()
//     {
//         weapon.IsShooting = false;
//         if (weapon.IsReady)
//         {
//             Destroy(bulletGameObject);
//             //Destroy(muzzleGameObject);
//         }
//
//     }
//
//     private void Shooting(string weaponType, Vector3 shootPoint, Quaternion rotation)
//     {
//         switch (weaponType)
//         {
//             case "Guns":
//                 //Instantiate(weapon.VFXShoot, shootPoint, rotation);
//                 Instantiate(bullet, shootPoint, rotation);
//                 weapon.IsShooting = true;
//                 break;
//             case "LaserAndFlamethrowers":
//                 if (!bulletGameObject)
//                 {
//                     bulletGameObject =  Instantiate(bullet, _shootPoint, _rotation);
//                     weapon.IsShooting = true;
//                 }
//                 break;
//             case "Shootguns":
//                 //Instantiate(weapon.VFXShoot, shootPoint, rotation);
//                 for (int i = 0; i <= 2; i++)
//                 {
//                     Instantiate(bullet, shootPoint, rotation);
//                 }
//                 weapon.IsShooting = true;
//                 break;
//             case "WMD":
//                 if (!bulletGameObject && weapon.IsReady)
//                 {
//                     Create(shootPoint, rotation);
//                     weapon.IsReady = false;
//                     weapon.IsShooting = true;
//                 }
//                 break;
//         }
//     }
//     
//     private void Create(Vector3 _shootPoint, Quaternion _rotation)
//     { 
//         //muzzleGameObject = Instantiate(weapon.VFXShoot, _shootPoint, _rotation);
//         bulletGameObject =  Instantiate(bullet, _shootPoint, _rotation);
//     }
// }
