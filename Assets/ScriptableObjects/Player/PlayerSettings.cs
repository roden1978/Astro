﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Player", menuName = "Players/Player")]
public class PlayerSettings : ScriptableObject
{
	#pragma warning disable 0649
	[SerializeField] [Tooltip("Имя игрока")]
	private new string name;
	
	[SerializeField]
	private Sprite imagePlayer;
	
	[SerializeField] [Tooltip("Максимальное здоровье")]
	private int maxHealthPoints;
	
	[SerializeField] [Tooltip("Текущий уровень здоровья")]
	private int currentHealthPoints;
	
	[SerializeField]
	[Tooltip("Текущий уровень брони костюма")]
	private int currentSuitPoints;
	
	[SerializeField] [Tooltip("Текщиее количество энергии оружия")]
	private int currentWeaponPoints;
	
	[SerializeField] [Tooltip("Максимальная энергия костюма")]
	private int maxSuitPoints;
	
	[SerializeField] [Tooltip("Максимальная энергия оружия")]
	private int maxWeaponPoints;
		
	[SerializeField] [Tooltip("Список оружия у игрока")]
	private List<GameObject> weapons;

	[SerializeField] [Tooltip("Текущее оружие")]
	private string currentWeaponName;
	
	[SerializeField] [Tooltip("Кнопка прыжка")]
	public Button jumpButton;
	
	[SerializeField] [Tooltip("Кнопка приседания")]
	public Button crouchButton;
	
	[SerializeField] [Tooltip("Кнопка стрельбы")]
	public Button fireButton;
	
	[SerializeField] [Tooltip("Кнопка бега")]
	public Button runButton;

	[SerializeField] [Tooltip("Текущее оружие")]
	private GameObject currentWeapon;
	#pragma warning restore 0649
	

	private bool alive = true;
	
	public void IncrementHealth(int healthValue)
	{
		Debug.Log("Increment health points");
		
		currentHealthPoints += healthValue;
			if(currentHealthPoints > maxHealthPoints)
			{
				currentHealthPoints = maxHealthPoints;
			}
		
	}
	
	public void DecrementHealth(int healthValue)
	{
		Debug.Log("Decrement health points");
		currentHealthPoints -= healthValue;
		if(healthValue < 0) 
		{
			alive = false;
		}
	}
	
	public void ChangeSuitPoints()
	{
		Debug.Log("Change suit points");
	}
	
	public void ChangeWeapon()
	{
		Debug.Log("Weapon change");
	}
	
	
	
	public List<GameObject> Weapons
	{
		get => weapons;
	}
	
	public bool Alive
	{
		get => alive;
		set => alive = value;
	}

	public GameObject CurrentWeapon
	{
		get => currentWeapon;
		set => currentWeapon = value;
	}

	public string CurrentWeaponName
	{
		get => currentWeaponName;
		set => currentWeaponName = value;
	}
}
