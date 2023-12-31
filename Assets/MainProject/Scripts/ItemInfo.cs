﻿using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Gameplay/Items/Create new ItemInfo")]
public class ItemInfo : ScriptableObject, IItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private int _maxItemsInInventorySlot;
    [SerializeField] private Sprite _spriteIcon;

    public string id => _id;

    public string title => _title;

    public string description => _description;

    public int maxItemsInInventorySlot => _maxItemsInInventorySlot;

    public Sprite spriteIcon => _spriteIcon;
}
