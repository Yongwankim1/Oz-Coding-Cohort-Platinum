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

    public GridData GridData {  get; private set; }
    public int Row => row;
    public int Col => col;

    public string ItemID { get; private set; }
    public void DrawSlot(GridData gridData, int row, int col)
    {
        Initialized(row, col);
        if (itemImage == null) return;
        if (amountText == null) return;
        if (string.IsNullOrWhiteSpace(gridData.ItemID)) return;
        this.row = row;
        this.col = col;

        if (ItemCatalogManager.Instance.TryGetItemData(gridData.ItemID, out ItemData data))
        {
            GridData = gridData;
            amount = gridData.Count;
            ItemID = data.ItemID;
            itemImage.sprite = data.ItemIcon;
            displayName = data.DisplayName;
            description = data.Description;
            itemImage.gameObject.SetActive(true);
            if (amount > 1)
            {
                amountText.gameObject.SetActive(true);
                amountText.text = amount.ToString();
            }
        }
        else
        {
            itemImage.sprite = null;
            displayName = string.Empty;
            description = string.Empty;
            itemImage.gameObject.SetActive(false);
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
        ItemID = string.Empty;
        displayName = string.Empty;
        description = string.Empty;
        amount = 0;
        itemImage.sprite = null;
        itemImage.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
        amountText.text = string.Empty;
    }
}