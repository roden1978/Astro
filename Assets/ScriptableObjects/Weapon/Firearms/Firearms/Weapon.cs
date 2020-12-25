﻿using System.Collections;
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
	
	[SerializeField]
	[Tooltip("Визуальный эффект выстрела")]
	private GameObject vfxShoot;
	
	[SerializeField]
	[Tooltip("Задержка перед следующим выстрелом (одиночные выстрелы 0)")]
	private float shootDelay;
	
	#pragma warning restore 0649
	
	private Vector3 shootPoint; //точка вылета пули
	private Quaternion shootPointRotation;
	private Vector3 targetPoint; // направление вылета пули

	private bool isReady = true;
	private bool isShooting;

	public void Shoot()
	{
		weaponShoot.Shoot(shootPoint, shootPointRotation);
	}
	
	public void StopShoot()
	{
		weaponShoot.StopShoot();
	}
	
	
	public float RightArmLockPositionUp {
		get => rightArmLockPositionUp;
	}
	
	public float RightArmLockPositionDown 
	{
		get => rightArmLockPositionDown;
	}
	
	public Vector3 ShootPoint 
	{
		get => shootPoint;
		
		set => shootPoint = value;
	}

	public Quaternion ShootPointRotation
	{
		get => shootPointRotation;
		set => shootPointRotation = value;
	}

	public Vector3 TargetPoint 
	{
		get => targetPoint;
		
		set => targetPoint = value;
	}

	public GameObject VFXShoot
	{
		get => vfxShoot;
	}

	public float ShootDelay
	{
		get => shootDelay;
		set => shootDelay = value;
	}

	public bool IsReady
	{
		get => isReady;
		set => isReady = value;
	}
	
	public bool IsShooting
	{
		get => isShooting;
		set => isShooting = value;
	}
	
	public string Name 
	{
		get => name.ToString();
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
