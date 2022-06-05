using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item[] items = new Item[30];

    void Start()
    {
        // Find all ScripteableObjects of the type Item in the Asset database. Does not work at runtime.
        var itemGuids = AssetDatabase.FindAssets("t:" + nameof(Item)).Select(AssetDatabase.GUIDToAssetPath).ToArray();

        // Iterate through the inventory, select a random item and put it in the current position.
        for (int i = 0; i < 30; i++)
        {
            var r = Random.Range(0, itemGuids.Length);
            items[i] = AssetDatabase.LoadAssetAtPath<Item>(itemGuids[r]);
        }

        // Update the UI
        RenderInventory();
    }

    /// <summary>
    /// Update the inventory UI
    /// </summary>
    public void RenderInventory()
    {
        for (int i = 0; i < 30; i++)
            transform.GetChild(i).GetComponent<Image>().sprite = items[i].icon;
    }
}
