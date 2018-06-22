using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour {

    public void OnButtonDown()
    {
        if (InventoryController._instance.lastSelectedSlot != null)
        {
            Item item = InventoryController._instance.lastSelectedSlot.transform.GetChild(0).GetComponent<Item>();
            InventoryController._instance.UseItem(item);
        }
    }
}
