using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;          // Скорость движения платформы
    public float upperHeight = 5f;       // Верхняя точка
    public float lowerHeight = 0f;       // Нижняя точка (стартовая позиция)
    
    [Header("Player Detection")]
    public LayerMask playerLayer;        // Слой игрока
    public Transform detectionPoint;     // Точка обнаружения игрока
    public float detectionRadius = 0.5f; // Радиус обнаружения

    private Vector3 initialPosition;
    private bool isPlayerOnPlatform = false;
    private bool isMoving = false;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        lowerHeight = initialPosition.y;
        targetPosition = initialPosition;
    }

    private void Update()
    {
        CheckForPlayer();
        MovePlatform();
    }

    // Проверка наличия игрока на платформе
    private void CheckForPlayer()
    {
        bool wasPlayerOnPlatform = isPlayerOnPlatform;
        isPlayerOnPlatform = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, playerLayer);

        // Если статус изменился
        if (isPlayerOnPlatform != wasPlayerOnPlatform)
        {
            targetPosition = isPlayerOnPlatform ? 
                new Vector3(initialPosition.x, upperHeight, initialPosition.z) : 
                initialPosition;
            isMoving = true;
        }
    }

    // Плавное движение платформы
    private void MovePlatform()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            // Остановка когда достигли цели
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    // Прикрепляем игрока к платформе при нахождении на ней
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            collision.transform.SetParent(transform);
        }
    }

    // Открепляем игрока при покидании платформы
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            collision.transform.SetParent(null);
        }
    }

    // Визуализация зоны обнаружения в редакторе
    private void OnDrawGizmosSelected()
    {
        if (detectionPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(detectionPoint.position, detectionRadius);
        }
    }
}