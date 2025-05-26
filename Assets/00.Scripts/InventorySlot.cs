using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Item item; 
    public InventoryUI inventoryUI; 

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null && inventoryUI != null)
        {
            inventoryUI.ShowItemDetail(item);
        }
    }
}
