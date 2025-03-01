using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot_move : MonoBehaviour
{
    float speedX;
    float speedY;
    public float speed;
    Rigidbody2D rb;
    private bool isFacingRight = true;
    public float jump;

    public Transform inGround;
    public LayerMask groundLayer;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(inGround.position, new Vector2(2.5f, 1.0f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        speedX = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButton("Jump") && isGrounded)
        {
            //Debug.Log("Jump button pressed and grounded!");
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump), ForceMode2D.Impulse);
        }

        Flip();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speedX, rb.linearVelocity.y); // Preserve Y velocity for gravity
    }

    private void Flip()
    {
        if ((isFacingRight && speedX < -0.1f) || (!isFacingRight && speedX > 0.1f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    

}