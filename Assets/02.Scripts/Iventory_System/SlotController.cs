using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour {

    void OnMouseEnter()
    {
        this.transform.parent.parent.GetComponent<InventoryController>().selectedSlot = this.transform;
        //Debug.Log(this.transform.parent.parent.GetComponent<InventoryController>().selectedSlot);
    }

    void OnMouseExit()
    {
        if (transform.parent.parent.GetComponent<InventoryController>().draggingItem)
            transform.parent.parent.GetComponent<InventoryController>().selectedSlot = null;
    }
}
