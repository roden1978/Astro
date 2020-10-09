using UnityEngine;
using UnityEngine.UI;

public class PController : MonoBehaviour
{
	//public Vector2 force;
	private Vector2 direction;
	private Vector2 joysticDirection;
	private Vector2 keyboardDirection;
	[SerializeField]
	private float maxVelocity;
	private float prevKeyboardY;
	private float armLockPositionUp = 0.3f;
	private float armLockPositionDown = -0.3f;
	private float walkArmDelta = 1.296f;
	private float crouchArmDelta = 1.06f;
	[SerializeField] float jumpForce;
	[SerializeField] Vector2 force;
	private Rigidbody2D playerRb2D;
	//private bool isFacingLeft;
	private Animator animator;
	private bool crouch;
	private bool run;
	private bool walk;
	private bool isGround;
	private bool crouchButtonDown;
	private bool isRunning;
	private bool isIdle;
	[SerializeField] Joystick viewJoystick;
	[SerializeField] Button jumpButton;
	[SerializeField] Button crouchButton;
	[SerializeField] Button fireButton;
	[SerializeField] Button runButton;
	[SerializeField] GameObject rightArm;
	[SerializeField] GameObject leftArm;
	[SerializeField] GameObject currentWeapon;
	//private GameObject leftArmPointer;
	private GameObject weapon;
	private astroGun astrogun;
	[SerializeField] GameObject initialWeapon;
	[SerializeField] GameObject weaponPoint;
	//[SerializeField] GameObject target;

	[SerializeField] bool keyboardController;
	[SerializeField] bool uiController;
	private Transform rightArmWeaponPoint;



	// Start is called before the first frame update
	void Start()
	{
		//force = new Vector2(70.0f, 0.0f);  //проверить нужен ли Vector2
		//maxVelocity = 2.0f;
		crouch = false;
		crouchButtonDown = false;
		run = false;
		walk = false;
		isRunning = false;
		isIdle = true;
		isGround = true;
		playerRb2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		weapon = Instantiate(initialWeapon, weaponPoint.transform) as GameObject;
		//leftArmPointer = GameObject.Find("leftArmPoint");
		//weapon = GameObject.FindGameObjectWithTag("weapon");
		astrogun = weapon.GetComponent<astroGun>();
		//rightArmWeaponPoint = weapon.transform.Find("rightArmPoint");
		
		//target.transform.localPosition = new Vector3(rightArmWeaponPoint.localPosition.x, rightArmWeaponPoint.localPosition.y,rightArmWeaponPoint.localPosition.z);
		
		//if(WeponRightArmPointFind()) print("find"); 
		//rightArm.transform.position = weapon.transform.FindChild("rightArmPoint").position + new Vector3(0,2 ,0);
		//Debug.Log("rightArmPoint " + rightArmWeaponPoint.position);
	}

	//private Transform WeponRightArmPointFind()
	//{
	//	for (int i = 0; i < weapon.transform.childCount; i++)
	//	{
	//		if(weapon.transform.GetChild(i).name == "rightArmPoint")
	//		{
	//			return weapon.transform.GetChild(i).FindChild(;
	//		}
	//		else{
	//			return null;
	//		}
	//	}
		
	//	return null;
	//}
	// Update is called once per frame
	void Update()
	{
		ReadDeviceDirections();
		
		//	rightArmWeaponPoint = weapon.transform.Find("rightArmPoint");
		
		
		//if(WeponRightArmPointFind()) print("find"); 
		//rightArm.transform.position = weapon.transform.FindChild("rightArmPoint").position + new Vector3(0,2 ,0);
		//	Debug.Log("rightArmPoint " + rightArmWeaponPoint.position);

		//if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && direction.x != 0)
		//{
		//    run = true;
		//    walk = false;
		//    isIdle = false;
		//    print("run");
		//}else if (((int)playerRb2D.velocity.x) != 0)
		//{
		//    walk = true;
		//    run = false;
		//    isRunning = false;
		//    isIdle = false;
            
		//    print("walk " + walk);
		//    print("run " + run);
		//    print("isRunning " + isRunning);
		//} else
		//{
		//    walk = false;
		//    run = false;
		//    isRunning = false;
		//    isIdle = true;
		//    print("idle");
		//}
            

		/*if ()
		walk = true;
		else walk = false;*/

		MovingRightArm();

		//IdleControl();

		//if (Input.GetKeyDown(KeyCode.LeftControl))
		//{
		//    astrogun.Shoot();
		//}

		//Debug.Log("Horizontal " + viewJoystick.Horizontal.ToString() + " Vertical " + viewJoystick.Vertical.ToString());
	}

	private void Run()
	{

		if (run && !isRunning && isGround)
		{
			animator.SetBool("walk", false);
			animator.SetBool("run", true);
			//animator.SetBool("jump", false);
			maxVelocity = 5.0f;

			//	print("running");
			//print("Run " + run);
			//	print("isRunning " + isRunning);
		}

		if (!run && !isRunning && !isIdle && isGround)
		{
			animator.SetBool("run", false);
			animator.SetBool("walk", true);
			//animator.SetBool("jump", false);
			maxVelocity = 2.0f;

			//	 print("walking");
			//	print("walk " + walk);

		}

		if (!run && !isRunning && !walk && isIdle && isGround)
		{
			animator.SetBool("run", false);
			animator.SetBool("walk", false);
			//animator.SetBool("jump", false);
            
			//print("Idling");

		}

		//print("run " + run);
	}

