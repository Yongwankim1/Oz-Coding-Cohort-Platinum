using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] int row;
    [SerializeField] int col;
    [SerializeField] Image itemImage;
    [SerializeField] string displayName;
    [SerializeField] string description;
    [SerializeField] int amount;
    [SerializeField] TextMeshProUGUI amountText;

    public void Initialized(GridData gridData, int row, int col)
    {
        if (itemImage == null) return;
        if (amountText == null) return;

        this.row = row;
        this.col = col;
        amount = gridData.Count;

        if (ItemCatalogManager.Instance.TryGetItemData(gridData.ItemID, out ItemData data))
        {
            itemImage.sprite = data.ItemIcon;
            displayName = data.DisplayName;
            description = data.Description;
            itemImage.gameObject.SetActive(true);
        }
        else
        {
            itemImage.sprite = null;
            displayName = string.Empty;
            description = string.Empty;
            itemImage.gameObject.SetActive(false);
        }

        if (amount > 1)
        {
            amountText.gameObject.SetActive(true);
            amountText.text = amount.ToString();
        }
        else
        {
            amountText.gameObject.SetActive(false);
            amountText.text = string.Empty;
        }
    }

    public void Initialized(int row, int col)
    {
        if (itemImage == null) return;
        if (amountText == null) return;

        this.row = row;
        this.col = col;
        displayName = string.Empty;
        description = string.Empty;
        amount = 0;
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
        amountText.text = string.Empty;
    }
}