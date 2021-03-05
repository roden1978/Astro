﻿using System.Collections.Generic;
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
    private float jumpForce;

    [SerializeField] [Tooltip("Сила используемая для движения игрока")]
    private Vector2 force;

    [SerializeField] [Tooltip("Объект используемый для упарвления правой рукой")]
    private GameObject rightArm;

    [SerializeField] [Tooltip("Объект используемый для управления левой рукой")]
    private GameObject leftArm;

    [SerializeField] [Tooltip("Точка крепления оружия")]
    private GameObject weaponPoint;

    [SerializeField] [Tooltip("Текущее оружие")]
    private GameObject currentWeapon;

    [SerializeField] [Tooltip("Стартовое оружие")]
    private GameObject initialWeapon;

    [SerializeField] [Tooltip("Мертвая зона джойстика")]
    private float joystickDelay;

    [SerializeField] [Tooltip("Коллайдер земли")]
    private Collider2D groundCollider2D;

#pragma warning restore 0649

    private StateMashine StateMashine => GetComponent<StateMashine>();
    private bool run;
    private Dictionary<System.Type, BaseState> states;
    private Joystick joystick;
    private Vector2 direction;
    private float maxVelocity;
    private Vector2 joystickDirection;

    private const float LEFT_ARM_LOCK_POSITION_UP = 0.15f;
    private const float LEFT_ARM_LOCK_POSITION_DOWN = -0.15f;

    private float rightArmLockPositionUp;
    private float rightArmLockPositionDown;

    public Animator animator;

    private Weapon weapon;

    private Transform rightArmWeaponPoint;
    private Transform rightWeaponPoint;

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
        MovingRight = true;
        animator = GetComponent<Animator>();
        weaponIndex = 0;
        joystick = FindObjectOfType<Joystick>();
        InitWeapon();
        weapon = playerSettings.CurrentWeapon.GetComponent<Weapon>();

        rightArmWeaponPoint = currentWeapon.transform.Find("rightArmPoint");

        rightWeaponPoint = transform.Find("bone_1").Find("bone_2").Find("bone_19").Find("bone_20")
            .Find("boneRightWrist").Find("right");

        rightArmLockPositionUp = weapon.WeaponSettings.RightArmLockPositionUp;
        rightArmLockPositionDown = weapon.WeaponSettings.RightArmLockPositionDown;


        if (rightWeaponPoint && rightArmWeaponPoint)
        {
            rightArm.transform.position += rightArmWeaponPoint.position - rightWeaponPoint.position;
        }
    }


    void Update()
    {
        KeyboardReadDirections();
        JoystickReadDirections();
        MovingRightArm();
    }

    private void KeyboardReadDirections()
    {

        direction = new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetKeyDown(KeyCode.W) || UIJumpButton ? 1 
                : Input.GetKeyDown(KeyCode.S) ? -1 : 0);

        run = !UIRunButton && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            float shootDelay = weapon.WeaponSettings.ShootDelay <= 0 ? 0 : weapon.WeaponSettings.ShootDelay;
            if (shootDelay > 0)
                InvokeRepeating("Shoot", shootDelay, shootDelay);
            else Shoot();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            float shootDelay = weapon.WeaponSettings.ShootDelay <= 0 ? 0 : weapon.WeaponSettings.ShootDelay;
            if (shootDelay > 0)
            {
                CancelInvoke("Shoot");
                weapon.WeaponSettings.IsShooting = false;
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

    private void JoystickReadDirections()
    {
        joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (Mathf.Abs(joystickDirection.x) > joystickDelay)
        {
            direction.x = joystickDirection.x;
        }
    }

    public void Shoot() => weapon.Shoot();
    
    public void StopShoot() => weapon.StopShoot();
    
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
                }

                count++;
            }
        }
        else
        {
            currentWeapon = Instantiate(playerSettings.Weapons[weaponIndex], weaponPoint.transform);
            //playerSettings.CurrentWeapon = currentWeapon;
            playerSettings.CurrentWeaponName = playerSettings.Weapons[weaponIndex].name;//playerSettings.Weapons[weaponIndex].name;
            weaponIndex++;
        }
    }

    public void ChangeWeapon()
    {
        if (!weapon.WeaponSettings.IsShooting)
        {
            int weaponCount = playerSettings.Weapons.Count;
            if (currentWeapon) Destroy(currentWeapon);

            currentWeapon = Instantiate(playerSettings.Weapons[weaponIndex], weaponPoint.transform);
            if (currentWeapon)
            {
                playerSettings.CurrentWeaponName = playerSettings.Weapons[weaponIndex].name;
                //playerSettings.CurrentWeapon = currentWeapon;
                rightArmWeaponPoint = currentWeapon.transform.Find("rightArmPoint");

                rightWeaponPoint = transform.Find("bone_1").Find("bone_2").Find("bone_19").Find("bone_20")
                    .Find("boneRightWrist").Find("right");

                if (rightWeaponPoint && rightArmWeaponPoint)
                {
                    rightArm.transform.position += rightArmWeaponPoint.position - rightWeaponPoint.position;
                   
                }
                rightArmLockPositionUp = weapon.WeaponSettings.RightArmLockPositionUp;
                rightArmLockPositionDown = weapon.WeaponSettings.RightArmLockPositionDown;

                if (weaponIndex == weaponCount - 1)
                {
                    weaponIndex = -1;
                }

                weaponIndex++;
            }
            else
            {
                Debug.Log("Weapon not change, weapon not found");
            }
        }
    }
    private void MovingRightArm()
    {
        Vector3 leftArmLocalPosition = leftArm.transform.localPosition;
        Vector3 rightArmLocalPosition = rightArm.transform.localPosition;

        Vector3 currentPositionLeftArm = new Vector3(leftArmLocalPosition.x,
            joystickDirection.y * .5f, leftArmLocalPosition.z);

        Vector3 currentPositionRightArm = new Vector3(rightArmLocalPosition.x,
            joystickDirection.y * .5f, rightArmLocalPosition.z);

        if (currentPositionLeftArm.y >= LEFT_ARM_LOCK_POSITION_DOWN
            && currentPositionLeftArm.y <= LEFT_ARM_LOCK_POSITION_UP)
        {
            leftArm.transform.localPosition = currentPositionLeftArm;
        }


        if (currentPositionRightArm.y >= rightArmLockPositionDown
            && currentPositionRightArm.y <= rightArmLockPositionUp)
        {
            var localPosition = rightArm.transform.localPosition;
            localPosition = new Vector3(localPosition.x, currentPositionRightArm.y, localPosition.z);
            rightArm.transform.localPosition = localPosition;
        }
    }
   
    public Collider2D GroundCollider2D => groundCollider2D;
    public Vector2 Force => force;
    public Vector2 Direction => direction;
    public float MaxWalkVelocity => maxWalkVelocity;
    public float MaxRunVelocity => maxRunVelocity;
    public bool UICrouchButton { get; set; }
    public bool UIRunButton { get; set; }
    public bool UIJumpButton { get; set; }
    public bool Run => run;
    public Weapon Weapon => weapon;
    public float JumpForce => jumpForce;
    public bool MovingRight { get; set; }
}