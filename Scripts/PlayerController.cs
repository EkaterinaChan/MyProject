using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float moveInput;

    private Rigidbody2D rb;

    private bool facingLeft = true;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Animator anim;
    public VectorValue pos;

    private void Start()
    {
        transform.position = pos.initialValue;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        
        if(moveInput > 0) // Движение вправо
        {
            if(facingLeft) Flip(); // Если смотрит влево - повернуть
        }
        else if(moveInput < 0) // Движение влево
        {
            if(!facingLeft) Flip(); // Если смотрит вправо - повернуть
        }
        if(moveInput == 0) 
        {
            anim.SetBool("isRunning", false);
        }
        else 
        {
            anim.SetBool("isRunning", true);
        }
    }

    void Flip() 
    {
        facingLeft = !facingLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOf");
        }
        if(isGrounded == true) 
        {
            anim.SetBool("isJumping", false);  
        }
        else 
        {
            anim.SetBool("isJumping", true);
        }
    }
}
