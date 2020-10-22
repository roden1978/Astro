using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ammo", menuName = "Weapons/Ammo")]
public class Ammo : AmmoAbstract
{
	#pragma warning disable 0649

	[SerializeField]
	private Device device;
	
	[SerializeField]
	private Type type;
	
	[SerializeField]
	private Sprite image;
	
	[SerializeField]
	private new string name; //имя
	
	[SerializeField]
	private float force; // мощность боеприпаса
	
	[SerializeField]
	private float distance; // дистанция срабатывания устройства
	
	[SerializeField]
	private float damageDistance; // дистанция нанесенияурона
	
	[SerializeField]
	private float delay; //время задержки передвзры
	#pragma warning restore 0649
	
	
	override public float Blow(){
		Debug.Log("Blow");
		return 0.0f;
	}
	
}

	public enum Device {
		Grenade,
		Mine
	}
	

	public enum Type {
		Frag,
		Electro,
		Plasma
	}
