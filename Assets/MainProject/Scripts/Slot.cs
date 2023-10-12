﻿
using System;

public class Slot : ISlot
{
    public bool isFull => !isEmpty && amount == capacity;
    public bool isEmpty => item == null;

    public IItem item { get; private set; }

    public int amount => isEmpty? 0 : item.state.amount;

    public Type itemType => item.type;

    public int capacity { get; private set; }

    public void Clear()
    {
        if (isEmpty) return;

        item.state.amount = 0;
        item = null;
    }

    public void SetItem(IItem item)
    {
        if (!isEmpty) return;

        this.item = item;
        this.capacity = item.info.maxItemsInInventorySlot;
    }
}
