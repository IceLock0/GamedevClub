using System;
using System.Collections;
using UnityEngine;


public class BanditPants : IItem
{
    public IItemInfo info { get; }
    public IItemState state { get; }
    public Type type => GetType();

    public BanditPants(IItemInfo info)
    {
        this.info = info;
        state = new ItemState();
    }

    public IItem Clone()
    {
        var clonedBanditPantsInfo = new BanditPants(info);
        clonedBanditPantsInfo.state.amount = state.amount;
        return clonedBanditPantsInfo;
    }
}
