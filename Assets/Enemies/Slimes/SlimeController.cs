using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
	#pragma warning disable 0649
	[SerializeField]
	[Tooltip("Физическое тело")]
    private Rigidbody2D rb;
    
	[SerializeField]
	[Tooltip("Скорость при патрулировании")]
    private float speed;
    
	[SerializeField]
	[Tooltip("Скорость при погоне за игроком")]
	private float attackSpeed;
	
	[SerializeField]
	[Tooltip("Дистанция патрулирования")]
	private float patrolDistance;
	
	[SerializeField]
	[Tooltip("Дистанция при которой начинается атака")]
	private float attackDistance;
	
	[SerializeField]
	[Tooltip("Дистанция при которой завершается погоня")]
	private float stopAngryDistance;
	
	[SerializeField]
	[Tooltip("Максимальное ускорение во время прыжка")]
	private float maxJumpVelocity;
	
	[SerializeField]
	[Tooltip("Максимальное ускорение при отскоке после атаки")]
	private float maxBackVelocity;
	
	[SerializeField]
	[Tooltip("Сила прыжка")]
	private float jumpForce;
	
	[SerializeField]
	[Tooltip("Длина луча контроля")]
	private float rayDistance;
	
	[SerializeField]
	[Tooltip("Сила отскока")]
	private float backForce;
	#pragma warning restore 0649
	
	private GameObject player;//Игрок
	private Vector3 playerPos;
	private Vector3 lastPosition;
	private Animator animator;
	private Vector3 startPosition;
	
	private float spriteSizeX, spriteSizeY;
	
	private SpriteRenderer sr;
	
	private bool movingRight = true;
	private bool chill = false;
	private bool angry = false;
	private bool goBack = false;
	private bool attack = false;
	private bool isOnAnimationMove;
	private bool isGround;
    
	private RaycastHit2D hit;
	
    // Start is called before the first frame update
    void Start()
    {
	    animator = GetComponent<Animator>();
	    if(!animator) Debug.Log("Slime controller Animator component not found");
	    
	    player = GameObject.FindGameObjectWithTag("Player");
	    if(!player) Debug.Log("Slime controller game object Player not found");
	    
	    isOnAnimationMove = false;
	    startPosition = transform.position;
	    
	    hit = new RaycastHit2D();
	    
	    Physics2D.queriesStartInColliders = false;
	    
	    SpriteRendererFind();
	    
    }
    
	private void OnDrawGizmos() 
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y + 3, 0));
		
		if (movingRight)
			Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + spriteSizeY / 2, 0), 
				new Vector3(transform.position.x + 1 * rayDistance, transform.position.y + spriteSizeY / 2, 0));
			//Gizmos.DrawLine(transform.localPosition * 0.5f, Vector3.right);
		else
			Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + spriteSizeY / 2, 0), 
				new Vector3(transform.position.x - 1 * rayDistance, transform.position.y + spriteSizeY / 2, 0));
			//Gizmos.DrawLine(transform.localPosition * 0.5f, Vector3.left);
	}

    // Update is called once per frame
    void Update()
    {
	    StartMoveAnimation();
	    JumpVelocityControll();
	    MovingDirectionControll();
	    
	    //HitCollisionDetect();
	    
	    ObstacleJumpControll();
	    CheckAttackSuccess();
	    
	    JumpBackVelocityControll();
	}
    
    private void FixedUpdate()
    {
	    lastPosition = transform.position;
	    
	    if(Vector2.Distance(startPosition, transform.position) < patrolDistance && !angry && !chill && !attack)  //chill 
	    {
	    	chill = true;
	    	angry = false;
	    	goBack = false;
	    	attack = false;
	    	//Debug.Log("chill ");
	    }
		    
	    if (Vector2.Distance(transform.position, player.transform.position) > stopAngryDistance && !goBack && !chill) //goBack
	    {
	    	goBack = true;
	    	angry = false;
	    	chill = false;
	    	attack = false;
	    	
	    	Flip();
	    	
	    	//Debug.Log("goBack ");
	    	
	    } 
	    
	    if (Vector2.Distance(transform.position, 
		    new Vector2(player.transform.position.x, 0)) < stopAngryDistance && !angry && !attack) //Angry
	    {
	    	angry = true;
	    	chill = false;
	    	goBack = false;
	    	attack = false;
	    	
	    	//Debug.Log("angry");
	    } 
	    
	    if(Vector2.Distance(transform.position, new Vector2(player.transform.position.x,0)) <= attackDistance)
	    	attack = true;
	    else
		    attack = false;
	    
	    if (chill) Chill();
	    
	    if (angry) Angry();
	    
	    if (goBack) GoBack();
	    
	    if (attack) Attack();
	    	  
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
         
	    if (collision.gameObject.CompareTag("ground"))
	    {
		    GetComponentInParent<SlimeController>().isGround = true;
	    }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
	   
	    if(collision.gameObject.CompareTag("ground"))
	    {
		    GetComponentInParent<SlimeController>().isGround = false;
		    isOnAnimationMove = false;
	    }
    }

    public bool Ground
    {
        get
        {
            return isGround;
        }

        set
        {
            isGround = value;
        }
    }
    
	private void Chill()
	{
		if(transform.position.x > startPosition.x + patrolDistance && movingRight)
		{
			Flip();
			//Debug.Log("patrol +");
		}
		else if (transform.position.x < startPosition.x - patrolDistance && !movingRight)
		{
			Flip();
			//Debug.Log("patrol -");
		}
		
		if (movingRight)
		{
			transform.position = new Vector2(transform.position.x + speed * Time.fixedDeltaTime, transform.position.y);
		}
		else 
		{
			transform.position = new Vector2(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y);	
		}
		
	}
	
	private void Angry()
	{
		if (PlayerSideDetect() != movingRight){
			Flip();
		}
			
			
		if(isGround)
			transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, 0), attackSpeed * Time.fixedDeltaTime);	
	}
	
	private void GoBack()
	{
		
		transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.fixedDeltaTime);
	}
	
	private void Attack()
	{
		if (PlayerSideDetect() != movingRight)
		{
			Flip();
		}
			
			
		if(isGround)
		{
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			//Debug.Log("attack");
		}
			
			
		
			
		isOnAnimationMove = true;
	}
	
	private void StartMoveAnimation(){
		if(isGround && !isOnAnimationMove){
			animator.SetBool("isSliming", true);
			animator.SetBool("isAttack", false);
			isOnAnimationMove = true;
			//Debug.Log("Start Moving Animation");
		} else 
			if (!isGround && isOnAnimationMove){
				animator.SetBool("isAttack", true);
			isOnAnimationMove = false;
				//Debug.Log("Stop Moving Animation");
		}
	}
	
	private RaycastHit2D HitCollisionDetect()
	{
		if(movingRight)
		{
			//Debug.Log($"Moving right = {movingRight}");
			return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + spriteSizeY / 2), 
				new Vector2(transform.position.x + 1 * rayDistance, transform.position.y + spriteSizeY / 2), rayDistance);
			///return Physics2D.Raycast(transform.position,Vector2.right, rayDistance);
			
		}
		else 
		{
			//Debug.Log($"Moving right = {movingRight}");
			return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + spriteSizeY / 2), 
				new Vector2(transform.position.x - 1 * rayDistance, transform.position.y + spriteSizeY / 2), rayDistance);
			
			//return Physics2D.Raycast(transform.position,Vector2.left, rayDistance);
		}
	}
	
	private void ObstacleJumpControll()
	{
		hit = HitCollisionDetect();
	   Debug.Log(hit.collider);
		if (hit.collider && hit.collider.CompareTag("ground"))
		{
			if(isGround)
				rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			Debug.Log($"Collider tag = {hit.collider.gameObject.tag}"); 
			
		}
	}
	
	private void CheckAttackSuccess()
	{
		hit = HitCollisionDetect();
		if (hit.collider && hit.collider.CompareTag("Player") && !isGround)
		{
			//Debug.Log("Player damage");
			if(movingRight)
			{
				rb.AddForce(Vector2.left * backForce, ForceMode2D.Impulse);
			}
			else
			{
				rb.AddForce(Vector2.right * backForce, ForceMode2D.Impulse);
			}
		}
	}
	
	public void Flip()
	{
		//меняем направление движения персонажа
		
		movingRight = !movingRight;
		transform.Rotate(0f, 180f, 0f);
			
		//Debug.Log("flip");
		
	}
	
	private bool PlayerSideDetect()
	{
		//right side true, left side false
		
		return player.transform.position.x > transform.position.x;
		//Debug.Log("player side detect");
	}
	
	private void JumpVelocityControll()
	{
		if (rb.velocity.y > maxJumpVelocity)
			rb.velocity = new Vector2(0, maxJumpVelocity);
	}
	
	private void JumpBackVelocityControll()
	{
		if (rb.velocity.x != 0)
		{
			if(rb.velocity.x > 0 && rb.velocity.x > maxBackVelocity)
				rb.velocity = new Vector2(maxBackVelocity, rb.velocity.y);
			else if (rb.velocity.x < 0 && rb.velocity.x < -maxBackVelocity)
				rb.velocity = new Vector2(-maxBackVelocity, rb.velocity.y);
		}
	}
	
	private void MovingDirectionControll()
	{
		if (lastPosition.x < transform.position.x && !movingRight)
			Flip();
		else if (lastPosition.x > transform.position.x && movingRight)
			Flip();
	}
	
	private void SpriteRendererFind()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			if(transform.GetChild(i).name == "slimeSprite")
			{
				sr = transform.GetChild(i).GetComponent<SpriteRenderer>();
	    		
				if (sr) 
				{
					spriteSizeX = sr.size.x;
					spriteSizeY = sr.size.y;
				}
			}
		}
	}
	
}
