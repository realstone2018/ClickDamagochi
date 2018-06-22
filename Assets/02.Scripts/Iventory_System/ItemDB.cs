using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour {

    public Sprite[] sprites;
    public static List<Item> acquiredItems = new List<Item>();
    public static List<Item> wholeItems = new List<Item>();
    //public static List<Item> allItems = new List<Item>();
    //public static List<Item> sortedItems = new List<Item>();
    
    public static ItemDB _instance = null;

    void Awake()
    {
        _instance = this;
        /*
        Item i0 = gameObject.GetComponent<Item>();
        Item i1 = gameObject.GetComponent<Item>();
        i0.name = "Egg";
        i1.name = "Meat";
        */
        Item i0 = new Item();
        //Item i0 = new Item("Dragon Egg", Item.Type.misc, sprites[0]);    new Item() 이 오류가 안나면 생성자로 생성하기 
        i0.itemName = "Egg";
        i0.type = Item.Type.consumable;
        i0.effect = "HP";
        i0.increment = 10;
        i0.sprite = sprites[0];
        wholeItems.Add(i0);

        Item i1 = new Item();
        i1.itemName = "Meat";
        i1.type = Item.Type.consumable;
        i1.effect = "Damage";
        i1.increment = 1;
        i1.sprite = sprites[1];
        wholeItems.Add(i1);

    }

    public bool SearchAcquiredItemsListBool(string dropItemName)
    {
        foreach (Item item in acquiredItems)
        {
            if (item.itemName == dropItemName)
            {
                //Debug.Log("SearchAcquredItemsList is return true");
                return true;
            }
        }
        //Debug.Log("SearchAcquredItemsList is return false");
        return false;
    }


    public void AddNewItemInAcquiredItemsList(Item item)
    {
        acquiredItems.Add(item);

    }

    public void RemoveItemInAcquiredItemsList(string item)
    {
        /*
        Item removeItem = null;
        
        foreach (Item i in acquiredItems)
        {
            if (i.itemName == item)
            {
                removeItem = i;
            }
        }

        Debug.Log("Remove : " + removeItem.itemName);
        acquiredItems.Remove(removeItem);
        */
        int n = 0;

        for (int i = 0; i < acquiredItems.Count; i++)
        {
            if (acquiredItems[i].itemName == item)
            {
                n = i;
            }
        }

        acquiredItems.RemoveAt(n);
       
        foreach (Item i in acquiredItems)
        {
            Debug.Log(i.itemName);
            Debug.Log(i.effect);
            Debug.Log(i.amount);
        }

        /*
        for (int i = 0; i < acquiredItems.Count; i++)
        {
            Debug.Log("List [" + i + "] : " + acquiredItems[i].itemName);
            Debug.Log("List [" + i + "] : " + acquiredItems[i].effect);
            Debug.Log("List [" + i + "] : " + acquiredItems[i].amount);
        }
        */
    }

    public Item SearchItem(string searchingItemName)
    {
        foreach(Item item in wholeItems)
        {
            if (item.itemName == searchingItemName)
            {
                return item;
            }
        }

        return null;
    }

    /* 
    public void SortAllItems()
    {
        sortedItems.Clear();

        foreach(Item item in allItems)
        {
            sortedItems.Add(item);
        }
    }

    public void SortItemByType(string type)
    {
        sortedItems.Clear();

        foreach(Item item in allItems)
        {
            if (item.type.ToString() == type)
                sortedItems.Add(item);
            
        }
    }

    // Stack or Add 결정 여기서?
    public void AddItem(Item item)
    {
        allItems.Add(item);
        //SortAllItems();
    }
        */
}
