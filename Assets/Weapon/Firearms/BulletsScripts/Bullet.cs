using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] Rigidbody2D rb;
	[SerializeField] float power;
	[SerializeField] float lifetime;
	[SerializeField] Weapon weapon;
	[SerializeField] GameObject vfxCollision;
#pragma warning restore 0649
	
	private Vector3 shootDirection;

	private Collider2D cc;
	void Start()
	{
		cc = transform.GetComponent<CapsuleCollider2D>();
		if (weapon)
		{
			shootDirection = weapon.TargetPoint.DirectionTo(weapon.ShootPoint); 
			rb.AddForce(shootDirection * power, ForceMode2D.Impulse);
			Destroy(gameObject, lifetime);
		}
	    
	}

	private void OnTriggerEnter2D(Collider2D hitObject)
	{
		if (vfxCollision) Instantiate(vfxCollision, cc.transform.position, Quaternion.identity);
		Destroy(gameObject);    
	}
}
