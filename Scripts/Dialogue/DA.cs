using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DA : MonoBehaviour
{
    public Animator startAnim;  // Аниматор, который управляет открытием/закрытием
    public DM dm;              // Менеджер диалогов (если используется)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Проверяем, что вошёл игрок
        {
            if (startAnim != null)    // Проверяем, что аниматор существует
            {
                startAnim.SetBool("StartOpen", true);
            }
            else
            {
                Debug.LogWarning("startAnim не назначен в инспекторе!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Проверяем, что вышел игрок
        {
            if (startAnim != null)    // Проверяем, что аниматор не уничтожен
            {
                startAnim.SetBool("StartOpen", false);
            }
        }
    }
}
