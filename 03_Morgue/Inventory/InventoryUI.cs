using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    private InventorySlot[] slots;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        List<Item> items = Inventory.Instance.GetItems();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].AddItem(items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
