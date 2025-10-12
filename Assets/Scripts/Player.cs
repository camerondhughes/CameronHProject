using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 6f;
    [SerializeField] float jumpPower = 1f;

    [SerializeField] Rigidbody2D rb;
    bool jumping = false;
    bool touchingGround;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("walking", movementX != 0f);
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;

        Debug.Log(v);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
