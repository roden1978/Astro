using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Firearms", menuName = "Weapons/Firearms/Weapon")]
public class Weapon : ScriptableObject
{
	#pragma warning disable 0649
	[SerializeField]
	WeaponNames name;
	
	[SerializeField]
	float rightArmLockPositionUp;
	
	[SerializeField]
	[Tooltip("right arm lock position down")]
	float rightArmLockPositionDown;
	
	[SerializeField]
	private AWeaponShoot weaponShoot;
	
	#pragma warning restore 0649
	
	private Vector3 shootPoint;
	private Vector3 targetPoint;
	

	
	//public void DrawShootDirectionRay(){
	//	if (weaponItem){
	//		Debug.DrawRay(weaponItem.transform.Find("shootPoint").position, 
	//			weaponItem.transform.Find("targetPoint").position - weaponItem.transform.Find("shootPoint").position, Color.red);
	//	}
	//}
	
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
