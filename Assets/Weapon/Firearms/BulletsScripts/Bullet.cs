using UnityEngine;

public class Bullet : MonoBehaviour
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
	void Start()
	{
		cc = transform.GetComponent<CapsuleCollider2D>();
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

	public void OnDisable()
	{
		CancelInvoke();
	}

	private void Destroy()
	{
		gameObject.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D hitObject)
	{
		if (vfxCollision) Instantiate(vfxCollision, cc.transform.position, Quaternion.identity);
		gameObject.SetActive(false);   
	}
}
