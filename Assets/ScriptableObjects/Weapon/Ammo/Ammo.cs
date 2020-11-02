using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Ammo", menuName = "Weapons/Ammo")]
public class Ammo : AmmoAbstract
{
	
	override public float Blow(){
		Debug.Log("Blow");
		return 0.0f;
	}
	
}


