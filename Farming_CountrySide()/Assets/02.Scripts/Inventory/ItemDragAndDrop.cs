using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class ItemDragAndDrop : MonoBehaviour
{
    public ItemSlot dragSlot;
    [SerializeField] GameObject itemIcon;
    [SerializeField] Image itemIconImage;
    [SerializeField] SpriteAtlas atlas;
    private RectTransform iconTransform;

    public bool isDragging = false;

    private void Start()
    {
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
        itemIcon.SetActive(false);
    }

    private void Update()
    {
        if (itemIcon.activeInHierarchy)
        {
            iconTransform.position = Input.mousePosition;
        }
    }

    public void StartDrag(ItemSlot startSlot)
    {
        dragSlot = startSlot;
        if(dragSlot.amount==0)
        {
            Debug.Log("Don't Drop");
            isDragging = false;
            return;
        }
        UpdateIcon(startSlot);
        isDragging = true;
    }

    public void Drop(ItemSlot dropSlot)
    {
        if (dragSlot == null)
        {
            return;
        }

        if (dropSlot.item == null)
        {
            dropSlot.Copy(dragSlot);
            dragSlot.Clean();
            ItemInventoryMain.instance.director.UpdateSlot(dropSlot);
            ItemInventoryMain.instance.director.UpdateSlot(dragSlot);  // Update the UI of the dragSlot too
        }
        else
        {
            ItemInventoryData tempItem = dragSlot.item;
            int tempAmount = dragSlot.amount;

            dragSlot.Set(dropSlot.item, dropSlot.amount);
            dropSlot.Set(tempItem, tempAmount);

            ItemInventoryMain.instance.director.UpdateSlot(dragSlot);
            ItemInventoryMain.instance.director.UpdateSlot(dropSlot);
        }

        dragSlot = null;
        itemIcon.SetActive(false);
        ItemInventoryMain.instance.SavePlayerInventory();
        isDragging = false;
    }


    private void UpdateUI(ItemSlot slot)
    {
        if (ItemInventoryMain.instance.container.slots.Contains(slot))
        {
            int index = ItemInventoryMain.instance.container.slots.IndexOf(slot);
            ItemInventorySlot uiSlot = ItemInventoryMain.instance.director.list[index];

            if (uiSlot != null)
            {
                if (slot.item == null || slot.amount == 0)
                {
                    uiSlot.Clean();
                }
                else
                {
                    uiSlot.Set(slot);
                }
            }
        }
        else
        {
            Debug.LogError("Slot not found in the list: " + slot);
        }
    }

    private void UpdateIcon(ItemSlot slot)
    {
        if(slot.amount==0)
        {
            Debug.Log("No Slot");
            return;
        }
        if (slot != null || slot.item != null)
        {
            itemIcon.SetActive(true);
            var itemData = DataManager.instance.GetItemInventoryData(slot.item.id);
            if (itemData != null)
            {
                itemIconImage.sprite = atlas.GetSprite(itemData.spritename);
            }
            itemIconImage.raycastTarget = false;
        }
        else
        {
            itemIcon.SetActive(false);
        }
    }
}
