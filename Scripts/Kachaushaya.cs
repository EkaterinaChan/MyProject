using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeesawTrap : MonoBehaviour 
{
    private Rigidbody2D rb;
    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePosition; // Фиксируем позицию, оставляя вращение
    }

    void OnCollisionStay2D(Collision2D col) 
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        // Получаем точку контакта относительно центра платформы
        float contactPoint = col.contacts[0].point.x - transform.position.x;
        
        // Применяем вращательный импульс
        rb.AddTorque(-contactPoint * 0.5f, ForceMode2D.Impulse);
    }
}