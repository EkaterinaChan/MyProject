using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public int slotIndex;
    private Inventory inventory;
    [SerializeField] private float dropDistance = 1.5f; // Дистанция выброса от игрока

    private void Start()
    {
        FindInventory();
    }

    private void FindInventory()
    {
        // Ищем сначала Inventory в сцене (более надежный способ)
        inventory = FindObjectOfType<Inventory>();
        if (inventory != null) return;
        
        // Если не нашли - пытаемся через игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Игрок с тегом 'Player' не найден в сцене!");
            return;
        }

        inventory = player.GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Компонент Inventory не найден ни в сцене, ни на игроке!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (inventory == null)
            {
                Debug.LogWarning("Попытка выбросить предмет, но Inventory не найден!");
                FindInventory();
                return;
            }
            DropItem();
        }
    }

    private void DropItem()
    {
        if (inventory == null)
        {
            Debug.LogWarning("Inventory не назначен!");
            return;
        }

        if (transform.childCount == 0)
        {
            Debug.Log("Слот пустой, нечего удалять");
            return;
        }

        Transform child = transform.GetChild(0);
        if (child == null) return;

        InventoryItem item = child.GetComponent<InventoryItem>();
        if (item == null)
        {
            Debug.LogWarning("Не найден компонент InventoryItem у ребенка");
            Destroy(child.gameObject);
            inventory.isFull[slotIndex] = false;
            return;
        }

    if (item.sourceItem != null)
    {
        // ИНВЕРТИРОВАНО направление: теперь >0 = влево, <0 = вправо
        float direction = inventory.transform.localScale.x > 0 ? -1 : 1;
        
        // Позиция выброса: на 1.5 единицы в направлении взгляда
        Vector2 dropPos = new Vector2(
            inventory.transform.position.x + (dropDistance * direction),
            inventory.transform.position.y - 1
        );
        
        item.sourceItem.Drop(dropPos);
    }
    else
    {
        Debug.LogWarning("SourceItem не назначен в InventoryItem");
    }

        Destroy(child.gameObject);
        inventory.isFull[slotIndex] = false;
    }
}