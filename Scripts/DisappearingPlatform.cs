using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] private float disappearDelay = 0.5f; // Через сколько секунд исчезнет
    [SerializeField] private float reappearDelay = 1f;  // Через сколько секунд reappear (если нужно)
    [SerializeField] private bool reappearAfterDelay = false; // Нужно ли появляться снова?

    private Collider2D platformCollider;
    private SpriteRenderer spriteRenderer;
    private bool isPlayerOnPlatform = false;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            Invoke("Disappear", disappearDelay);
        }
    }

    private void Disappear()
    {
        if (!isPlayerOnPlatform) return;
        
        platformCollider.enabled = false;
        spriteRenderer.enabled = false;

        if (reappearAfterDelay)
        {
            Invoke("Reappear", reappearDelay);
        }
        else
        {
            Destroy(gameObject); // Полное удаление, если не нужно появляться снова
        }
    }

    private void Reappear()
    {
        platformCollider.enabled = true;
        spriteRenderer.enabled = true;
        isPlayerOnPlatform = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }
}