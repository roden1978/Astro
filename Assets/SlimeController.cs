using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float moveSpeed;
    private Transform player;
    private Vector3 playerPos;
    private Vector2 attackPos;
    private Animator animator;
    private bool isCollision;
    private bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        isCollision = false;
        animator = GetComponent<Animator>();
        player = null;  

        // isGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        
       // print(Ground);
    }

    private void FixedUpdate()
    {
        if (isCollision && isGround)
        {
            rb.AddForce(playerPos * speed, ForceMode2D.Impulse);
            //rb.AddForceAtPosition(playerPos, transform.position, ForceMode2D.Impulse);
            print("Attack " + playerPos * speed);
        }

        if (isCollision)
            Debug.DrawRay(transform.position, player.position - transform.position, Color.red);

        rb.AddForce(new Vector2(moveSpeed, 0));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print(collision.name);
            isCollision = true;
            if (!player)
            {
                player = GameObject.Find("HeadPoint").GetComponent<Transform>();

                //playerPos = transform.position - player.transform.position;
                //attackPos = new Vector2(playerPos.x, playerPos.y);
            }
            else
            {
                playerPos = player.position - transform.position;
            }

            
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollision = false;
        //isGround = false;
    }

    public bool Ground
    {
        get
        {
            return isGround;
        }

        set
        {
            isGround = value;
        }
    }
}
