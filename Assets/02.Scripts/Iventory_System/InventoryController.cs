using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    public Transform selectedItem, selectedSlot, originalSlot, lastSelectedSlot;
    public bool draggingItem = false;

    public GameObject itemPrefab;
    public GameObject lastSelectedSlotBorder;
    public SlotController[] slots;

    public Item dropItem;
    public Item alreadyOwnItem;

    public static InventoryController _instance = null;

    void Awake()
    {
        _instance = this;

        slots = GameObject.FindObjectsOfType<SlotController>();
    }

    void Start()
    {
        DataController.instance.LoadInventory(this);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectedItem != null)
        {
            originalSlot = selectedSlot;

            draggingItem = true;
            SetItemsColliders(false);
        }
        else if (Input.GetMouseButton(0) && selectedItem != null && draggingItem)
        {
            selectedItem.position = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && selectedItem != null)
        {
            draggingItem = false;
            SetItemsColliders(true);

            // 슬롯이 아닌곳인가
            if (selectedSlot == null)
            {
                selectedItem.parent = originalSlot;
            }
            else if (selectedSlot == originalSlot)
            {
                lastSelectedSlot = selectedSlot;
                lastSelectedSlotBorder.transform.GetComponent<RectTransform>().anchoredPosition = lastSelectedSlot.GetComponent<RectTransform>().anchoredPosition;
            }
            else
            {
                // 다른아이템이 있는 슬롯인가
                if (selectedSlot.childCount > 0)
                {
                    // STACK ITEMS
                    if (selectedItem.name == selectedSlot.GetChild(0).name)
                    {
                        selectedSlot.GetChild(0).GetComponent<Item>().IncreaseAmount(selectedItem.GetComponent<Item>().amount);
                        Destroy(originalSlot.GetChild(0).gameObject);
                    }
                    // SWAP ITEMS
                    else
                    {
                        selectedSlot.GetChild(0).SetParent(originalSlot);
                        foreach (Transform t in originalSlot) t.localPosition = Vector3.zero;
                    }
                }

                selectedItem.parent = selectedSlot;

                lastSelectedSlot = selectedSlot;
                lastSelectedSlotBorder.transform.GetComponent<RectTransform>().anchoredPosition = lastSelectedSlot.GetComponent<RectTransform>().anchoredPosition;
            }
            selectedItem.localPosition = Vector3.zero;
        }
    }

    void SetItemsColliders(bool b)
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
        {
            item.GetComponent<Collider>().enabled = b;
        }
    }

    public void GetItem(string dropItemName, int quantity)
    {
        // Add New Item
        if (! ItemDB._instance.SearchAcquiredItemsListBool(dropItemName))
        {
            dropItem = ItemDB._instance.SearchItem(dropItemName);
            ItemDB._instance.AddNewItemInAcquiredItemsList(dropItem);

            GameObject itemObject = Instantiate(itemPrefab) as GameObject;
            itemObject.name = dropItem.itemName;
            itemObject.GetComponent<Image>().sprite = dropItem.sprite;
            
            SlotController emptySlot = SearchEmptySlot();

            // 비어있는 슬롯이 있는가 
            if (emptySlot == null)
            {
                // 인벤토리가 꽉찬 상태입니다 메세지 출력 
            }
            else itemObject.transform.SetParent(emptySlot.transform);
            itemObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;


            Item itemComponent = itemObject.GetComponent<Item>();
            //itemComponent = iData;  로 한번에 처리 안되나 ?
            itemComponent.itemName = dropItem.itemName;
            itemComponent.type = dropItem.type;
            itemComponent.effect = dropItem.effect;
            itemComponent.increment = dropItem.increment;
            itemComponent.IncreaseAmount(quantity - 1);
            itemComponent.sprite = dropItem.sprite;

        }
        // Stack Already Own Item 
        else
        {
            alreadyOwnItem = GameObject.Find(dropItemName).GetComponent<Item>();

            alreadyOwnItem.IncreaseAmount(quantity);
            alreadyOwnItem.transform.GetChild(0).GetComponent<Text>().text = alreadyOwnItem.amount.ToString();
        }

        DataController.instance.SaveInventory(this);
    }

    public SlotController SearchEmptySlot()
    {
        foreach(SlotController slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }

    public void UseItem(Item item)
    {
        if (item.type == Item.Type.consumable)
        {
            item.DecreaseAmount(1);

            Pet growingPet = GameObject.Find("Pet").GetComponent<Pet>();
            growingPet.AddStatus(item.effect, item.increment);
        }
    }

    /*
    public void CreateInventory()
    {

    }

    public void ClearSlot()
    {
        foreach (Transform t in this.transform.Find("SlotPanel")) 
        {
            if (t.childCount > 0) Destroy(t.GetChild(0).gameObject);
        }
    }
    */

}