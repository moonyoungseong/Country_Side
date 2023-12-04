using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class InfoManager
{
    public static readonly InfoManager instance = new InfoManager();

    public List<ItemInventoryInfo> ItemInventoryInfos { get; set; }

    private InfoManager() { }

    public void Init()
    {
        this.ItemInventoryInfos = new List<ItemInventoryInfo>();
    }

    public void SaveItemInventoryInfos()
    {
        var path = string.Format("{0}/item_inventory_info.json", Application.persistentDataPath);
        //직렬화 
        var json = JsonConvert.SerializeObject(this.ItemInventoryInfos);

        Debug.Log(json);

        //파일로 저장 
        File.WriteAllText(path, json);
        Debug.Log("<color=yellow>[save success] item_inventory_data.json</color>");
    }

    public void LoadItemInventoryInfos()
    {
        var path = string.Format("{0}/item_inventory_info.json", Application.persistentDataPath);
        var json = File.ReadAllText(path);
        //역직렬화 
        var arr = JsonConvert.DeserializeObject<ItemInventoryInfo[]>(json);
        this.ItemInventoryInfos = arr.ToList();

        Debug.Log("<color=yellow>[load success] item_inventory_info.json</color>");
    }

    public ItemInventoryInfo GetItemInventoryInfo(int id)
    {
        return this.ItemInventoryInfos.Find(x => x.id == id);
    }

}


