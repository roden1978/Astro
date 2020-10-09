using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    
	[SerializeField]
    private float speed;
    
	[SerializeField]
	private float attackSpeed;
	
	[SerializeField]
	private float patrolDistance;
	
	[SerializeField]
	private float attackDistance;
	
	[SerializeField]
	private float stopAngryDistance;
	
	[SerializeField]
	private float maxJumpVelocity;
	
	[SerializeField]
	private float maxBackVelocity;
	
	[SerializeField]
	private float jumpForce;
	
	[SerializeField]
	private float rayDistance;
	
	[SerializeField]
	private float backForce;
	
	private GameObject player;
	private Vector3 playerPos;
	private Vector3 lastPosition;
	private Animator animator;
	private Vector3 startPosition;
	
	private float spriteSizeX, spriteSizeY;
	
	private SpriteRenderer sr;
	
	private bool movingRight = true;
	private bool chill = false;
	private bool angry;
	private bool goBack;
	private bool attack;
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
	    
	    Physics2D.queriesStartInColliders = false;
	    
	    SpriteRendererFind();
	    
    }
    
	private void OnDrawGizmos() 
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y + 3, 0));
		
		
		Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + spriteSizeY / 2, 0), 
			new Vector3(transform.position.x + 1 * rayDistance, transform.position.y + spriteSizeY / 2, 0));
		//Gizmos.DrawLine(transform.position, transform.position + Vector3.left * rayDistance);
		Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y + spriteSizeY / 2, 0), 
			new Vector3(transform.position.x - 1 * rayDistance, transform.position.y + spriteSizeY / 2, 0));
	}

    // Update is called once per frame
    void Update()
    {
	    StartMoveAnimation();
	    JumpVelocityControll();
	    MovingDirectionControll();
	    
	    HitCollisionDetect();
	    
	    ObstacleJumpControll();
	    CheckAttackSuccess();
	    
	    JumpBackVelocityControll();
	}
    


    private void FixedUpdate()
    {
	    lastPosition = transform.position;
	    
	    if(Vector2.Distance(startPosition, transform.position) < patrolDistance && !angry && !chill)  //chill 
	    {
	    	chill = true;
	    	angry = false;
	    	goBack = false;
	    	attack = false;
	    }
	    else if (Vector2.Distance(transform.position, new Vector2 (player.transform.position.x ,0)) > stopAngryDistance && !goBack && !chill) //goBack
	    {
	    	goBack = true;
	    	angry = false;
	    	chill = false;
	    	attack = false;
	    	
	    	Flip();
	    	
	    	//Debug.Log("goBack " + Vector2.Distance(transform.position, player.transform.position));
	    	
	    } else if (Vector2.Distance(transform.position, 
		    new Vector2(player.transform.position.x, 0)) < stopAngryDistance && !angry) //Angry
	    {
	    	angry = true;
	    	chill = false;
	    	goBack = false;
	    	attack = false;
	    	
	    	Debug.Log("angry");
	    } 
	    
	    if(Vector2.Distance(transform.position, new Vector2(player.transform.position.x,0)) <= attackDistance)
	    {
	    
	    	attack = true;
	    }
	    
	    if (chill) Chill();
	    
	    if (angry) Angry();
	    
	    if (goBack) GoBack();
	    
	    if (attack) Attack();
	  
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
         
	    if (collision.gameObject.tag == "ground")
	    {
		    GetComponentInParent<SlimeController>().isGround = true;
	    }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
	   
	    if(collision.gameObject.tag == "ground")
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
		if(transform.position.x > startPosition.x + patrolDistance)
		{
			Flip();
		}
		else if (transform.position.x < startPosition.x - patrolDistance)
		{
			Flip();
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
		if (PlayerSideDetect() != movingRight)
			Flip();
			
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, 0), attackSpeed * Time.fixedDeltaTime);	
	}
	
	private void GoBack()
	{
		
		transform.position = Vector2.MoveTowards(transform.position, new Vector2(startPosition.x, 0), speed * Time.fixedDeltaTime);
	}
	
	private void Attack()
	{
		if (PlayerSideDetect() != movingRight)
			Flip();
			
		if(isGround)
		{
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			Debug.Log("attack");
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
	
	private void HitCollisionDetect()
	{
		if(movingRight)
		{
			//hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
			hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + spriteSizeY / 2), 
				new Vector2(transform.position.x + 1 * rayDistance, transform.position.y + spriteSizeY / 2), rayDistance);
			//Debug.Log("ray collider " + hit.collider);
		}
		else 
		{
			//hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, rayDistance);
			hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + spriteSizeY / 2), 
				new Vector2(transform.position.x - 1 * rayDistance, transform.position.y + spriteSizeY / 2), rayDistance);
			//Debug.Log("ray collider " + hit.collider);
		}
	}
	
	private void ObstacleJumpControll()
	{
		
	    
		if (hit && hit.collider.gameObject.tag == "ground")
		{
			if(isGround)
				rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}
	}
	
	private void CheckAttackSuccess()
	{
		if (hit && hit.collider.gameObject.tag == "Player" && !isGround)
		{
			Debug.Log("Player damage");
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
		
		return player.transform.position.x > transform.position.x ? true: false;
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
