﻿using UnityEngine;
using UnityEngine.UI;

public class PController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Player player;
    [SerializeField] private float maxVelocity;
    [SerializeField] float jumpForce;
    [SerializeField] Vector2 force;
    [SerializeField] GameObject rightArm;
    [SerializeField] GameObject leftArm;

    [SerializeField] [Tooltip("Точка крепления оружия")]
    private GameObject weaponPoint;

    [SerializeField] [Tooltip("Текущее оружие")]
    private GameObject currentWeapon;

    [SerializeField] [Tooltip("Стартовое оружие")]
    private GameObject initialWeapon;

    [SerializeField] [Tooltip("Джойстик для управления игроком")]
    private Joystick viewJoystick;

    [SerializeField] bool keyboardController;
    [SerializeField] bool uiController;

#pragma warning restore 0649

    //public Vector2 force;
    private Vector2 direction;
    private Vector2 joysticDirection;
    private Vector2 keyboardDirection;

    private float prevKeyboardY;

    private float leftArmLockPositionUp = 0.15f;
    private float leftArmLockPositionDown = -0.15f;

    /*
    //gun
    private float rightArmLockPositionUp = 0.15f;
    private float rightArmLockPositionDown = -0.15f;
    */

    //agressor
    private float rightArmLockPositionUp; // = 0.01f;
    private float rightArmLockPositionDown; // = -0.07f;


    //private float walkArmDelta = 1.296f;
    //private float crouchArmDelta = 1.06f;

    private Rigidbody2D playerRb2D;

    //private FixedJoint2D fxJ2D;
    //private bool isFacingLeft;
    private Animator animator;
    private bool crouch;
    private bool run;
    private bool walk;
    private bool isGround;
    private bool crouchButtonDown;
    private bool isRunning;
    private bool isIdle;

    //private GameObject leftArmPointer;
    //private GameObject[] weapons;
    private WeaponController wc;

    //[SerializeField] GameObject target;


    private Transform rightArmWeaponPoint;
    private Transform rightWeaponPoint;

    private int weaponIndex;
    private static readonly int Walk = Animator.StringToHash("walk");


    // Start is called before the first frame update
    void Start()
    {
        crouch = false;
        crouchButtonDown = false;
        run = false;
        walk = false;
        isRunning = false;
        isIdle = true;
        isGround = true;
        playerRb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponIndex = 0;


        InitWeapon();
        wc = currentWeapon.GetComponent<WeaponController>();

        rightArmWeaponPoint = currentWeapon.transform.Find("rightArmPoint");

        if (rightArmWeaponPoint) Debug.Log("rightArmWeaponPoint ok");
        else Debug.Log("rightArmWeaponPoint false");

        rightWeaponPoint = transform.Find("bone_1").Find("bone_2").Find("bone_19").Find("bone_20")
            .Find("boneRightWrist").Find("right");

        rightArmLockPositionUp = wc.Weapon.RightArmLockPositionUp;
        rightArmLockPositionDown = wc.Weapon.RightArmLockPositionDown;


        if (rightWeaponPoint && rightArmWeaponPoint)
        {
            var position = rightWeaponPoint.position;
            rightArm.transform.position += rightArmWeaponPoint.position - position;
        }
        else Debug.Log("rightArmWeaponPoint not found");
    }


    // Update is called once per frame
    void Update()
    {
        ReadDeviceDirections();


        MovingRightArm();

    }

    private void Run()
    {
        if (run && !isRunning && isGround)
        {
            animator.SetBool(Walk, false);
            animator.SetBool("run", true);

            maxVelocity = 5.0f;
        }

        if (!run && !isRunning && !isIdle && isGround)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", true);

            maxVelocity = 2.0f;
        }

        if (!run && !isRunning && !walk && isIdle && isGround)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
        }
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
           
        }
        else if (((int) playerRb2D.velocity.x) != 0)
        {
            walk = true;
            run = false;
            isRunning = false;
            isIdle = false;

        }
        else
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
            wc.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeWeapon();
        }
    }

    private void FixedUpdate()
    {
        KeyboardJump();
        Crouch();
        Move();
        VelocityControl();
        Run();
    }

    public void ChangeWeapon()
    {
        int weaponCount = player.Weapons.Count;
        Destroy(currentWeapon);
        weaponIndex++;
        currentWeapon = Instantiate(player.Weapons[weaponIndex], weaponPoint.transform) as GameObject;
        wc = currentWeapon.GetComponent<WeaponController>();
        currentWeapon.name = player.Weapons[weaponIndex].name;

        rightArmWeaponPoint = currentWeapon.transform.Find("rightArmPoint");

        if (rightArmWeaponPoint) Debug.Log("rightArmWeaponPoint ok");
        else Debug.Log("rightArmWeaponPoint false");

        rightWeaponPoint = transform.Find("bone_1").Find("bone_2").Find("bone_19").Find("bone_20")
            .Find("boneRightWrist").Find("right");

        if (rightWeaponPoint && rightArmWeaponPoint)
        {
            rightArm.transform.position += rightArmWeaponPoint.position - rightWeaponPoint.position;
            Debug.Log("rightArmWeaponPoint " + rightArmWeaponPoint.position + "rightWeaponPoint " +
                      rightWeaponPoint.position);
            Debug.Log("Pos " + (rightArmWeaponPoint.position - rightWeaponPoint.position));
        }
        else Debug.Log("rightArmWeaponPoint not found");

        rightArmLockPositionUp = wc.Weapon.RightArmLockPositionUp;
        rightArmLockPositionDown = wc.Weapon.RightArmLockPositionDown;

        Debug.Log(currentWeapon);
        if (weaponIndex == weaponCount - 1)
        {
            weaponIndex = 0;
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
            animator.SetTrigger("jump");
            playerRb2D.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
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

    public void InitWeapon()
    {
        currentWeapon = Instantiate(player.Weapons[weaponIndex], weaponPoint.transform) as GameObject;
        currentWeapon.name = player.Weapons[weaponIndex].name;
    }


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
        get { return direction; }

        set { direction = value; }
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
        get { return crouchButtonDown; }

        set { crouchButtonDown = value; }
    }

    public GameObject WeaponPoint
    {
        get { return weaponPoint; }

        private set { weaponPoint = value; }
    }

    private void MovingRightArm()
    {
        Vector3 leftArmLocalPosition = leftArm.transform.localPosition;
        Vector3 rightArmLocalPosition = rightArm.transform.localPosition;

        Vector3 currentPositionLeftArm = new Vector3(leftArmLocalPosition.x,
            joysticDirection.y * .5f,
            leftArmLocalPosition.z);


        Vector3 currentPositionRightArm = new Vector3(rightArmLocalPosition.x,
            joysticDirection.y * .5f,
            rightArmLocalPosition.z);

        if (currentPositionLeftArm.y >= leftArmLockPositionDown
            && currentPositionLeftArm.y <= leftArmLockPositionUp)
        {
            leftArm.transform.localPosition = currentPositionLeftArm;
        }


        if (currentPositionRightArm.y >= rightArmLockPositionDown
            && currentPositionRightArm.y <= rightArmLockPositionUp)
        {
           rightArm.transform.localPosition = new Vector3(rightArm.transform.localPosition.x,
                currentPositionRightArm.y,
                rightArm.transform.localPosition
                    .z);
        }

    }
}