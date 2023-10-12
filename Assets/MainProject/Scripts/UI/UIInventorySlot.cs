using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : UISlot
{
    [SerializeField] private UIInventoryItem _uiInventoryItem;

    public ISlot slot { get;private set; }

    private UIInventory _uiInventory;

    private void Awake()
    {
        _uiInventory = GetComponentInParent<UIInventory>();   
    }

    public void SetSlot(ISlot newSlot)
    {
        slot = newSlot;
    }

    public override void OnDrop(PointerEventData eventdata)
    {
        UIInventoryItem otherItemUI = eventdata.pointerDrag.GetComponent<UIInventoryItem>();
        UIInventorySlot otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
        ISlot otherSlot = otherSlotUI.slot;
        Inventory inventory = _uiInventory.inventory;

        inventory.TransitFromSlotToSlot(otherSlot, slot);

        Refresh();
        otherSlotUI.Refresh();
    }

    public void Refresh()
    {
        if (slot != null)
        {
            _uiInventoryItem.Refresh(slot);
        }
    }
}

