using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityScale = 3f; // Теперь используется

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isClimbing;
    private bool isGrounded;
    private float originalGravity;
    private Vector2 movementInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        rb.gravityScale = gravityScale; // Применяем настройку гравитации
    }

    void Update()
    {
        GetInput();
        CheckGround();
        HandleJump();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void GetInput()
    {
        movementInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void MoveCharacter()
    {
        if (isClimbing)
        {
            // Движение по лестнице
            rb.gravityScale = 0;
            rb.velocity = new Vector2(
                movementInput.x * walkSpeed * 0.5f,
                movementInput.y * climbSpeed
            );
        }
        else
        {
            // Обычное движение
            rb.gravityScale = gravityScale; // Используем нашу переменную
            rb.velocity = new Vector2(
                movementInput.x * walkSpeed,
                rb.velocity.y
            );
        }
    }

    public void SetClimbing(bool climbing)
    {
        isClimbing = climbing;
        
        if (!climbing)
        {
            // Восстанавливаем нашу гравитацию
            rb.gravityScale = gravityScale;
        }
    }
}