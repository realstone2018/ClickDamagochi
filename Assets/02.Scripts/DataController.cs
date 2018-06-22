using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour {

    public int Scale
    {
        get
        {
            return PlayerPrefs.GetInt("Scale", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Scale", value);
        }
    }

    public int Gold
    {
        get
        {
            return PlayerPrefs.GetInt("Gold", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Gold", value);
        }
    }

    public int Food
    {
        get
        {
            return PlayerPrefs.GetInt("Food", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Food", value);
        }
    }


    public static DataController instance;

    void Awake()
    {
        instance = this;

        //PlayerPrefs.DeleteAll();
    }

    public void LoadFarm(Farm farm)
    {
        string key = farm.farmName;

        farm.level = PlayerPrefs.GetInt(key + "_level", 0);
        farm.currentCost = PlayerPrefs.GetInt(key + "_currentCost", 0);
        farm.foodPerSec = PlayerPrefs.GetInt(key + "_foodPerSec", 0);

        if (PlayerPrefs.GetInt(key + "_purchased") == 1)
        {
            farm.isPurchased = true;
        }
        else
        {
            farm.isPurchased = false;
        }
    }
    
    public void SaveFarm(Farm farm)
    {
        string key = farm.farmName;

        PlayerPrefs.SetInt(key + "_level", farm.level);
        PlayerPrefs.SetInt(key + "_currentCost", farm.currentCost);
        PlayerPrefs.SetInt(key + "_foodPerSec", farm.foodPerSec);

        if (farm.isPurchased)
        {
            PlayerPrefs.SetInt(key + "_purchased", 1);
        }
        else
        {
            PlayerPrefs.SetInt(key + "_purchased", 0);
        }
    }

    public void LoadHunter(Hunter hunter)
    {
        string key = hunter.hunterName;

        hunter.level = PlayerPrefs.GetInt(key + "_level", 0);
        hunter.currentCost = PlayerPrefs.GetInt(key + "_currentCost", 0);
        hunter.goldPerSec = PlayerPrefs.GetInt(key + "_goldPerSec", 0);

        if (PlayerPrefs.GetInt(key + "_purchased") == 1)
        {
            hunter.isPurchased = true;
        }
        else
        {
            hunter.isPurchased = false;
        }
    }

    public void SaveHunter(Hunter hunter)
    {
        string key = hunter.hunterName;

        PlayerPrefs.SetInt(key + "_level", hunter.level);
        PlayerPrefs.SetInt(key + "_currentCost", hunter.currentCost);
        PlayerPrefs.SetInt(key + "_goldPerSec", hunter.goldPerSec);

        if (hunter.isPurchased)
        {
            PlayerPrefs.SetInt(key + "_purchased", 1);
        }
        else
        {
            PlayerPrefs.SetInt(key + "_purchased", 0);
        }
    }

    public void SavePetStatus(Pet pet)
    {
        string key = "Pet";

        PlayerPrefs.SetInt(key + "_exp", pet.exp);
        PlayerPrefs.SetInt(key + "satiety", pet.satiety);
    }

    public void LoadPetStatus(Pet pet)
    {
        string key = "Pet";

        pet.exp = PlayerPrefs.GetInt(key + "_exp", 0);
        pet.satiety = PlayerPrefs.GetInt(key + "satiety", 100);
    }

    public void SaveMonsterLevel(Monster mon)
    {
        string key = "Monster";
        PlayerPrefs.SetInt(key + "_lv", mon.monsterLv);
    }

    public void LoadMonsterLevel(Monster mon)
    {
        string key = "Monster";
        mon.monsterLv = PlayerPrefs.GetInt(key + "_lv", 1);
    }

    public void SaveInventory(InventoryController inven)
    {

        /*
        foreach(SlotController slot in inven.slots)
        {
            if (slot.transform.childCount > 0)
            {
                Transform t = slot.transform.GetChild(0);
                PlayerPrefs.GetString(key + .name + "ItemName", t.name);
                PlayerPrefs.GetString(key + t.name + "_Quantity", t.GetChild(0).GetComponent<Text>().text);

            }
        }
        */
        for (int i = 0; i < inven.slots.Length; i++)
        {
            if (inven.slots[i].transform.childCount > 0)
            {
                string key = "Slot_" + i;
                Transform item = inven.slots[i].transform.GetChild(0);

                //Debug.Log("SaveInventory_" + item.name + " : " + item.GetComponent<Item>().amount);
                PlayerPrefs.SetString(key + "_ItemName", item.name);
                PlayerPrefs.SetInt(key + "_Quantity", item.GetComponent<Item>().amount);

            }
        }
    }

    public void LoadInventory(InventoryController inven)
    {
        for (int i = 0; i < inven.slots.Length; i++)
        {
            string key = "Slot_" + i;

            string itemName = PlayerPrefs.GetString(key + "_ItemName", "Empty");
            //Debug.Log("LoadInventory_Slot_" + i + " : " + itemName);
            
            // 인벤창에서 아이템 수량의 0은 에러를 의미 
            int itemQuantity = PlayerPrefs.GetInt(key + "_Quantity", 0);

            if (itemName != "Empty") {
                //Debug.Log("LoadInventory_Slot" + i + "_" + itemName + " : " + itemQuantity);
                //Debug.Log("LoadInventory_Slot_" + i + "is not null, If is not Empty");
                InventoryController._instance.GetItem(itemName, itemQuantity);
            }
        }
    }
}
