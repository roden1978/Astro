using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmmoAbstract : ScriptableObject
{
	/*
	private float force; // мощность боеприпаса
	private float distance; // дистанция срабатывания устройства
	private float damageDistance; // дистанция нанесенияурона
	private float delay; //время задержки передвзрывом
	*/
	
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
	
	public abstract float Blow();
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