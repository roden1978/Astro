using UnityEngine;

[CreateAssetMenu(fileName = "New Firearms", menuName = "Weapons/Firearms/Weapon")]
public class WeaponSettings : ScriptableObject
{
	#pragma warning disable 0649
	[SerializeField]
	private Sprite image;
	
	[SerializeField] 
	[Tooltip("Позиция блокирования движения правой руки вверх (максимальное значение 0,15)")]
	private float rightArmLockPositionUp;
	
	[SerializeField]
	[Tooltip("Позиция блокирования правой руки вниз (максимальное значение -0,15)")]
	private float rightArmLockPositionDown;
	
	[SerializeField]
	[Tooltip("Скрипт обработки выстрела")]
	private AWeaponShoot weaponShoot;
	
	[SerializeField]
	[Tooltip("Визуальный эффект выстрела")]
	private GameObject vfxShoot;
	
	[SerializeField]
	[Tooltip("Задержка перед следующим выстрелом (одиночные выстрелы 0)")]
	private float shootDelay;
	
	[SerializeField]
	[Tooltip("Префаб пули")]
	private GameObject bulletPrefab;
	
	#pragma warning restore 0649

	public void Shoot()
	{
		weaponShoot.Shoot(ShootPoint, ShootPointRotation);
		Instantiate(vfxShoot, ShootPoint, ShootPointRotation);
	}
	
	public void StopShoot()
	{
		weaponShoot.StopShoot();
	}
	
	
	public float RightArmLockPositionUp => rightArmLockPositionUp;
	public float RightArmLockPositionDown => rightArmLockPositionDown; 
	public Vector3 ShootPoint { get; set; }
	public Quaternion ShootPointRotation { get; set; }
	public Vector3 TargetPoint { get; set; }
	public float ShootDelay => shootDelay;
	public bool IsReady { get; set; } = true;
	public bool IsShooting { get; set; }

	public GameObject BulletPrefab => bulletPrefab;
}

