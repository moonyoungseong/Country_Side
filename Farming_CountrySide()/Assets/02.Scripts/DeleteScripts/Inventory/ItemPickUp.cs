using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public int itemId;
    public int amount = 1;

    void Pickup()
    {
        ItemInventoryMain.instance.container.Add(itemId, amount);
        ItemSlot slot = ItemInventoryMain.instance.container.slots.Find(x => x.item != null && x.item.id == itemId);
        if (slot != null)
        {
            ItemInventoryMain.instance.pickup(slot);
        }
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
