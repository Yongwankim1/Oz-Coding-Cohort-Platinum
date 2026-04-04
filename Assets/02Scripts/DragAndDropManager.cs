using UnityEngine;
public enum DropType
{
    None,
    Equip,
    Inventory
}
public class DragAndDropManager : MonoBehaviour
{
    public static DragAndDropManager Instance;

    public DropType type = DropType.None;

    public Vector2 dragingSlot = new Vector2(-1,-1);
    public Vector2 dropSlot = new Vector2(-1,-1);

    public GridData dragData = new GridData();
    public Sprite dragSprite;


    [SerializeField] PlayerInventoryGrid inventoryGrid;
    [SerializeField] PlayerEquipment playerEquipment;
    [SerializeField] PlayerInventory PlayerInventory;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if(inventoryGrid == null) inventoryGrid = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryGrid>();
    }

    public void ItemSlotChanged()
    {
        if (type == DropType.Inventory)
        {
            inventoryGrid.ChangeSlotItemId((int)dragingSlot.x, (int)dragingSlot.y, (int)dropSlot.x, (int)dropSlot.y);
            ///TODO:: 장비창에서 빼서 아이템에 슬롯에 넣어주는 코드도 추가해줘야함
        }
        else if (type == DropType.Equip)
        {
            playerEquipment.EquipItem(dragData.ItemID);
            PlayerInventory.RemoveItem(dragData.ItemID, dragData.Count, (int)dragingSlot.x, (int)dragingSlot.y);
            ///TODO:: 아이템 추가해줘야함
            ///예를 들어 아이템끼리 교환일때
        }
        dragingSlot = new Vector2(-1, -1);
        dropSlot = new Vector2(-1,- 1);
        dragData = new GridData();
    }
}
// 인벤토리에서 아이템드래그 시작(데이터 담음) -> 장비칸에 드랍(드랍할 데이터 담음) -> 장비칸 기존 아이템 자리 바꿈

// 장비칸에서 아이템 드래그 시작 -> 인벤토리 빈칸 or 장비가 있는 슬롯에 드랍 -> 자리 바꿈