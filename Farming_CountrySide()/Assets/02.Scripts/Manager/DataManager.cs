using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

public class DataManager
{
    public static readonly DataManager instance = new DataManager();
    private ItemInventoryData defaultItem;

    private DataManager()
    {
        
        defaultItem = new ItemInventoryData
        {
            id = 0,
            type = 0,
            name = "디폴트 아이템",
            stackable = false,
            price = 0,
            explain = "아무것도 없는 디폴트 아이템이다",
            spritename = "default_sprite"
        };
        
    }


    private Dictionary<int, ItemInventoryData> dicItemInventoryData;
   
    public void LoadItemInventoryData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Item_Inventory.json");
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);
        var arr = JsonConvert.DeserializeObject<ItemInventoryData[]>(json);
        this.dicItemInventoryData = arr.ToDictionary(x => x.id);
        Debug.LogFormat("Item inventory data loaded : <color=yellow>{0}</color>", this.dicItemInventoryData.Count);
    }

    public void SaveItemInventoryData()
    {
        var json = JsonConvert.SerializeObject(this.dicItemInventoryData.Values.ToArray());
        var path = Path.Combine(Application.streamingAssetsPath, "Item_Inventory.json");
        File.WriteAllText(path, json);
        Debug.LogFormat("Item inventory data saved : <color=yellow>{0}</color>", this.dicItemInventoryData.Count);
    }

    public ItemInventoryData GetItemInventoryData(int id)
    {
        if (this.dicItemInventoryData.ContainsKey(id))
        {
            return this.dicItemInventoryData[id];
        }
        else
        {
            Debug.LogError("No item with ID " + id + " exists in the inventory data.");
            return null;
        }
    }


    public int GetItemInventoryCount()
    {
        return this.dicItemInventoryData.Count;
    }
    public IEnumerable <ItemInventoryData> GetItemInventoryDatas()
    {
        return dicItemInventoryData.Values;
    }

}
