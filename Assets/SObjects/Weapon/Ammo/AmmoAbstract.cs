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
	public abstract float Blow();
}
