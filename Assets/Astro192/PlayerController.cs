using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	#pragma warning disable 0649
	[SerializeField] float jumpForce;
	[SerializeField] Vector2 force;
	[SerializeField] Joystick viewJoystick;
	[SerializeField] Button jumpButton;
	[SerializeField] Button crouchButton;
	[SerializeField] Button fireButton;
	[SerializeField] Button runButton;
	[SerializeField] GameObject rightArm;
	[SerializeField] GameObject leftArm;
	[SerializeField] GameObject currentWeapon;
	[SerializeField] GameObject initialWeapon;
	[SerializeField] GameObject weaponPoint;
	[SerializeField] bool keyboardController;
	[SerializeField] bool uiController;
	#pragma warning restore 0649
    private Vector2 direction;
    private Vector2 joysticDirection;
    private Vector2 keyboardDirection;
    public float maxVelocity;
    private float prevKeyboardY;
    private float armLockPositionUp = 1.7f;
    private float armLockPositionDown = 0.3f;
    private float walkArmDelta = 1.296f;
    private float crouchArmDelta = 1.06f;
    
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
    
    private GameObject leftArmPointer;
    private GameObject weapon;
    private astroGun astrogun;
    



    // Start is called before the first frame update
    void Start()
    {
	    //force = new Vector2(70.0f, 0.0f);  //проверить нужен ли Vector2
        maxVelocity = 2.0f;
        crouch = false;
        crouchButtonDown = false;
        run = false;
        walk = false;
        isRunning = false;
        isIdle = true;
        playerRb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Instantiate(initialWeapon, weaponPoint.transform);
        leftArmPointer = GameObject.Find("leftArmPoint");
	    weapon = GameObject.FindGameObjectWithTag("weapon");
	    astrogun = weapon.GetComponent<astroGun>();

	    // Debug.Log("astrogun " + weapon);
    }


    // Update is called once per frame
    void Update()
    {
        ReadDeviceDirections();

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

    private void RunOneHandGun()
    {

        if (run && !isRunning)
        {
            animator.SetBool("walkOneArm", false);
            animator.SetBool("runOneArm", true);
            maxVelocity = 5.0f;

           /* print("running");
            print("Run " + run);
            print("isRunning " + isRunning);*/
        }

        if (!run && !isRunning && !isIdle)
        {
            animator.SetBool("runOneArm", false);
            animator.SetBool("walkOneArm", true);
            maxVelocity = 2.0f;

           /* print("walking");
            print("walk " + walk);*/

        }

        if (!run && !isRunning && !walk && isIdle)
        {
            animator.SetBool("runOneArm", false);
            animator.SetBool("walkOneArm", false);
            
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
        RunOneHandGun();
        //WalkOneHandGun(walk);
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
            Jump(jumpForce);
        }

    }

    public void Jump(float jump)
    {
        if (isGround && !crouchButtonDown)
        {
            animator.SetBool("jumpOneArm", true);
            playerRb2D.AddForce(new Vector2(0.0f, jump), ForceMode2D.Impulse);
            animator.SetBool("jumpOneArm", false);
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
            animator.SetBool("crouchOneArm", true);
            crouch = true;
            //crouchButtonDown = !crouchButtonDown;
        }
        else
        {
            animator.SetBool("crouchOneArm", false);
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
        get { return isGround; }
        set { isGround = value; }
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
        Vector3 rightArmLocalPosition = rightArm.transform.localPosition;

        float armDelta;

        if (crouch)
        {
            armDelta = crouchArmDelta;
        }
        else
        {
            armDelta = walkArmDelta;
        }


        Vector3 currentPosition = new Vector3(rightArmLocalPosition.x,
              joysticDirection.y + armDelta,
              rightArmLocalPosition.z);



        if (currentPosition.y >= armLockPositionDown
            && currentPosition.y <= armLockPositionUp)
        {
            rightArm.transform.localPosition = currentPosition;
	        leftArm.transform.position = weaponPoint.transform.position;//leftArmPointer.transform.position;
            //print("current rightArm Position " + currentPosition + "RightArm local rot " + rightArm.transform.localRotation + " RA glo rot" + rightArm.transform.rotation);
            //print(Camera.main.ScreenToWorldPoint(currentPosition));
        }



    }
}
