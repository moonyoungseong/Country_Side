using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ItemInventoryMain : MonoBehaviour
{
    public static ItemInventoryMain instance;

    private void Awake()
    {
        instance = this;
    }

    public ItemPanelContainer container;
    public ItemInventoryDirector director;
    public ItemDragAndDrop itemDragAndDrop;

    void Start()
    {
        DataManager.instance.LoadItemInventoryData();
        InfoManager.instance.Init();
        container.InitializeSlots();
        director.Init(container);
        LoadPlayerInventory();
    }

    public void pickup(ItemSlot updatedSlot)
    {
        director.UpdateSlot(updatedSlot);
        SavePlayerInventory();
    }

    public void SavePlayerInventory()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "PlayerInventory.json");
        string json = JsonConvert.SerializeObject(container.slots);
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        File.WriteAllText(filePath, json);
    }

    public void LoadPlayerInventory()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "PlayerInventory.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(json))
            {
                List<ItemSlot> loadedSlots = JsonConvert.DeserializeObject<List<ItemSlot>>(json);
                container.slots = loadedSlots;
                for (int i = 0; i < loadedSlots.Count; i++)
                {
                    director.list[i].GetSlot().item = loadedSlots[i].item;
                    director.list[i].Set(loadedSlots[i]);
                }
            }
        }
    }
}
