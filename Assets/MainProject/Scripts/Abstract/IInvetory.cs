using System;

public interface IInvetory
{
    int capacity { get; set; }
    bool isFull { get; }

    IItem GetItem(Type itemType);
    IItem[] GetAllItems();
    IItem[] GetAllItems(Type itemType);
    int GetItemAmount(Type itemType);

    bool TryToAdd(IItem item);
    void Remove(Type itemType, int amount = 1);
    bool HasItem(Type type, out IItem item);
}
