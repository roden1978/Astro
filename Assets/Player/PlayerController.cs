using UnityEngine;

public class PlayerController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] [Tooltip("Ссылка на SO игрока")]
    private Player player;

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

    [SerializeField] bool keyboardController;
    [SerializeField] bool uiController;

#pragma warning restore 0649

    private Vector2 direction;
    private float maxVelocity;
    private Vector2 joystickDirection;
    private Vector2 keyboardDirection;

    private float prevKeyboardY;

    private float leftArmLockPositionUp = 0.15f;
    private float leftArmLockPositionDown = -0.15f;

    private float rightArmLockPositionUp;
    private float rightArmLockPositionDown;

    private Rigidbody2D playerRb2D;

    private Animator animator;
    private bool crouch;
    private bool isCrouchAnimation;
    private bool run;
    private bool isRunningAnimation;
    private bool walk;
    private bool isWalkingAnimation;
    private bool isGround;
    private bool uiRunButton;
    private bool uiCrouchButton;

    private WeaponController wc;

    private Transform rightArmWeaponPoint;
    private Transform rightWeaponPoint;
    private Vector3 Rarm;

    private int weaponIndex;


    // Start is called before the first frame update
    void Start()
    {
        crouch = false;
        isCrouchAnimation = false;
        uiCrouchButton = false;
        run = false;
        walk = false;
        isWalkingAnimation = false;
        isGround = true;
        playerRb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        weaponIndex = 0;
        uiRunButton = false;

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
            rightArm.transform.position += rightArmWeaponPoint.position - rightWeaponPoint.position;
        }
        else Debug.Log("rightArmWeaponPoint not found");
    }


    // Update is called once per frame
    void Update()
    {
        ReadDeviceDirections();
        VelocityControl();
        MovingRightArm();
        Walk();
        Run();
    }

    private void ReadDeviceDirections()
    {
        //Считываем нажатие клавиш управления на клавиатуре и изменение положения джойстика
        keyboardDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        joystickDirection = new Vector2(viewJoystick.Horizontal, viewJoystick.Vertical);

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

            if (!walk && !run)
            {
                walk = true;
                Debug.Log($"walk = {walk}");
            }
        }
        else
        {
            if (walk)
            {
                walk = false;

                Debug.Log($"walk = {walk}");
            }

            if (run)
            {
                run = false;
                Debug.Log($"run = {run}");
            }

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
                uiCrouchButton = !uiCrouchButton;
                crouch = true;
            }
        }
        else
        {
            ResetY();
        }

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
            if (shootDelay > 0) CancelInvoke("Shoot");
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

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || uiRunButton) && direction.x != 0)
        {
            if (!run)
            {
                run = true;
                walk = false;
                Debug.Log($"run = {run}");
            }
        }
        else if (run) run = false;
    }

    private void Walk()
    {
        if (walk)
        {
            if (!isWalkingAnimation)
            {
                maxVelocity = maxWalkVelocity;
                animator.SetBool("walk", true);
                isWalkingAnimation = true;
                Debug.Log($"isWalkingAnimation = {isWalkingAnimation}");
            }
        }
        else
        {
            if (isWalkingAnimation)
            {
                animator.SetBool("walk", false);
                isWalkingAnimation = false;
                Debug.Log($"isWalkingAnimation = {isWalkingAnimation}");
            }
        }
    }

    private void Run()
    {
        if (run)
        {
            if (!isRunningAnimation)
            {
                maxVelocity = maxRunVelocity;
                animator.SetBool("run", true);
                isRunningAnimation = true;
                Debug.Log($"isRunningAnimation = {isRunningAnimation}");
            }
        }
        else
        {
            if (isRunningAnimation)
            {
                animator.SetBool("run", false);
                isRunningAnimation = false;
                Debug.Log($"isRunningAnimation = {isRunningAnimation}");
            }
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

    private void FixedUpdate()
    {
        KeyboardJump();
        Crouch();
        Move();
    }

    private void InitWeapon()
    {
        int count = 0;
        if (player.CurrentWeaponName != "")
        {
            foreach (var weapon in player.Weapons)
            {
                if (weapon.name == player.CurrentWeaponName)
                {
                    currentWeapon = Instantiate(weapon, weaponPoint.transform);
                    player.CurrentWeaponName = weapon.name;
                    weaponIndex = count == player.Weapons.Count - 1 ? 0 : count + 1;
                    Debug.Log($"weaponIndex {player.Weapons.Count} count {count}");
                }
                else
                {
                    Debug.Log("Error weapon Init");
                }

                count++;
            }
        }
        else
        {
            currentWeapon = Instantiate(player.Weapons[weaponIndex], weaponPoint.transform);
            player.CurrentWeaponName = player.Weapons[weaponIndex].name;
            weaponIndex++;
        }
    }

    public void ChangeWeapon()
    {
        Debug.Log(wc.Weapon.IsFire);
        if (!wc.Weapon.IsFire)
        {
            int weaponCount = player.Weapons.Count;
            if (currentWeapon) Destroy(currentWeapon);

            currentWeapon = Instantiate(player.Weapons[weaponIndex], weaponPoint.transform);
            if (currentWeapon)
            {
                wc = currentWeapon.GetComponent<WeaponController>();
                currentWeapon.name = player.Weapons[weaponIndex].name;
                player.CurrentWeaponName = currentWeapon.name;
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

                Debug.Log($"{currentWeapon.name} index {weaponIndex}");
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

    private void Move()
    {
        if (direction.x != 0 && !crouch)
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
        if (isGround && !uiCrouchButton)
        {
            animator.SetTrigger("jump");
            playerRb2D.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
        }
        else
        {
            uiCrouchButton = false;
            crouch = false;
        }
    }

    public void GrenadeThrow()
    {
        rightArm.transform.localPosition = new Vector3(0,
            rightArm.transform.localPosition.y, rightArm.transform.localPosition.z);
        animator.SetTrigger("throw");
    }

    public void RestoreArmPosition()
    {
        //rightArm.transform.localPosition = new Vector3(Rarm.x, leftArm.transform.position.y, Rarm.z);
        Debug.Log($"LarmY {leftArm.transform.position.y}");
    }

    public void Crouch()
    {
        if (uiCrouchButton)
        {
            if (!isCrouchAnimation)
            {
                animator.SetBool("crouch", true);
                crouch = true;
                isCrouchAnimation = true;
            }
        }
        else
        {
            if (isCrouchAnimation)
            {
                animator.SetBool("crouch", false);
                crouch = false;
                isCrouchAnimation = false;
            }
        }
    }

    private void VelocityControl()
    {
        if (direction.x > 0 && playerRb2D.velocity.x > maxVelocity)
        {
            playerRb2D.velocity = new Vector2(maxVelocity, playerRb2D.velocity.y);
        }

        if (direction.x < 0 && playerRb2D.velocity.x < -maxVelocity)
        {
            playerRb2D.velocity = new Vector2(-maxVelocity, playerRb2D.velocity.y);
        }

        if (direction.x == 0) playerRb2D.velocity = new Vector2(0, playerRb2D.velocity.y);

        if (Mathf.Abs(playerRb2D.velocity.y) >= 12)
        {
            Debug.Log("Player is dead");
        }
        
        if (crouch) playerRb2D.velocity = new Vector2(0, playerRb2D.velocity.y);
    }


    public bool Ground
    {
        set => isGround = value;
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