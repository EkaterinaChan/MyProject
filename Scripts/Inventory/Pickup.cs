using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int id;
    public GameObject inventoryPrefab; // Префаб для слота инвентаря
    private bool isPickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isPickedUp)
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if(inventory != null)
            {
                if(inventory.AddItem(this))
                {
                    isPickedUp = true;
                    gameObject.SetActive(false); // Деактивируем вместо уничтожения
                }
            }
        }
    }

    public void Drop(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        isPickedUp = false;
    }
}