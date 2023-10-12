using System;
using System.Collections;
using UnityEditor;
using UnityEngine;


public class BulletproofCloak : IItem
{
    public IItemInfo info { get; }
    public IItemState state { get; }
    public Type type => GetType();

    public BulletproofCloak(IItemInfo info)
    {
        this.info = info;
        state = new ItemState();
    }

    public IItem Clone()
    {
        var clonedBulletproofCloakInfo = new BulletproofCloak(info);
        clonedBulletproofCloakInfo.state.amount = state.amount;
        return clonedBulletproofCloakInfo;
    }
}
