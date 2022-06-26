using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[10];

    public void Add(HotBarItem itemToAdd)
    {
        foreach(var hotbarSlot in hotbarSlots)
        {
            if (hotbarSlot.AddItem(itemToAdd))
            {
                return;
            }
        }
    }
}
