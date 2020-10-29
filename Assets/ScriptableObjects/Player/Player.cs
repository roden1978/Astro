using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Player", menuName = "Players/Player")]
public class Player : ScriptableObject
{
	#pragma warning disable 0649
	[SerializeField]
	[Tooltip("Имя игрока")]
	private new string name;
	
	[SerializeField]
	private Sprite imagePlayer;
	
	[SerializeField]
	[Tooltip("Максимальное здоровье")]
	private int maxHealthPoints;
	
	[SerializeField]
	[Tooltip("Текущий уровень здоровья")]
	private int currentHealthPoints;
	
	[SerializeField]
	[Tooltip("Текущий уровень брони костюма")]
	private int currentSuitPoints;
	
	[SerializeField]
	[Tooltip("Текщиее количество энергии оружия")]
	private int currentWeaponPoints;
	
	[SerializeField]
	[Tooltip("Максимальная энергия костюма")]
	private int maxSuitPoints;
	
	[SerializeField]
	[Tooltip("Максимальная энергия оружия")]
	private int maxWeaponPoints;
	
	[SerializeField]
	[Tooltip("Текущее оружие")]
	private GameObject currentWeapon;
	
	[SerializeField]
	[Tooltip("Стартовое оружие")]
	private GameObject initialWeapon;
	
	[SerializeField]
	[Tooltip("Список оружия у игрока")]
	private List<GameObject> weapons;
	
	[SerializeField] 
	[Tooltip("Джойстик для управления игроком")]
	public Joystick viewJoystick;
	
	[SerializeField]
	[Tooltip("Кнопка прыжка")]
	public Button jumpButton;
	
	[SerializeField]
	[Tooltip("Кнопка приседания")]
	public Button crouchButton;
	
	[SerializeField]
	[Tooltip("Кнопка стрельбы")]
	public Button fireButton;
	
	[SerializeField]
	[Tooltip("Кнопка бега")]
	public Button runButton;
	
	[SerializeField]
	[Tooltip("Точка крепления оружия")]
	private GameObject weaponPoint;
	
	[SerializeField]
	[Tooltip("Верхняя точка движения левой руки вверх (по умолчанию 0.15)")]
	private float leftArmLockPositionUp; // default 0.15f;
	
	[SerializeField]
	[Tooltip("Нижняя точка движения левой руки вниз (по умолчанию -0.15)")]
	private float leftArmLockPositionDown; // dfault -0.15f;
	#pragma warning restore 0649
	
	public GameObject InitWeapon ()
	{
		currentWeapon = Instantiate(initialWeapon, weaponPoint.transform) as GameObject;
		currentWeapon.name.Replace("(Clone)", "");
	}
	
	
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
	
	
	
	public GameObject CurrentWeapon
	{
		get {return currentWeapon;}
		set {currentWeapon = value;}
	}
}
