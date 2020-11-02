using UnityEngine;

public class PController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField][Tooltip("Ссылка на SO игрока")] 
    private Player player;
    
    [SerializeField][Tooltip("Максимальноу ускорение при хотьбе")] 
    private float maxVelocity;
    
    [SerializeField][Tooltip("Сила прыжка")] 
    float jumpForce;
    
    [SerializeField][Tooltip("Сила используемая для движения игрока")] 
    Vector2 force;
    
    [SerializeField][Tooltip("Объект используемый для упарвления правой рукой")]
    GameObject rightArm;
    
    [SerializeField][Tooltip("Объект используемый для управления левой рукой")] 
    GameObject leftArm;

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

    private Vector2 direction;
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
    private bool run;
    private bool walk;
    private bool isGround;
    private bool crouchButtonDown;
    private bool isRunning;
    private bool isIdle;

    private WeaponController wc;

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
        joystickDirection = new Vector2(viewJoystick.Horizontal, viewJoystick.Vertical);

        if (joystickDirection.x != 0 || keyboardDirection.x != 0)
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
    public void InitWeapon()
    {
        int count = 0;
        if (player.CurrentWeaponName != "")
        {
            foreach (var weapon in player.Weapons)
            {
                count++;
                if (weapon.name == player.CurrentWeaponName)
                {
                    currentWeapon = Instantiate(weapon, weaponPoint.transform) as GameObject;
                    player.CurrentWeaponName = weapon.name;
                    weaponIndex = count;
                }
                else
                {
                    Debug.Log("Error weapon Init");
                }
            }
        }
        else
        {
            currentWeapon = Instantiate(player.Weapons[weaponIndex], weaponPoint.transform) as GameObject;
            player.CurrentWeaponName = player.Weapons[weaponIndex].name;
            weaponIndex++;
        }
      
    }
    public void ChangeWeapon()
    {
        int weaponCount = player.Weapons.Count;
        if (currentWeapon) Destroy(currentWeapon);
        
        currentWeapon = Instantiate(player.Weapons[weaponIndex], weaponPoint.transform) as GameObject;
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
            weaponIndex++;
            Debug.Log(currentWeapon);
            if (weaponIndex == weaponCount - 1)
            {
                weaponIndex = 0;
            }
        }
        else
        {
            Debug.Log("Weapon not change, weapon not found");
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
        }
        else
        {
            animator.SetBool("crouch", false);
            crouch = false;
        }
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
        get => isGround;
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


    public bool CrouchButtonDown
    {
        get => crouchButtonDown;
        set => crouchButtonDown = value;
    }

    public GameObject WeaponPoint
    {
        get => weaponPoint;
        private set => weaponPoint = value; 
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