	private void ReadDeviceDirections()
	{
		//Считываем нажатие клавиш управления на клавиатуре и изменение положения джойстика
		keyboardDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		joysticDirection = new Vector2(viewJoystick.Horizontal, viewJoystick.Vertical);

		if (joysticDirection.x != 0 || keyboardDirection.x != 0)
		{

			if (keyboardDirection.x != 0)
			{
				direction.x = keyboardDirection.x;
			}

			if (joysticDirection.x != 0)
			{
				direction.x = joysticDirection.x;
			}
		}
		else
		{
			ResetX();
		}

		if (keyboardDirection.y != 0)
		{

			if (keyboardDirection.y != 0)
			{
				direction.y = keyboardDirection.y;
			}

			if (keyboardDirection.y < 0 && !crouch)
			{
				crouchButtonDown = !crouchButtonDown;
				crouch = true;
			}

		}
		else
		{
			ResetY();
		}
        
		//Считываем нажатие клавиши Shift для бега
		if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && direction.x != 0)
		{
			run = true;
			walk = false;
			isIdle = false;
			//print("run");
		}else if (((int)playerRb2D.velocity.x) != 0)
		{
			walk = true;
			run = false;
			isRunning = false;
			isIdle = false;
            
			//print("walk " + walk);
			//print("run " + run);
			//print("isRunning " + isRunning);
		} else
		{
			walk = false;
			run = false;
			isRunning = false;
			isIdle = true;
			//print("idle");
		}
	    
		//Считываем нажатие Ctrl для выстрела
		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			astrogun.Shoot();
		}
	}

	private void FixedUpdate()
	{
		KeyboardJump();
		Crouch();
		Move();
		VelocityControl();
		Run();
		
		if(isGround)
		{
			animator.SetBool("jump", false);
			Debug.Log("jump off");
		}
		else 
		{
			animator.SetBool("jump", true);
			Debug.Log("jump");
		}
			
	}


	private void Move()
	{
		if (direction.x != 0 && !crouchButtonDown)
		{
			playerRb2D.AddForce(new Vector2(force.x * direction.x, 0.0f), ForceMode2D.Force);
		}

	}

	private void KeyboardJump()
	{
		if (direction.y > 0 && prevKeyboardY != 1)
		{
			prevKeyboardY = direction.y;
			Jump();
		}

	}

	public void Jump()
	{
		if (isGround && !crouchButtonDown)
		{
			//animator.SetBool("jump", true);
			playerRb2D.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
			//animator.SetBool("jump", false);
			Debug.Log("jump");
		}
		else
		{
			crouchButtonDown = false;
			crouch = false;
		}
	}

	public void Crouch()
	{
		if (crouchButtonDown)
		{
			animator.SetBool("crouch", true);
			crouch = true;
			//crouchButtonDown = !crouchButtonDown;
		}
		else
		{
			animator.SetBool("crouch", false);
			crouch = false;
		}
	}

	/* private void WalkOneHandGun(bool walk)
	{
	if (!run)
	{
	animator.SetBool("runOneArm", run);
	animator.SetBool("walkOneArm", walk);
	maxVelocity = 2.0f;
	//print("walk " + walk);
	}

	}*/

   

	private void VelocityControl()
	{
		if ((direction.x > 0) && playerRb2D.velocity.x > maxVelocity)
		{
			playerRb2D.velocity = new Vector2(maxVelocity, playerRb2D.velocity.y);
		}
		else
		{
			if ((direction.x < 0) && playerRb2D.velocity.x < -maxVelocity)
			{
				playerRb2D.velocity = new Vector2(-maxVelocity, playerRb2D.velocity.y);
			}
		}
	}



	private void IdleControl()
	{
		if (direction.x == 0)
		{
			walk = false;
			run = false;
			isRunning = false;
		}
	}


	public bool Ground 
	{
		get
		{return isGround;}
		
		set{isGround = value;}
	}


	public Vector2 Direction
	{
		get
		{
			return direction;
		}

		set
		{
			direction = value;
		}
	}

	private void ResetX()
	{
		direction.x = 0;
	}

	private void ResetY()
	{
		direction.y = prevKeyboardY = 0;
	}


	public bool CrouchButtonDown
	{
		get
		{
			return crouchButtonDown;
		}

		set
		{
			crouchButtonDown = value;
		}
	}


	private void MovingRightArm()
	{
		Vector3 leftArmLocalPosition = leftArm.transform.localPosition;

		float armDelta;

		if (crouch)
		{
			armDelta = crouchArmDelta;
		}
		else
		{
			armDelta = walkArmDelta;
		}


		Vector3 currentPosition = new Vector3(leftArmLocalPosition.x,
			joysticDirection.y * .5f,
			leftArmLocalPosition.z);



		if (currentPosition.y >= armLockPositionDown
			&& currentPosition.y <= armLockPositionUp)
		{
		leftArm.transform.localPosition = rightArm.transform.localPosition = currentPosition;
		
		//rightArm.transform.position = new Vector3(rightArm.transform.position.x, weapon.transform.Find("rightArmPoint").position.y, rightArm.transform.position.z);//weaponPoint.transform.position;//leftArmPointer.transform.position;
		//print("current lefttArm Position " + currentPosition);
			//print(Camera.main.ScreenToWorldPoint(currentPosition));
		}
		Debug.Log("currentPosition " + currentPosition);
		//leftArm.transform.position = currentPosition;
		//rightArm.transform.position = weaponPoint.transform.position;
		//print("current lefttArm Position " + currentPosition + "RightArm local rot " + rightArm.transform.localRotation + " RA glo rot " + rightArm.transform.rotation);

	}
}
