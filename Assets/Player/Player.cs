using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] [Tooltip("Ссылка на SO игрока")]
    private PlayerSettings playerSettings;

    [SerializeField] [Tooltip("Максимальноу ускорение при хотьбе")]
    private float maxWalkVelocity;

    [SerializeField] [Tooltip("Максимальноу ускорение при беге")]
    private float maxRunVelocity;

    [SerializeField] [Tooltip("Сила прыжка")]
    float jumpForce;

    [SerializeField] [Tooltip("Сила используемая для движения игрока")]
    Vector2 force;

    [SerializeField] [Tooltip("Объект используемый для упарвления правой рукой")]
    GameObject rightArm;

    [SerializeField] [Tooltip("Объект используемый для управления левой рукой")]
    GameObject leftArm;

    [SerializeField] [Tooltip("Точка крепления оружия")]
    private GameObject weaponPoint;

    [SerializeField] [Tooltip("Текущее оружие")]
    private GameObject currentWeapon;

    [SerializeField] [Tooltip("Стартовое оружие")]
    private GameObject initialWeapon;

    [SerializeField] [Tooltip("Джойстик для управления игроком")]
    private Joystick viewJoystick;

    [SerializeField] [Tooltip("Мертвая зона джойстика")]
    private float joystickDelay;
    
    [SerializeField] [Tooltip("Коллайдер земли")]
    private Collider2D groundCollider2D;


    [SerializeField] bool keyboardController;
    [SerializeField] bool uiController;

