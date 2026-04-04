using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputReader : MonoBehaviour
{
    [SerializeField,Tooltip("PlayerInput을 담아둘 변수입니다 참조가 되어있어야합니다")]
    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction mouseAction;
    private InputAction interactAction;
    private InputAction inventoryAction;

    [Header("액션 이름(동일)")]
    [SerializeField] 
    private string moveName = "Move";
    [SerializeField] 
    private string mouseName = "Mouse";
    [SerializeField] 
    private string interactName = "Interact";
    [SerializeField]
    private string inventoryName = "InventoryToggle";

    private Vector2 moveVector2D;
    private Vector2 mouseVector2D;

    public bool IsInteractPerformedThisFrame {  get; private set; }
    public bool IsInventoryTogglePerformedThisFrame { get; private set; }
    public float Horizontal {  get; private set; }
    public float Vertical { get; private set; }

    public float MouseAxisX { get; private set; }
    public float MouseAxisY { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        if(playerInput == null) playerInput = GetComponent<PlayerInput>();

        moveAction = FindAction(moveName);
        mouseAction = FindAction(mouseName);
        interactAction = FindAction(interactName);
        inventoryAction = FindAction(inventoryName);
    }

    private InputAction FindAction(string actionName)
    {
        if(playerInput == null) return null;
        if (string.IsNullOrWhiteSpace(actionName)) return null;
        InputAction action = null;
        action = playerInput.actions.FindAction(actionName,false);

        if (action == null) Debug.LogWarning($"{action}이 null입니다 확인하세요");

        return action;
    }
    // Update is called once per frame
    void Update()
    {
        moveVector2D = moveAction.ReadValue<Vector2>().normalized;
        mouseVector2D = mouseAction.ReadValue<Vector2>();

        Horizontal = moveVector2D.x;
        Vertical = moveVector2D.y;

        MouseAxisX = mouseVector2D.x;
        MouseAxisY = mouseVector2D.y;

        IsInteractPerformedThisFrame = interactAction != null && interactAction.WasPerformedThisFrame();
        IsInventoryTogglePerformedThisFrame = inventoryAction != null && inventoryAction.WasPerformedThisFrame();
    }
}
