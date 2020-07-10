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
    private Rigidbody2D playerRb2D;
    private bool isFacingLeft;
    private Animator animator;
    private bool crouch;
    private bool run;
    private bool walk;
    private bool isGround;
    [SerializeField] Joystick viewJoystick;
    [SerializeField] Button jumpButton;
    [SerializeField] Button crouchButton;
    [SerializeField] Button fireButton;

    // Start is called before the first frame update
    void Start()
    {
        force = new Vector2(50.0f, 3.0f);
        maxVelocity = 2.0f;
        isFacingLeft = false;
        crouch = false;
        run = false;
        walk = false;
        playerRb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        walk = direction.x != 0 || viewJoystick.Horizontal != 0;

        if ((direction.x > 0) && isFacingLeft)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if ((direction.x < 0) && !isFacingLeft)
            Flip();

        VelocityControl();
        RunOneHandGun(run);
        WalkOneHandGun(walk);

        IdleControl();



       // Debug.Log("Horizontal " + viewJoystick.Horizontal.ToString() + " Vertical " + viewJoystick.Horizontal.ToString());
    }

    private void FixedUpdate()
    {
        KeyboardJump();
        Crouch();
        Move();
    }

    private void Flip()
    {
        //меняем направление движения персонажа
        isFacingLeft = !isFacingLeft;

        transform.Rotate(0f, 180f, 0f);
    }

    private void Move()
    {
        if ((direction.x !=0) && !crouch)
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
        }
        else
        {
            animator.SetBool("jumpOneArm", false);
        }
    }

    private void Crouch()
    {
        if(direction.y < 0)
        {
            animator.SetBool("crouchOneArm", true);
            crouch = true;
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
            animator.SetBool("runOneArm", run);
            maxVelocity = 5.0f;
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

}
