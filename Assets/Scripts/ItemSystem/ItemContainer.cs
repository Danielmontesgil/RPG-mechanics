using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemContainer : IItemContainer
{
    private ItemSlot[] itemSlots = new ItemSlot[0];

    public ItemContainer(int size) => itemSlots = new ItemSlot[size];

    public ItemSlot GetSlotByIndex(int slotIndex) => itemSlots[slotIndex];

    public ItemSlot AddItem(ItemSlot slot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item != null)
            {
                if (itemSlots[i].item == slot.item)
                {
                    int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                    if (slot.quantity <= slotRemainingSpace)
                    {
                        itemSlots[i].quantity += slot.quantity;

                        slot.quantity = 0;

                        EventManager.Instance.Trigger(new OnItemsUpdated());

                        return slot;
                    }
                    else if (slotRemainingSpace > 0)
                    {
                        itemSlots[i].quantity += slotRemainingSpace;

                        slot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                if (slot.quantity <= slot.item.MaxStack)
                {
                    itemSlots[i] = slot;

                    slot.quantity = 0;

                    EventManager.Instance.Trigger(new OnItemsUpdated());

                    return slot;
                }
                else
                {
                    itemSlots[i] = new ItemSlot(slot.item, slot.item.MaxStack);

                    slot.quantity -= slot.item.MaxStack;

                }
            }
        }

        EventManager.Instance.Trigger(new OnItemsUpdated());

        return slot;
    }

    public int GetTotalQuantity(InventoryItem item)
    {
        int totalCount = 0;

        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == null)
            {
                continue;
            }

            if (slot.item != item)
            {
                continue;
            }

            totalCount += slot.quantity;
        }

        return totalCount;
    }

    public bool HasItem(InventoryItem item)
    {
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == null)
            {
                continue;
            }

            if (slot.item != item)
            {
                continue;
            }

            return true;
        }

        return false;
    }

    public void RemoveAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= itemSlots.Length - 1)
        {
            return;
        }

        itemSlots[slotIndex] = new ItemSlot();

        EventManager.Instance.Trigger(new OnItemsUpdated());
    }

    public void RemoveItem(ItemSlot slot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item != null)
            {
                if (itemSlots[i].item == slot.item)
                {
                    if (itemSlots[i].quantity < slot.quantity)
                    {
                        slot.quantity -= itemSlots[i].quantity;

                        itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        itemSlots[i].quantity -= slot.quantity;

                        if (itemSlots[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();

                            EventManager.Instance.Trigger(new OnItemsUpdated());

                            return;
                        }
                    }
                }
            }
        }
    }

    public void Swap(int indexOne, int indexTwo)
    {
        ItemSlot firstSlot = itemSlots[indexOne];
        ItemSlot secondSlot = itemSlots[indexTwo];

        if (firstSlot == secondSlot)
        {
            return;
        }

        if (secondSlot.item != null)
        {
            if (firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                if (firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    itemSlots[indexTwo].quantity += firstSlot.quantity;

                    itemSlots[indexOne] = new ItemSlot();

                    EventManager.Instance.Trigger(new OnItemsUpdated());

                    return;
                }
            }
        }

        itemSlots[indexOne] = secondSlot;
        itemSlots[indexTwo] = firstSlot;

        EventManager.Instance.Trigger(new OnItemsUpdated());
    }
}
