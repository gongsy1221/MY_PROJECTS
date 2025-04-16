using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    public InventoryUI inventoryUI;

    private List<Item> items = new List<Item>();

    private void Awake()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void Add(Item item)
    {
        items.Add(item);
        inventoryUI.UpdateUI();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        inventoryUI.UpdateUI();
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
