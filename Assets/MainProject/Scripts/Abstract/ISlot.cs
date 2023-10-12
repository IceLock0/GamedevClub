using System;
public interface ISlot 
{
    bool isFull { get; }
    bool isEmpty { get; }
    IItem item { get; }
    Type itemType { get; }
    int amount { get; }
    int capacity { get; }
    void SetItem(IItem item);
    void Clear();
}
