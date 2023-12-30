using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.U2D;

public class ItemInventoryDirector : MonoBehaviour
{
    public GameObject itemslotPrefab;
    public Transform gridTransform;

    public List<ItemInventorySlot> list;

    public SpriteAtlas atlas;

    public TMP_Text ItemName;
    public TMP_Text ItemPrice;
    public TMP_Text ItemAmount;
    public TMP_Text ItemExplain;

    public Button ClearButton;
    public Button InitButton;

    public ItemPanelContainer container;

    private ItemInventoryMain inventoryMain;

    void Start()
    {
        ClearButton.onClick.AddListener(ClearUI);
        if (InitButton != null)
        {
            InitButton.onClick.AddListener(ClearAllSlots);
        }
        inventoryMain = GameObject.FindGameObjectWithTag("ItemInventory").GetComponent<ItemInventoryMain>();
    }

    public void Init(ItemPanelContainer container)
    {
        this.container = container;
        if (this.container == null)
        {
            Debug.LogError("ItemPanelContainer is not assigned in the ItemInventoryDirector.");
            return;
        }

        if (list.Count == 0)
        {
            list = new List<ItemInventorySlot>();
            for (int i = 0; i < 16; i++)
            {
                var go = Instantiate(this.itemslotPrefab, this.gridTransform);
                var itemslot = go.GetComponent<ItemInventorySlot>();
                list.Add(itemslot);
            }
        }

        SetIndex();
    }

    private void SetIndex()
    {
        for (int i = 0; i < this.list.Count; i++)
        {
            list[i].SetIndex(i);
        }
    }

    public void UpdateSlot(ItemSlot updatedSlot)
    {
        ItemInventorySlot correspondingSlot = this.list[updatedSlot.index];
        if (updatedSlot.item == null || updatedSlot.amount == 0)
        {
            correspondingSlot.Clean();
        }
        else
        {
            correspondingSlot.Set(updatedSlot);
        }
    }

    public void UpdateSlotDetails(ItemSlot slot)
    {
        if (slot == null || slot.item == null || slot.amount == 0)
        {
            ClearText();
            return;
        }
        else
        {
            ItemName.text = "아이템 이름: " + slot.item.name;
            ItemPrice.text = "아이템 가격: " + slot.item.price.ToString();
            ItemExplain.text = "아이템 설명: " + slot.item.explain;

            if (slot.item.stackable)
            {
                ItemAmount.text = "아이템 개수: " + slot.amount.ToString();
            }
            else if (!slot.item.stackable)
            {
                ItemAmount.text = "아이템 개수: 1";
            }
        }
    }

    public void ClearUI()
    {
        foreach (var slot in container.slots)
        {
            slot.Clean();
        }
        foreach (var slot in this.list)
        {
            slot.Clean();
        }
        inventoryMain.SavePlayerInventory();
    }

    public void ClearText()
    {
        ItemName.text = "아이템 이름: ";
        ItemPrice.text = "아이템 가격: ";
        ItemExplain.text = "아이템 설명: ";
        ItemAmount.text = "아이템 개수: ";
    }

    public void ClearAllSlots()
    {
        if (container != null || container.slots != null)
        {
            container.slots.Clear();
        }
        if (list != null)
        {
            list.Clear();
        }
    }
}
