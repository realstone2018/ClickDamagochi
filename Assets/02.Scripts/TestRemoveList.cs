using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRemoveList : MonoBehaviour {

    public static List<Item> items = new List<Item>();
    public Item item1;
    public Item item2;
    public Item item3;

    void Start()
    {
        items.Add(item1);
        items.Add(item2);
        items.Add(item3);

        RemoveList(item2);
        ReadList();

    }

    void ReadList()
    {
        foreach(Item item in items)
        {
            Debug.Log(item.itemName);
        }
    }

    void RemoveList(Item item)
    {
        items.Remove(item);
    }
}
