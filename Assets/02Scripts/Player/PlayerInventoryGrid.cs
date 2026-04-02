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

    public void Initialzed(int row, int col)
    {
        inventoryGrid = new GridData[row, col];
    }

    public int SetGrid(string itemID, int amount)
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
}