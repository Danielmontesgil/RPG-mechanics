using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarSlot : ItemSlotUI
{
    [SerializeField] private Inventory inventory = null;
    [SerializeField] private TextMeshProUGUI itemQuantityText = null;

    private HotBarItem slotItem = null;

    protected override void Start()
    {
        base.Start();
        EventManager.Instance.AddListener<OnItemsUpdated>(OnItemsUpdated);
    }

    private void OnDisable()
    {
        if(EventManager.HasInstance())
        {
            EventManager.Instance.RemoveListener<OnItemsUpdated>(OnItemsUpdated);
        }
    }

    public override HotBarItem SlotItem 
    {
        get { return slotItem; }
        set
        {
            slotItem = value;
            UpdateSlotUI();
        }
    }

    public bool AddItem(HotBarItem itemToAdd)
    {
        if(SlotItem !=  null)
        {
            return false;
        }

        SlotItem = itemToAdd;

        return true;
    }

    public void UseSlot(int index)
    {
        if(index != SlotIndex)
        {
            return;
        }
    }

    public override void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        if(itemDragHandler == null)
        {
            return;
        }

        InventorySlot inventorySlot  =  itemDragHandler.ItemSlotUI  as InventorySlot;
        if(inventorySlot != null)
        {
            SlotItem = inventorySlot.ItemSlot.item;
            return;
        }

        HotbarSlot hotbarSlot = itemDragHandler.ItemSlotUI as HotbarSlot;
        if(hotbarSlot != null)
        {
            HotBarItem oldItem = SlotItem;
            SlotItem = hotbarSlot.SlotItem;
            hotbarSlot.SlotItem = oldItem;
            return;
        }
    }

    private void OnItemsUpdated(OnItemsUpdated e)
    {
        UpdateSlotUI();
    }

    public override void UpdateSlotUI()
    {
        if(SlotItem  == null)
        {
            EnableSlotUI(false);
            return;
        }

        itemIconImage.sprite = SlotItem.Icon;

        EnableSlotUI(true);

        SetItemQuantityUI();
    }

    private void SetItemQuantityUI()
    {
        if(SlotItem is InventoryItem inventoryItem)
        {
            if (inventory.ItemContainer.HasItem(inventoryItem))
            {
                int quantityCount = inventory.ItemContainer.GetTotalQuantity(inventoryItem);
                itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";
            }
            else
            {
                SlotItem = null;
            }
        }
        else
        {
            itemQuantityText.enabled = false;
        }
    }

    protected override void EnableSlotUI(bool enable)
    {
        base.EnableSlotUI(enable);
        itemQuantityText.enabled=enable;
    }
}
