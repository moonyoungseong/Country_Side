using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public ItemInventoryData item;
    public int amount;
    public int index; 

    public void Copy(ItemSlot slot)
    {
        this.item = slot.item;
        this.amount = slot.amount;
        //this.index = slot.index; 
    }

    public void Set(ItemInventoryData item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void Clean()
    {
        this.item = null;
        this.amount = 0;
    }
}

[CreateAssetMenu(menuName = "Item/Item Container")]
public class ItemPanelContainer : ScriptableObject
{
    public List<ItemSlot> slots;

    public void InitializeSlots()
    {
        if (slots.Count == 0)
        {
            for (int i = 0; i < 16; i++)
            {
                var slot = new ItemSlot();
                slot.index = i;
                slots.Add(slot);
                Debug.Log("Added slot " + i);
            }
        }
        else
        {
            Debug.Log("Slots already initialized. Count: " + slots.Count);
        }
    }

    public void Add(int itemId, int amount = 1)
    {
        ItemInventoryData item = DataManager.instance.GetItemInventoryData(itemId);

        if (item == null)
        {
            return;
        }

        ItemSlot existingItemSlot = slots.Find(x => x.item != null && x.item.id == itemId);

        if (item.stackable)
        {
            if (existingItemSlot != null)
            {
                existingItemSlot.amount += amount;
            }
            else
            {
                ItemSlot emptySlot = slots.Find(x => x.item == null || x.amount == 0);

                if (emptySlot != null)
                {
                    emptySlot.Set(item, amount);  
                }
                else
                {
                    Debug.Log("Inventory is full. Cannot add item with ID " + itemId + " and amount " + amount);
                }
            }
        }
        else
        {
            if (existingItemSlot == null)
            {
                ItemSlot emptySlot = slots.Find(x => x.item == null);

                if (emptySlot != null)
                {
                    emptySlot.Set(item, 1);  
                }
                else
                {
                    Debug.Log("Inventory is full. Cannot add non-stackable item with ID " + itemId);
                }
            }
        }
    }
}
