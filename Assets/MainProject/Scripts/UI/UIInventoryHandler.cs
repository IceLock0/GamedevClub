using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIInventoryHandler
{
    private ItemInfo _bulletproofCloakInfo;
    private ItemInfo _banditPantsInfo;
    private UIInventorySlot[] _uiSlots;

    public Inventory inventory = Inventory.instance;

    public UIInventoryHandler(ItemInfo bulletproofCloakInfo, ItemInfo banditPantsInfo, UIInventorySlot[] uiSlots)
    {
        _bulletproofCloakInfo = bulletproofCloakInfo;
        _banditPantsInfo = banditPantsInfo;
        _uiSlots = uiSlots;

        inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }

    public void FillSlots()
    {
        ISlot[] allSlots = inventory.GetAllSlots();
        List<ISlot> availableSlots = new List<ISlot>(allSlots);

        int filledSlots = 5;
        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomBulletproofCloakIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);

            filledSlot = AddRandomBanditPantsIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);
        }

        SetupInventoryUI(inventory);
    }

    private ISlot AddRandomBulletproofCloakIntoRandomSlot(List<ISlot> slots)
    {
        int rSlotIndex = Random.Range(0, slots.Count);
        ISlot rSlot = slots[rSlotIndex];
        int rCount = Random.Range(1, 4);
        BulletproofCloak bulletproofCloak = new BulletproofCloak(_bulletproofCloakInfo);
        bulletproofCloak.state.amount = rCount;
        inventory.TryToAddToSlot(rSlot, bulletproofCloak);
        return rSlot;
    }

    private ISlot AddRandomBanditPantsIntoRandomSlot(List<ISlot> slots)
    {
        int rSlotIndex = Random.Range(0, slots.Count);
        ISlot rSlot = slots[rSlotIndex];
        int rCount = Random.Range(1, 4);
        BanditPants banditPants = new BanditPants(_banditPantsInfo);
        banditPants.state.amount = rCount;
        inventory.TryToAddToSlot(rSlot, banditPants);
        return rSlot;
    }

    public void SetupInventoryUI(Inventory inventory)
    {
        ISlot[] allSlots = inventory.GetAllSlots();
        int allSlotsCount = allSlots.Length;
        for (int i = 0; i < allSlotsCount; i++)
        {
            ISlot slot = allSlots[i];
            UIInventorySlot uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    private void OnInventoryStateChanged()
    {
        foreach (var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }
}
