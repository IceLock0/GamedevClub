using System;
using UnityEngine;

public interface IItem
{ 
    IItemInfo info { get; }
    IItemState state { get; }
    Type type { get; }
    IItem Clone();
}
