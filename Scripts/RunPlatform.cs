using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;              // Скорость движения
    public Vector2[] waypoints;           // Массив точек пути
    public bool cyclic = true;            // Зациклить движение
    public float waitTime = 1f;           // Время ожидания на точке

    [Header("Gizmos")]
    public Color pathColor = Color.cyan;   // Цвет отображения пути

    private int currentWaypointIndex = 0;
    private float waitCounter;
    private bool isWaiting = false;

    private void Start()
    {
        // Начинаем с первой точки (если точки заданы)
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0];
        }
    }

    private void Update()
    {
        if (waypoints.Length < 2) return; // Нужно минимум 2 точки

        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0f)
            {
                isWaiting = false;
            }
            return;
        }

        // Движение к текущей точке
        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypointIndex],
            speed * Time.deltaTime
        );

        // Проверка достижения точки
        if ((Vector2)transform.position == waypoints[currentWaypointIndex])
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (!cyclic && currentWaypointIndex >= waypoints.Length - 1)
        {
            // Достигли конца нециклического пути
            return;
        }

        isWaiting = true;
        waitCounter = waitTime;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    // Прикрепляем игрока при столкновении
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    // Открепляем игрока
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    // Визуализация пути в редакторе
    private void OnDrawGizmosSelected()
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Gizmos.color = pathColor;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
        }

        if (cyclic)
        {
            Gizmos.DrawLine(waypoints[waypoints.Length - 1], waypoints[0]);
        }
    }
}