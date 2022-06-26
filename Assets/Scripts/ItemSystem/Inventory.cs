using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName ="Items/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private ItemSlot testHealthPotion = new ItemSlot();
    [SerializeField] private ItemSlot testManaPotion = new ItemSlot();

    public ItemContainer ItemContainer { get; } = new ItemContainer(20);

    [ContextMenu("Test Add Health Potion")]
    public void AddHelathPotion()
    {
        ItemContainer.AddItem(testHealthPotion);
    }

    public void AddManaPotion()
    {
        ItemContainer.AddItem(testManaPotion);
    }
}
