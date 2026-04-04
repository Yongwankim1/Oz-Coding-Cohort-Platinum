using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{

    [SerializeField] private ItemType equipType;
    [SerializeField] Sprite noneEquipSlotSprite;
    [SerializeField] Sprite equipSlotSprite;
    [SerializeField] Image slotImage;

    [SerializeField] Image iconImage;
    [SerializeField] ItemData itemData;
    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        slotImage.sprite = noneEquipSlotSprite;
        iconImage.sprite = null;
        iconImage.gameObject.SetActive(false);
    }

    public void DrawSlot(string itemID)
    {
        Initialize();
        if (!ItemCatalogManager.Instance.TryGetItemData(itemID, out ItemData itemData))
        {
            return;
        }

        iconImage.sprite = itemData.ItemIcon;
        iconImage.gameObject.SetActive(true);
    }
}
