using UnityEngine;
using UnityEngine.Events;

public class Bullet : ABullet
{
#pragma warning disable 0649
	[SerializeField] Rigidbody2D rb;
	[SerializeField] float power;
	[SerializeField] float lifetime;
	[SerializeField] WeaponSettings weapon;
	[SerializeField] GameObject vfxCollision;
	
#pragma warning restore 0649
	
	private Vector3 shootDirection;
	private Collider2D cc;
	private AEnemy enemy;
	void Start()
	{
		cc = transform.GetComponent<CapsuleCollider2D>();
		damage = weapon.Damage;
		showEffect.AddListener(Play);
	}

	public void OnEnable()
	{
		Invoke("Destroy", lifetime);
		if (weapon)
		{
			shootDirection = weapon.ShootPoint.DirectionTo(weapon.TargetPoint); 
			rb.AddForce(shootDirection * power, ForceMode2D.Impulse);
		}
	}

	public void OnDisable() { CancelInvoke(); }

	private void Destroy() { gameObject.SetActive(false); }

	private void OnTriggerEnter2D(Collider2D hitObject)
	{
		showEffect?.Invoke();
		Debug.Log($"HitObject = {hitObject.name}, Type = {hitObject.gameObject.GetType()}");
		if (hitObject.gameObject.CompareTag("enemie"))
		{
			if(!enemy) enemy = hitObject.gameObject.GetComponent<AEnemy>();
			enemy.OnDamage?.Invoke(damage);
		}
	}

	private void Play()
	{
		if (vfxCollision) Instantiate(vfxCollision, cc.transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}
}
