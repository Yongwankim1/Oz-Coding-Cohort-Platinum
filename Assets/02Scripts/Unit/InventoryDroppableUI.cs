using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    InventorySlotUI inventorySlotUI;
    Image myImage;

    private void Awake()
    {
        Initialize();
    }
    void Initialize()
    {
        inventorySlotUI = GetComponent<InventorySlotUI>();
        myImage = GetComponent<Image>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragAndDropManager.Instance.dropSlot.x = inventorySlotUI.Row;
        DragAndDropManager.Instance.dropSlot.y = inventorySlotUI.Col;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DragAndDropManager.Instance.type = DropType.Inventory;
        myImage.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DragAndDropManager.Instance.type = DropType.None;
        myImage.color = Color.white;
    }



}
