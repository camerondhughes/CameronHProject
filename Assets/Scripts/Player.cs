using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class Player : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 6f;
    [SerializeField] float jumpPower = 1f;

    [SerializeField] Rigidbody2D rb;
    bool jumping = false;
    bool touchingGround;
    //bool facingRight = true;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private int coinScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("walking", movementX != 0f);

        if (rb.linearVelocityX != 0)
        {
            spriteRenderer.flipX = (rb.linearVelocityX < 0);
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;

        Debug.Log(v);

        //facingRight = v.x > 0;
        //if (spriteRenderer.flipX == facingRight)
        //{
        //    spriteRenderer.flipX = !facingRight;
        //}
    }

    void FixedUpdate()
    {
        float XmoveDistance = movementX * speed;
        //float YmoveDistance = movementY * speed * Time.fixedDeltaTime;\
        //transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y + YmoveDistance);

        rb.linearVelocityX = XmoveDistance;

        if (touchingGround && jumping)
        {
            rb.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
            jumping = false;
        }
    }
    void OnJump()
    {
        if(touchingGround)
        {
            jumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("Ground"))
       {
            touchingGround = true;
       }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = false;
        }
    }

    public void AddCoin(int value)
    {
        coinScore += value;
    }
}
