using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Firearms", menuName = "Weapons/Firearms/Weapon")]
public class Weapon : ScriptableObject
{
	#pragma warning disable 0649
	[SerializeField] 
	private new WeaponNames name;
	
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
	
	#pragma warning restore 0649
	
	private Vector3 shootPoint; //точка вылета пули
	private Vector3 targetPoint; // направление вылета пули
	

	public void Shoot()
	{
		weaponShoot.Shoot(shootPoint);
	}
	
	
	public float RightArmLockPositionUp {
		get {
			return rightArmLockPositionUp;
		}
	}
	
	public float RightArmLockPositionDown 
	{
		get
		{
			return rightArmLockPositionDown;
		}
	}
	
	public Vector3 ShootPoint 
	{
		get {
			return shootPoint;
		}
		
		set{
			shootPoint = value;
		}
	}
	
	public Vector3 TargetPoint 
	{
		get {
			return targetPoint;
		}
		
		set {
			targetPoint = value;
		}
	}
	

}

enum WeaponNames {
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
}
