using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController2D : MonoBehaviour
{

    float moveInput;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public float speed;
    public float jumpForce;
    public float heightCutter;
    public float groundHeight;
    bool grounded;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        grounded = isGrounded();
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * heightCutter);
            }
        }
    }

    bool isGrounded()
    {
        return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, groundHeight, groundLayer);
    }
}
