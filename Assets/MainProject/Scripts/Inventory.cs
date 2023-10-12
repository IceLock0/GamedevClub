using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : IInvetory
{
    private static Inventory _instance;

    public static Inventory instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Inventory(15);
            }
            return _instance;
        }
    }

    public int capacity { get; set; }
    public bool isFull => _slots.All(slot => slot.isFull);

    private List<ISlot> _slots;

    public event Action<IItem, int> OnInventoryItemAddedEvent;
    public event Action<Type, int> OnInventoryItemRemovedEvent;
    public event Action OnInventoryStateChangedEvent;

    public Inventory(int capacity)
    {
        this.capacity = capacity;

        _slots = new List<ISlot>(capacity);
        for (int i = 0; i < capacity; i++)
            _slots.Add(new Slot());
    }

    public ISlot GetItem(int index)
    {
        return _slots[index];
    }

    public IItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType).item;
    }

    public int GetItemAmount(Type itemType)
    {
        int amount = 0;
        List<ISlot> allitemSlots = _slots.
            FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
        foreach (var itemSlot in allitemSlots)
            amount += itemSlot.amount;
        return amount;
    }

    public bool HasItem(Type type, out IItem item)
    {
        item = GetItem(type);
        return item != null;
    }

    public void Remove(int slotNumber)
    {
        _slots[slotNumber].Clear();
    }

    public void Remove(Type itemType, int amount)
    {
        ISlot[] slotsWithItem = GetAllSlots(itemType);
        if (slotsWithItem.Length == 0) return;

        int amountToRemove = amount;
        int count = slotsWithItem.Length;

        for (int i = count - 1; i >= 0; i--)
        {
            ISlot slot = slotsWithItem[i];

            if (slot.amount >= amountToRemove)
            {
                slot.item.state.amount -= amountToRemove;

                if (slot.amount <= 0) 
                    slot.Clear();

                Debug.Log($"Item removed from inventory. Item type: {itemType}, amount: {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(itemType, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke();
                break;
            }

            int amountRemoved = slot.amount;
            amountToRemove -= slot.amount;
            slot.Clear();
            OnInventoryItemRemovedEvent?.Invoke(itemType, amountRemoved);
            OnInventoryStateChangedEvent?.Invoke();
        }
    }

    public void TransitFromSlotToSlot(ISlot fromSlot, ISlot toSlot)
    {
        if (fromSlot.isEmpty)
            return;

        if (toSlot.isFull)
            return;

        if (!toSlot.isEmpty && fromSlot.itemType != toSlot.itemType)
            return;

        int slotCapacity = fromSlot.capacity;
        bool fits = fromSlot.amount + toSlot.amount <= slotCapacity;
        int amountToAdd = fits ? fromSlot.amount : slotCapacity - toSlot.amount;
        int amountLeft = fromSlot.amount - amountToAdd;

        if (toSlot.isEmpty)
        {
            toSlot.SetItem(fromSlot.item);
            fromSlot.Clear();
            OnInventoryStateChangedEvent?.Invoke();
        }

        toSlot.item.state.amount += amountToAdd;
        if (fits)
            fromSlot.Clear();
        else
            fromSlot.item.state.amount = amountLeft;
        OnInventoryStateChangedEvent?.Invoke();
    }

    public ISlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }

    public ISlot[] GetAllSlots(Type itemType) 
    {
        return _slots.
            FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
    }

    public bool TryToAdd(IItem item)
    {
        ISlot slotWithSameItem = _slots.
            Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFull);

        if (slotWithSameItem != null) return TryToAddToSlot(slotWithSameItem, item);

        ISlot emptySlot = _slots.Find(slot => slot.isEmpty);
        if (emptySlot != null) return TryToAddToSlot(emptySlot, item);

        Debug.Log("Inventory is full");
        return false;
    }

    public bool TryToAddToSlot(ISlot slot, IItem item)
    {
        bool fits = slot.amount + item.state.amount <= item.info.maxItemsInInventorySlot;
        int amountToAdd = fits
            ? item.state.amount
            : item.info.maxItemsInInventorySlot - slot.amount;
        int amountLeft = item.state.amount - amountToAdd;

        if (slot.isEmpty)
        {
            IItem clonedItem = item.Clone();
            clonedItem.state.amount = amountToAdd;
            slot.SetItem(clonedItem);
        }
        else
            slot.item.state.amount += amountToAdd;

        Debug.Log($"Item added to inventory. Item type: {item.type}, amount: {amountToAdd}");
        OnInventoryItemAddedEvent?.Invoke(item, amountToAdd);
        OnInventoryStateChangedEvent?.Invoke();

        if (amountLeft <= 0)
            return true;

        item.state.amount = amountLeft;
        return TryToAdd(item);
    }

    public IItem[] GetAllItems()
    {
        List<IItem> allItems = new List<IItem>();
        foreach (var slot in _slots)
        {
            if (!slot.isEmpty)
                allItems.Add(slot.item);
        }
        return allItems.ToArray();
    }

    public IItem[] GetAllItems(Type itemType)
    {
        List<IItem> allItemsOftype = new List<IItem>();
        List<ISlot> slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);
        foreach (var slot in slotsOfType)
            allItemsOftype.Add(slot.item);
        return allItemsOftype.ToArray();
    }
}
