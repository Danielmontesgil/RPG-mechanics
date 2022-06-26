using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : HotBarItem
{
    [Header("Item Data")]
    [SerializeField] private Rarity rarity = null;
    [SerializeField] [Min(0)] private int sellPrice = 1;
    [SerializeField] [Min(0)] private int maxStack = 1;

    public override string ColoredName
    {
        get
        {
            string hexColour = ColorUtility.ToHtmlStringRGB(rarity.TextColour);
            return $"<color=#{hexColour}>{Name}</color>";
        }
    }
    public int SellPrice => sellPrice;
    public int MaxStack => maxStack;
    public Rarity Rarity => rarity;
}
