using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] PlayerInventoryGrid inventoryGrid;
    [SerializeField] int rowCount;
    [SerializeField] int columnCount;

    public int RowCount => rowCount;
    public int ColumnCount => columnCount;
    public PlayerInventoryGrid InventoryGrid => inventoryGrid;

    Dictionary<string, int> itemIdByCount = new Dictionary<string, int>();
    public Dictionary<string, int> ItemIdByCount => itemIdByCount;

    public event Action OnItemAmountChanged;

    private void Awake()
    {
        if (inventoryGrid == null)
            inventoryGrid = GetComponent<PlayerInventoryGrid>();

        Init();
    }

    private void Init()
    {
        if (inventoryGrid != null)
            inventoryGrid.Initialzed(RowCount, ColumnCount);

        itemIdByCount.Clear();
    }

    private void RaiseItemChanged() => OnItemAmountChanged?.Invoke();

    public bool AddItem(string itemID, int amount, out int restAmount)
    {
        restAmount = -1;

        if (string.IsNullOrWhiteSpace(itemID)) return false;
        if (amount <= 0) return false;
        if (!ItemCatalogManager.Instance.TryGetItemData(itemID, out _)) return false;
        if (inventoryGrid == null) return false;

        restAmount = inventoryGrid.SetGrid(itemID, amount);
        int addedAmount = amount - restAmount;

        if (addedAmount <= 0)
            return true;

        if (!itemIdByCount.ContainsKey(itemID))
            itemIdByCount.Add(itemID, addedAmount);
        else
            IncreaseItemCount(itemID, addedAmount);

        RaiseItemChanged();
        return true;
    }

    private void IncreaseItemCount(string itemId, int amount)
    {
        if (amount <= 0) return;

        if (itemIdByCount.TryGetValue(itemId, out int value))
            itemIdByCount[itemId] = value + amount;
    }

    private void DecreaseItemCount(string itemId, int amount)
    {
        if (amount <= 0) return;

        if (itemIdByCount.TryGetValue(itemId, out int value))
        {
            int newValue = value - amount;

            if (newValue <= 0)
                itemIdByCount.Remove(itemId);
            else
                itemIdByCount[itemId] = newValue;
        }
    }
}