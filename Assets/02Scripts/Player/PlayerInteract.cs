using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] InputAction interactAction;
    private List<IInteractable> curInteractable = new List<IInteractable>();

    private void Awake()
    {
        interactAction.Enable();
    }
    private void Update()
    {
        if(interactAction.WasPerformedThisFrame())
        {
            Interact();
        }
    }
    void Interact()
    {
        if(curInteractable.Count == 0)
        {
            Debug.Log("상호작용 대상 없음");
            return;
        }
        IInteractable target = curInteractable[curInteractable.Count - 1];
        curInteractable.RemoveAt(curInteractable.Count-1);
        if (target == null)
        {
            Debug.Log("상호작용 대상이 비어있음");
            return;
        }
        target.Interact(this);
    }
    private void OnTriggerEnter(Collider other)
    { 
        IInteractable target = other.GetComponent<IInteractable>();
        if (target == null)
        {
            return;
        }
        curInteractable.Add(target);
    }
    private void OnTriggerExit(Collider other)
    {
        IInteractable target = other.GetComponent<IInteractable>();
        if (target == null)
        {
            return;
        }
        if (curInteractable.Contains(target))
        {
            curInteractable.Remove(target);
        }
    }
}
