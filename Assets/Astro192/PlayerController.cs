using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 force;
    private Vector2 direction;
    public float maxVelocity;
    private Rigidbody2D playerRb2D;
    private bool isFacingLeft;
    private Animator animator;
    private bool crouch;
    // Start is called before the first frame update
    void Start()
    {
        force = new Vector2(50.0f, 1.0f);
        maxVelocity = 2.0f;
        isFacingLeft = false;
        crouch = false;
        playerRb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (direction.x > 0 && isFacingLeft)
            //отражаем персонажа вправо
            Flip();
        //обратная ситуация. отражаем персонажа влево
        else if (direction.x < 0 && !isFacingLeft)
            Flip();

        VelocityControl();
    }

    private void FixedUpdate()
    {
        
        Move();
        Jump();
        Crouch();
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
            WalkOneHandGun(true);
            playerRb2D.AddForce(new Vector2(force.x * direction.x, 0.0f), ForceMode2D.Force);
        }
        else
        {
            WalkOneHandGun(false);
        }
        
    }

    private void Jump()
    {
        if (direction.y > 0)
        {
            playerRb2D.AddForce(new Vector2(0.0f, force.y * direction.y), ForceMode2D.Impulse);
        }
    }

    private void Crouch()
    {
        if(direction.y < 0)
        {
            animator.SetBool("crouchOneHandGun", true);
            crouch = true;
        }
        else
        {
            animator.SetBool("crouchOneHandGun", false);
            crouch = false;
        }
    }

    private void WalkOneHandGun(bool walk)
    {
        animator.SetBool("walkOneHandGun", walk);
    }

    private void VelocityControl()
    {
        if (direction.x > 0 && playerRb2D.velocity.x > maxVelocity)
        {
            playerRb2D.velocity = new Vector2(maxVelocity, playerRb2D.velocity.y);
        }
        else
        {
            if(direction.x < 0 && playerRb2D.velocity.x < -maxVelocity)
            {
                playerRb2D.velocity = new Vector2(-maxVelocity, playerRb2D.velocity.y);
            }
        }
    }
}
