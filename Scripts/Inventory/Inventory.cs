using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; } // Перенесено внутрь класса
    
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject inventory;
    private bool inventoryOn;

    private void Awake() // Перенесено внутрь класса
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryOn = false;
        inventory.SetActive(false);
    }

    public bool AddItem(Pickup item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(!isFull[i])
            {
                GameObject slotItem = Instantiate(item.inventoryPrefab, slots[i].transform);
                slotItem.GetComponent<InventoryItem>().sourceItem = item;
                isFull[i] = true;
                return true;
            }
        }
        return false;
    }

    public void Chest()
    {
        inventoryOn = !inventoryOn;
        inventory.SetActive(inventoryOn);
    }
}