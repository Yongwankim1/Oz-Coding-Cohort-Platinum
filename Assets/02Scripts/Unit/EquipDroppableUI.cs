using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipDroppableUI : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    EquipmentSlotUI equipmentSlotUI;
    Image myImage;
    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        if (equipmentSlotUI == null) equipmentSlotUI = GetComponent<EquipmentSlotUI>();
        myImage = GetComponent<Image>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        //DragAndDropManager.Instance.dropData = equipmentSlotUI.GridData;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DragAndDropManager.Instance.type = DropType.Equip;
        myImage.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DragAndDropManager.Instance.type = DropType.None;
        myImage.color = Color.white;
    }

}
