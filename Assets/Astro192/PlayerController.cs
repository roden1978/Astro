using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Vector2 force;
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
    private bool isFacingLeft;
    private Animator animator;
    private bool crouch;
    private bool run;
    private bool walk;
    private bool isGround;
    private bool crouchButtonDown;
    private bool fire;
    [SerializeField] Joystick viewJoystick;
    [SerializeField] Button jumpButton;
    [SerializeField] Button crouchButton;
    [SerializeField] Button fireButton;
    [SerializeField] GameObject rightArm;
    [SerializeField] GameObject leftArm;
     private GameObject leftArmPointer;
    private GameObject weapon;
    private astroGun astrogun;
    [SerializeField] GameObject initialWeapon;
    [SerializeField] GameObject weaponPoint;


    // Start is called before the first frame update
    void Start()
    {
        force = new Vector2(70.0f, 5.0f);
        maxVelocity = 2.0f;
        isFacingLeft = false;
        crouch = false;
        crouchButtonDown = false;
        run = false;
        walk = false;
        playerRb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        Instantiate(initialWeapon, weaponPoint.transform);
        leftArmPointer = GameObject.Find("leftArmPoint");
        weapon = GameObject.FindGameObjectWithTag("weapon");
        astrogun = weapon.GetComponent<astroGun>();

        //Debug.Log("rightArm " + rightArm);
    }

    // Update is called once per frame
    void Update()
    {
        keyboardDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        joysticDirection = new Vector2(viewJoystick.Horizontal, viewJoystick.Vertical);


        if(joysticDirection.x != 0 || keyboardDirection.x != 0)
        {
            if(joysticDirection.x != 0)
            {
                direction.x = joysticDirection.x;
            }

            if (keyboardDirection.x != 0)
            {
                direction.x = keyboardDirection.x;
            }

        } else
        {
            ResetX();
        }

        if (joysticDirection.y != 0 || keyboardDirection.y != 0)
        {
            if(joysticDirection.y != 0)
            {
                //direction.y = joysticDirection.y;
            }

            if(keyboardDirection.y != 0)
            {
                direction.y = keyboardDirection.y;

            }
            
        }
        else
        {
            ResetY();
        }

        
        run = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && direction.x != 0;
        walk = ((int) playerRb2D.velocity.x) != 0;

        if ((direction.x > 0) && isFacingLeft)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if ((direction.x < 0) && !isFacingLeft)
            Flip();

        VelocityControl();
        RunOneHandGun(run);
        WalkOneHandGun(walk);
        MovingRightArm();

        IdleControl();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            astrogun.Shoot();
        }

        //Debug.Log("Horizontal " + viewJoystick.Horizontal.ToString() + " Vertical " + viewJoystick.Vertical.ToString());
    }

    private void FixedUpdate()
    {
        KeyboardJump();
        Crouch();
        Move();
        

        //print("position " + (rightArm.transform.localPosition.y + joysticDirection.y));
    }

    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingLeft = !isFacingLeft;

        transform.Rotate(0f, 180f, 0f);
    }

    private void Move()
    {
        if (direction.x != 0 && !crouch)
        {
           //WalkOneHandGun(true);
          /* if(viewJoystick.Horizontal > 0)
            {
                direction.x = viewJoystick.Horizontal; //(float) Math.Ceiling((double)viewJoystick.Horizontal);

            }

           if(viewJoystick.Horizontal < 0)
            {
                direction.x = viewJoystick.Horizontal; //(float)Math.Floor((double)viewJoystick.Horizontal);
            }*/
            
            playerRb2D.AddForce(new Vector2(force.x * direction.x, 0.0f), ForceMode2D.Force);
        }
        
    }

    private void KeyboardJump()
    {
        if (direction.y > 0 && prevKeyboardY != 1)
        {
            prevKeyboardY = direction.y;
            Jump(1.0f);
        }

    }

    public void Jump(float jump)
    {
        if (isGround)
        {

            animator.SetBool("jumpOneArm", true);
            playerRb2D.AddForce(new Vector2(0.0f, force.y * jump), ForceMode2D.Impulse);
            animator.SetBool("jumpOneArm", false);
        }
        else
        {
            
        }
    }

    public void Crouch()
    {
        if(direction.y < 0 || crouchButtonDown)
        {
            animator.SetBool("crouchOneArm", true);
            crouch = true;
            //SetCrouchButtonDown(false);
        }
        else
        {
            animator.SetBool("crouchOneArm", false);
            crouch = false;
        }
    }

    private void WalkOneHandGun(bool walk)
    {
        if(!run)
        {
            animator.SetBool("walkOneArm", walk);
            maxVelocity = 2.0f;
            //print("walk " + walk);
        }
        
    }

    private void VelocityControl()
    {
        if ((direction.x > 0 || viewJoystick.Horizontal != 0) && playerRb2D.velocity.x > maxVelocity)
        {
            playerRb2D.velocity = new Vector2(maxVelocity, playerRb2D.velocity.y);
        }
        else
        {
            if((direction.x < 0 || viewJoystick.Horizontal != 0) && playerRb2D.velocity.x < -maxVelocity)
            {
                playerRb2D.velocity = new Vector2(-maxVelocity, playerRb2D.velocity.y);
            }
        }
    }

    private void RunOneHandGun(bool run)
    {

        if (run)
        {
            animator.SetBool("runOneArm", run);
            maxVelocity = 5.0f;
        }
        
            //print("run " + run);
    }

    private void IdleControl()
    {
        if (direction.x == 0 || viewJoystick.Horizontal == 0)
        {
            walk = false;
            run = false;
        }
    }

    public void SetGround(bool isGround)
    {
        this.isGround = isGround;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void SetDirection(float x, float y)
    {
        direction.x = x;
        direction.y = y;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    private void ResetX()
    {
        direction.x = 0;
    }

    private void ResetY()
    {
        direction.y = prevKeyboardY = 0;
    }

    public void SetCrouchButtonDown(bool down)
    {
        this.crouchButtonDown = down;
    }

    public bool GetCrouchButtonDown()
    {
        return crouchButtonDown;
    }

    private void MovingRightArm()
    {
        Vector3 rightArmLocalPosition = rightArm.transform.localPosition;

        float armDelta;

        if (crouchButtonDown)
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
            leftArm.transform.position = leftArmPointer.transform.position;
            //print("current rightArm Position " + currentPosition + "RightArm local rot " + rightArm.transform.localRotation + " RA glo rot" + rightArm.transform.rotation);
            //print(Camera.main.ScreenToWorldPoint(currentPosition));
        }

        

    }
}
