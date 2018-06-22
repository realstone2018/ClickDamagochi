using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int totalFoodPerSec = 0;
    public Farm[] farms;
    public int totalGoldPerSec = 0;
    public Hunter[] hunters;

    public static GameManager instance;

    void Awake()
    {
        instance = this;

        // 비활성화되어있어 불가능 
        //facilities = GetComponents<FacilitiesButton>();

    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();

        StartCoroutine(PlusFoodPerSec());
        StartCoroutine(PlusGoldPerSec());
        // HunterButton, FarmButton 스크립트의 Start에서 호출 불가능, 비활성화상태여서 
        UpdateFoodPerSec();
        UpdateGoldPerSec();
    }


    IEnumerator PlusFoodPerSec()
    {
        while (true)
        {
            DataController.instance.Food += totalFoodPerSec;

            // 일단 임시적으로 여기 
            UIManager.instance.UIUpgade();

            yield return new WaitForSeconds(1.0f);
        }
    }

    // FarmButton스크립트에서 구매시 호출 
    // 하나 구매시마다 foreach를 돌리기보다 더  좋은방법 생각하기 
    public void UpdateFoodPerSec()
    {
        totalFoodPerSec = 0;

        foreach (Farm farm in farms)
        {
            if (farm.isPurchased)
            {
                totalFoodPerSec += farm.foodPerSec;
            }
        }
    }

    IEnumerator PlusGoldPerSec()
    {
        while (true)
        {
            DataController.instance.Gold += totalGoldPerSec;

            // 일단 임시적으로 여기 
            UIManager.instance.UIUpgade();

            yield return new WaitForSeconds(1.0f);
        }
    }

    // FarmButton스크립트에서 구매시 호출 
    // 하나 구매시마다 foreach를 돌리기보다 더  좋은방법 생각하기 
    public void UpdateGoldPerSec()
    {
        totalGoldPerSec = 0;

        foreach (Hunter hunter in hunters)
        {
            if (hunter.isPurchased)
            {
                totalGoldPerSec += hunter.goldPerSec;
            }
        }
    }
}
