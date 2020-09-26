﻿using System.Collections;
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
	private Vector3 startPosition;
	[SerializeField]
	private float stopAngryDistance;
	[SerializeField]
	private float maxJumpVelocity;
	[SerializeField]
	private float jumpForce;
	//[SerializeField]
	//private float rayDistance;
	//private Vector2 rayStartPosition;
	private GameObject player;
	private Vector3 playerPos;
	private Vector3 lastPosition;
	//private Vector2 attackPos;
	
	private bool movingRight = true;
	
	private bool chill = false;
	private bool angry;
	private bool goBack;
	
	
	
    private Animator animator;
	private bool isOnAnimationMove;
    private bool isGround;
    // Start is called before the first frame update
    void Start()
    {
	    animator = GetComponent<Animator>();
	    if(!animator) Debug.Log("Slime controller Animator component not found");
	    
	    player = GameObject.FindGameObjectWithTag("Player");
	    if(!player) Debug.Log("Slime controller game object Player not found");
	    
	    isOnAnimationMove = false;
	    startPosition = transform.position;
	    
	    //Debug.Log("startPosition " + startPosition);
    }
    
	private void OnDrawGizmos() 
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(startPosition, new Vector3(startPosition.x, startPosition.y + 3, 0));
	}

    // Update is called once per frame
    void Update()
    {
	    StartMoveAnimation();
	    JumpVelocityControll();
	    MovingDirectionControll();
    }
    


    private void FixedUpdate()
    {
	    lastPosition = transform.position;
	    
	    if(Vector2.Distance(startPosition, transform.position) < patrolDistance && !angry && !chill)  //chill 
	    {
	    	chill = true;
	    	angry = false;
	    	goBack = false;
	    	
	    	Debug.Log("chill");
	    }
	    else if (Vector2.Distance(transform.position, player.transform.position) > stopAngryDistance && !goBack && !chill) //goBack
	    {
	    	goBack = true;
	    	angry = false;
	    	chill = false;
	    	
	    	Flip();
	    	
	    	Debug.Log("goBack " + Vector2.Distance(transform.position, player.transform.position));
	    	
	    } else if (Vector2.Distance(transform.position, player.transform.position) < stopAngryDistance && !angry) //Angry
	    {
	    	angry = true;
	    	chill = false;
	    	goBack = false;
	    	
	    	Debug.Log("angry");
	    } 
	    
	    if(Vector2.Distance(transform.position, player.transform.position) <= attackDistance)
	    {
	    	Attack();
	    }
	    
	    if (chill)
	    {
	    	Chill();
	    }
	    
	    if (angry)
	    {
	    	Angry();
	    }
	    
	    if(goBack)
	    {
	    	
	    	GoBack();
	    }
	  
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
			//movingRight = false;
			Flip();
		}
		else if (transform.position.x < startPosition.x - patrolDistance)
		{
			//movingRight = true;
			Flip();
		}
		
		if (movingRight)
		{
			transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
		}
		else 
		{
			transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);	
		}
		
	}
	
	private void Angry()
	{
		if (PlayerSideDetect() != movingRight)
			Flip();
			
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, attackSpeed * Time.deltaTime);	
	}
	
	private void GoBack()
	{
		
		transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);		
	}
	
	private void Attack()
	{
		if (PlayerSideDetect() != movingRight)
			Flip();
			
		if(isGround)
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}
	
	private void StartMoveAnimation(){
		if(isGround && !isOnAnimationMove){
			animator.SetBool("isSliming", true);
			isOnAnimationMove = true;
			Debug.Log("Start Moving Animation");
		}
		
		if (!isGround && isOnAnimationMove){
			animator.SetBool("isSliming", false);
			Debug.Log("Stop Moving Animation");
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
	
	private void MovingDirectionControll()
	{
		if (lastPosition.x < transform.position.x && !movingRight)
			Flip();
		else if (lastPosition.x > transform.position.x && movingRight)
			Flip();
	}
		
}