#pragma warning restore 0649
    
    //-----------------------------------------
    public StateMashine StateMashine => GetComponent<StateMashine>();
    private bool movingRight;
    public bool ground;
    private Dictionary<System.Type, BaseState> states;
    //-----------------------------------------

    private Vector2 direction;
    private float maxVelocity;
    private Vector2 joystickDirection;
    private Vector2 keyboardDirection;

    private float prevKeyboardY;

    private float leftArmLockPositionUp = 0.15f;
    private float leftArmLockPositionDown = -0.15f;

    private float rightArmLockPositionUp;
    private float rightArmLockPositionDown;

    public Animator animator;
  
     bool uiRunButton;
    private bool uiCrouchButton;

    private WeaponController wc;

    private Transform rightArmWeaponPoint;
    private Transform rightWeaponPoint;
    private Vector3 Rarm;

    private int weaponIndex;


    private void Awake()
    {
        InitializeStateMashine();
    }
    private void InitializeStateMashine()
    {
        states = new Dictionary<System.Type, BaseState>()
        {
            {typeof(IdleState), new IdleState(this)},
            {typeof(MoveState), new MoveState(this)},
            {typeof(RunState), new RunState(this)},
            {typeof(JumpState), new JumpState(this)},
            {typeof(CrouchState), new CrouchState(this)},
            {typeof(PlayerFlipState), new PlayerFlipState(this)}
        };
        
        StateMashine.SetStates(states);
    }
    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
       animator = GetComponent<Animator>();
        weaponIndex = 0;

        InitWeapon();
        wc = currentWeapon.GetComponent<WeaponController>();

        rightArmWeaponPoint = currentWeapon.transform.Find("rightArmPoint");

        rightWeaponPoint = transform.Find("bone_1").Find("bone_2").Find("bone_19").Find("bone_20")
            .Find("boneRightWrist").Find("right");

        rightArmLockPositionUp = wc.Weapon.RightArmLockPositionUp;
        rightArmLockPositionDown = wc.Weapon.RightArmLockPositionDown;


        if (rightWeaponPoint && rightArmWeaponPoint)
        {
            rightArm.transform.position += rightArmWeaponPoint.position - rightWeaponPoint.position;
        }
        //else Debug.Log("rightArmWeaponPoint not found");
    }

   
    void Update()
    {
        ReadDeviceDirections();
        //ground = GroundCollider2D.GroundCheck("Ground");
        
        MovingRightArm();
    }
    
    private void ReadDeviceDirections()
    {
        //Считываем нажатие клавиш управления на клавиатуре и изменение положения джойстика
        keyboardDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f);
        joystickDirection = new Vector2(viewJoystick.Horizontal, viewJoystick.Vertical);
        //Debug.Log(Input.GetKeyDown(KeyCode.W));
        
        if (Mathf.Abs(joystickDirection.x) > joystickDelay || keyboardDirection.x != 0)
        {
            if (keyboardDirection.x != 0)
            {
                direction.x = keyboardDirection.x;
            }

            if (joystickDirection.x != 0)
            {
                direction.x = joystickDirection.x;
            }
        }
        else
        {
           ResetX();
        }
        
        /*if (keyboardDirection.y != 0)
        {*/
            direction.y = Input.GetKeyDown(KeyCode.W) ? 1 : Input.GetKeyDown(KeyCode.S) ? -1 : 0;
            //direction.y = Input.GetKeyDown(KeyCode.S) ? -1 : 0;

        /*}
       else
        {
            ResetY();
        }*/
        
        
        
        //Считываем нажатие Ctrl для выстрела
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            float shootDelay = wc.Weapon.ShootDelay <= 0 ? 0 : wc.Weapon.ShootDelay;
            if (shootDelay > 0)
                InvokeRepeating("Shoot", shootDelay, shootDelay);
            else Shoot();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            float shootDelay = wc.Weapon.ShootDelay <= 0 ? 0 : wc.Weapon.ShootDelay;
            if (shootDelay > 0)
            {
                CancelInvoke("Shoot");
                wc.Weapon.IsShooting = false;
            }
            else StopShoot();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //GrenadeThrow();
        }

       
    }

    public void Shoot()
    {
        wc.Shoot();
    }

    public void StopShoot()
    {
        wc.StopShoot();
    }

    private void InitWeapon()
    {
        int count = 0;
        if (playerSettings.CurrentWeaponName != "")
        {
            foreach (var weapon in playerSettings.Weapons)
            {
                if (weapon.name == playerSettings.CurrentWeaponName)
                {
                    currentWeapon = Instantiate(weapon, weaponPoint.transform);
                    playerSettings.CurrentWeaponName = weapon.name;
                    weaponIndex = count == playerSettings.Weapons.Count - 1 ? 0 : count + 1;
                    //Debug.Log($"weaponIndex {playerSettings.Weapons.Count} count {count}");
                }
                else
                {
                    //Debug.Log("Error weapon Init");
                }

                count++;
            }
        }
        else
        {
            currentWeapon = Instantiate(playerSettings.Weapons[weaponIndex], weaponPoint.transform);
            playerSettings.CurrentWeaponName = playerSettings.Weapons[weaponIndex].name;
            weaponIndex++;
        }
    }

    public void ChangeWeapon()
    {
        Debug.Log(wc.Weapon.IsShooting);
        if (!wc.Weapon.IsShooting)
        {
            int weaponCount = playerSettings.Weapons.Count;
            if (currentWeapon) Destroy(currentWeapon);

            currentWeapon = Instantiate(playerSettings.Weapons[weaponIndex], weaponPoint.transform);
            if (currentWeapon)
            {
                wc = currentWeapon.GetComponent<WeaponController>();
                currentWeapon.name = playerSettings.Weapons[weaponIndex].name;
                playerSettings.CurrentWeaponName = currentWeapon.name;
                rightArmWeaponPoint = currentWeapon.transform.Find("rightArmPoint");

                /*if (rightArmWeaponPoint) Debug.Log("rightArmWeaponPoint ok");
                else Debug.Log("rightArmWeaponPoint false");*/

                rightWeaponPoint = transform.Find("bone_1").Find("bone_2").Find("bone_19").Find("bone_20")
                    .Find("boneRightWrist").Find("right");

                if (rightWeaponPoint && rightArmWeaponPoint)
                {
                    rightArm.transform.position += rightArmWeaponPoint.position - rightWeaponPoint.position;
                    //Debug.Log("rightArmWeaponPoint " + rightArmWeaponPoint.position + "rightWeaponPoint " +
                    //          rightWeaponPoint.position);
                    //Debug.Log("Pos " + (rightArmWeaponPoint.position - rightWeaponPoint.position));
                }
                //else Debug.Log("rightArmWeaponPoint not found");

                rightArmLockPositionUp = wc.Weapon.RightArmLockPositionUp;
                rightArmLockPositionDown = wc.Weapon.RightArmLockPositionDown;

                //Debug.Log($"{currentWeapon.name} index {weaponIndex}");
                if (weaponIndex == weaponCount - 1)
                {
                    weaponIndex = -1;
                }

                weaponIndex++;
            }
            else
            {
                //Debug.Log("Weapon not change, weapon not found");
            }
        }
    }

   
    public Collider2D GroundCollider2D
    {
        get => groundCollider2D;
        set => groundCollider2D = value;
    }

    public Vector2 Force
    {
        get => force;
        set => force = value;
    }

    public Vector2 Direction
    {
        get => direction;
        set => direction = value;
    }

    private void ResetX()
    {
        direction.x = 0;
    }

    private void ResetY()
    {
        direction.y = prevKeyboardY = 0;
    }

    public float MaxWalkVelocity
    {
        get => maxWalkVelocity;
        set => maxWalkVelocity = value;
    }

    public float MaxRunVelocity
    {
        get => maxRunVelocity;
        set => maxRunVelocity = value;
    }

    public bool UICrouchButton
    {
        get => uiCrouchButton;
        set => uiCrouchButton = value;
    }

    public GameObject WeaponPoint
    {
        get => weaponPoint;
    }

    public bool UIRunButton
    {
        get => uiRunButton;
        set => uiRunButton = value;
    }

    public WeaponController getWC
    {
        get => wc;
    }

    public float JumpForce => jumpForce;

    public bool MovingRight
    {
        get => movingRight;
        set => movingRight = value;
    }

    private void MovingRightArm()
    {
        Vector3 leftArmLocalPosition = leftArm.transform.localPosition;
        Vector3 rightArmLocalPosition = rightArm.transform.localPosition;

        Vector3 currentPositionLeftArm = new Vector3(leftArmLocalPosition.x,
            joystickDirection.y * .5f,
            leftArmLocalPosition.z);


        Vector3 currentPositionRightArm = new Vector3(rightArmLocalPosition.x,
            joystickDirection.y * .5f,
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
                currentPositionRightArm.y, rightArm.transform.localPosition.z);
        }
    }
}