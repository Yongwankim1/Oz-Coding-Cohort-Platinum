using System;
using UnityEngine;

public struct GridData
{
    public string ItemID;
    public int Count;
    public int MaxCount;
}

public class PlayerInventoryGrid : MonoBehaviour
{
    GridData[,] inventoryGrid = new GridData[0, 0];
    public GridData[,] InventoryGrid => inventoryGrid;
    public event Action OnSlotChangedAction;

    public void Initialzed(int row, int col)
    {
        inventoryGrid = new GridData[row, col];
    }
    /// <summary>
    /// 슬롯 체인지
    /// </summary>
    public void ChangeSlotItemId(int row, int col, int changeRow, int changeCol)
    {
        if(row < 0 || col < 0) return;
        if (changeCol < 0 || changeRow < 0) return;
        GridData gridTargetData = inventoryGrid[row, col];
        GridData gridData = inventoryGrid[changeRow, changeCol];

        inventoryGrid[row, col] = gridData;
        inventoryGrid[changeRow, changeCol] = gridTargetData;

        OnSlotChangedAction?.Invoke();
    }
    public void SetRemoveItemGrid(string itemId,int amount,int row, int col)
    {
        if(string.IsNullOrEmpty(itemId)) return;
        if(amount <= 0) return;
        if(row < 0 || row > inventoryGrid.GetLength(0) - 1) return;
        if(col < 0 || col > inventoryGrid.GetLength(1) - 1) return;

        if (inventoryGrid[row, col].ItemID != itemId) return;
        inventoryGrid[row, col].Count -= amount;
        if(inventoryGrid[row, col].Count <= 0)
        {
            inventoryGrid[row, col] = new GridData();
        }
    }

    public int SetAddGrid(string itemID, int amount)
    {
        if (!ItemCatalogManager.Instance.TryGetItemData(itemID, out ItemData itemData))
            return amount;

        for (int col = 0; col < inventoryGrid.GetLength(1); col++)
        {
            for (int row = 0; row < inventoryGrid.GetLength(0); row++)
            {
                GridData gridData = inventoryGrid[row, col];

                if (gridData.ItemID != itemID) continue;
                if (gridData.Count >= gridData.MaxCount) continue;

                int canAdd = gridData.MaxCount - gridData.Count;
                int addAmount = Mathf.Min(amount, canAdd);

                gridData.Count += addAmount;
                amount -= addAmount;

                inventoryGrid[row, col] = gridData;

                if (amount <= 0)
                    return 0;
            }
        }

        for (int col = 0; col < inventoryGrid.GetLength(1); col++)
        {
            for (int row = 0; row < inventoryGrid.GetLength(0); row++)
            {
                GridData gridData = inventoryGrid[row, col];

                if (!string.IsNullOrEmpty(gridData.ItemID)) continue;

                gridData.ItemID = itemID;
                gridData.MaxCount = itemData.MaxStack;

                int addAmount = Mathf.Min(amount, gridData.MaxCount);
                gridData.Count = addAmount;

                inventoryGrid[row, col] = gridData;
                amount -= addAmount;

                if (amount <= 0)
                    return 0;
            }
        }

        if (amount > 0)
        {
            Debug.Log($"인벤토리가 가득 차서 {amount}개를 넣지 못했습니다.");
        }

        return amount;
    }

    [ContextMenu("PrintItemGrid")]
    private void PrintItemGrid()
    {
        string itemDebug = string.Empty;
        Debug.Log("=============Inventory=============");
        for (int col = 0; col < inventoryGrid.GetLength(1); col++)
        {
            for (int row = 0; row < inventoryGrid.GetLength(0); row++)
            {
                itemDebug += $"[{inventoryGrid[row, col].ItemID}]\t";
            }
            Debug.Log(itemDebug);
            itemDebug = string.Empty;
        }
        Debug.Log("====================================");
    }
}