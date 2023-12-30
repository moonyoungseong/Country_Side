using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInventorySlot : MonoBehaviour, IPointerClickHandler
{
    public int id;
    private Button button;
    public ItemSlot slot;
    public Image ItemSprite;
    public SpriteAtlas atlas;
    public int Index;

    void Start()
    {
        /*
        this.button = GetComponent<Button>();
        button.onClick.AddListener(() => ItemInventoryMain.instance.director.GetComponent<ItemInventoryDirector>().UpdateSlotDetails(slot));
        */
    }

    public void SetIndex(int index)
    {
        Index = index;
    }

    public ItemSlot GetSlot()
    {
        return slot;
    }

    public void Set(ItemSlot slot)
    {
        if (slot == null || slot.item == null || slot.item.id == 0)
        {
            return;
        }

        var ItemInventoryData = DataManager.instance.GetItemInventoryData(slot.item.id);
        if (ItemInventoryData == null)
        {
            return;
        }

        Sprite sp = atlas.GetSprite(ItemInventoryData.spritename);
        if (sp == null)
        {
            return;
        }

        this.slot = slot;
        this.id = slot.item.id;
        this.ItemSprite.sprite = sp;
    }

    public void Clean()
    {
        Sprite sp = atlas.GetSprite("d_iconclosebutton");
        this.ItemSprite.sprite = sp;
        this.slot.item = null;
        this.slot.amount = 0;
        GameObject.FindGameObjectWithTag("InventoryUI").GetComponent<ItemInventoryDirector>().ClearText();
        this.id = 0;
    }
    /*
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
        {
            return;
        }

        ItemPanelContainer container = ItemInventoryMain.instance.container;
        ItemSlot clickedSlot = container.slots[Index];

        if (!ItemInventoryMain.instance.itemDragAndDrop.isDragging)
        {
            ItemInventoryMain.instance.itemDragAndDrop.StartDrag(clickedSlot);
        }
        else
        {
            ItemInventoryMain.instance.itemDragAndDrop.Drop(clickedSlot);
        }
    }
    */
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPanelContainer container = ItemInventoryMain.instance.container;
        ItemSlot clickedSlot = container.slots[Index];

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ItemInventoryMain.instance.director.GetComponent<ItemInventoryDirector>().UpdateSlotDetails(slot);
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!ItemInventoryMain.instance.itemDragAndDrop.isDragging)
            {
                ItemInventoryMain.instance.itemDragAndDrop.StartDrag(clickedSlot);
            }
            else
            {
                ItemInventoryMain.instance.itemDragAndDrop.Drop(clickedSlot);
            }
        }
    }

}
