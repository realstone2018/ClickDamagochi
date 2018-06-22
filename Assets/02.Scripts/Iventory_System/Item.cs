using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    //public string name;
    public string itemName;
    public enum Type { equip, consumable, misc };

    public Type type;
    public Sprite sprite;

    public int amount = 1;
    public string effect;
    public int increment = 1;

    void OnMouseEnter()
    {
        this.transform.parent.parent.parent.GetComponent<InventoryController>().selectedItem = this.transform;
    }

    void OnMouseExit()
    {
        if (!transform.parent.parent.parent.GetComponent<InventoryController>().draggingItem)
            transform.parent.parent.parent.GetComponent<InventoryController>().selectedItem = null;
    }

    public void IncreaseAmount(int value)
    {
        amount += value;
        transform.GetChild(0).gameObject.GetComponent<Text>().text = amount.ToString();
    
    }

    public void DecreaseAmount(int value)
    {
        amount -= value;
        transform.GetChild(0).gameObject.GetComponent<Text>().text = amount.ToString();

        if (amount == 0)
        {
            ItemDB._instance.RemoveItemInAcquiredItemsList(this.itemName);
            Destroy(this.gameObject);
        }
    }
}
