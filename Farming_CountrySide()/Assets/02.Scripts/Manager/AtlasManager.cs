using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : MonoBehaviour
{
    public static AtlasManager instance;

    public SpriteAtlas[] arrAtlas;
    public Dictionary<string, SpriteAtlas> dicAtlas = new Dictionary<string, SpriteAtlas>();

    private void Awake()
    {
        AtlasManager.instance = this;

        Debug.LogFormat("this.arrAtlas.Length: {0}", this.arrAtlas.Length);

        for (int i = 0; i < this.arrAtlas.Length; i++)
        {
            var atlas = this.arrAtlas[i];

            var atlasName = atlas.name.Replace("Atlas", "");
            this.dicAtlas.Add(atlasName, atlas);

            Debug.Log("Loaded atlas: " + atlasName);
        }

    }

    public SpriteAtlas GetAtlasByName(string name) //key
    {
        Debug.Log("Fetching atlas with name: " + name);

        if (this.dicAtlas.ContainsKey(name))
        {
            return this.dicAtlas[name];
        }
        else
        {
            Debug.LogError("No atlas found with name: " + name);
            return null;
        }
    }
}