using UnityEngine;

public class InventoryGUI : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] PlayerInventory inventory;
    [SerializeField] PlayerInventoryGrid inventoryGrid;
    [SerializeField] InventorySlotUI slotPrefab;
    [SerializeField] Transform parentTransform;

    [SerializeField] InventorySlotUI[,] inventorySlotUIs = new InventorySlotUI[0, 0];

    private void Awake()
    {
        if (inventory == null)
            inventory = GetComponent<PlayerInventory>();

        if (inventoryGrid == null && inventory != null)
            inventoryGrid = inventory.InventoryGrid;

        Init();
    }

    public void Init()
    {
        if (slotPrefab == null) return;
        if (inventory == null) return;
        if (inventoryGrid == null) return;
        if (parentTransform == null) return;

        inventorySlotUIs = new InventorySlotUI[inventory.RowCount, inventory.ColumnCount];

        for (int col = 0; col < inventory.ColumnCount; col++)
        {
            for (int row = 0; row < inventory.RowCount; row++)
            {
                inventorySlotUIs[row, col] = Instantiate(slotPrefab, parentTransform);
                inventorySlotUIs[row, col].Initialized(row, col);
            }
        }
    }

    private void OnEnable()
    {
        if (inventory != null)
            inventory.OnItemAmountChanged += ReDrawAllUI;
    }

    private void OnDisable()
    {
        if (inventory != null)
            inventory.OnItemAmountChanged -= ReDrawAllUI;
    }

    void ReDrawAllUI()
    {
        if (inventory == null) return;
        if (inventoryGrid == null) return;
        if (inventoryGrid.InventoryGrid == null) return;

        for (int col = 0; col < inventory.ColumnCount; col++)
        {
            for (int row = 0; row < inventory.RowCount; row++)
            {
                inventorySlotUIs[row, col].Initialized(inventoryGrid.InventoryGrid[row, col], row, col);
            }
        }
    }
}