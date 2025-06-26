using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;            // Скорость движения
    public float leftBound = -5f;       // Левая граница движения
    public float rightBound = 5f;       // Правая граница движения
    public float startDelay = 0f;       // Задержка перед стартом

    [Header("Player Settings")]
    public bool carryPlayer = true;     // Переносить ли игрока
    public float playerCheckOffset = 0.5f; // Смещение для проверки игрока

    private bool movingRight = true;
    private float delayTimer;
    private Transform playerTransform;

    private void Start()
    {
        delayTimer = startDelay;
    }

    private void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            return;
        }

        MovePlatform();
    }

    private void MovePlatform()
    {
        // Определяем направление движения
        float direction = movingRight ? 1 : -1;
        float newX = transform.position.x + direction * speed * Time.deltaTime;

        // Проверяем границы
        if (movingRight && newX >= rightBound)
        {
            newX = rightBound;
            movingRight = false;
        }
        else if (!movingRight && newX <= leftBound)
        {
            newX = leftBound;
            movingRight = true;
        }

        // Применяем движение
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (carryPlayer && collision.gameObject.CompareTag("Player"))
        {
            // Проверяем, что игрок действительно стоит на платформе
            if (IsPlayerOnTop(collision))
            {
                playerTransform = collision.transform;
                playerTransform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (carryPlayer && collision.gameObject.CompareTag("Player"))
        {
            if (playerTransform != null)
            {
                playerTransform.SetParent(null);
                playerTransform = null;
            }
        }
    }

    private bool IsPlayerOnTop(Collision2D collision)
    {
        // Проверяем, что игрок касается верхней части платформы
        float platformTop = transform.position.y + GetComponent<Collider2D>().bounds.extents.y;
        float playerBottom = collision.collider.bounds.min.y;
        
        return Mathf.Abs(playerBottom - platformTop) < 0.1f;
    }

    private void OnDrawGizmosSelected()
    {
        // Визуализация границ движения
        Gizmos.color = Color.cyan;
        Vector3 leftPos = new Vector3(leftBound, transform.position.y, transform.position.z);
        Vector3 rightPos = new Vector3(rightBound, transform.position.y, transform.position.z);
        
        Gizmos.DrawLine(leftPos, rightPos);
        Gizmos.DrawSphere(leftPos, 0.1f);
        Gizmos.DrawSphere(rightPos, 0.1f);
    }
}