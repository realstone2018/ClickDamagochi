using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour {

    public Text farmText;

    public string farmName;
    // 구입전 level은 0 
    public int level = 0;

    public int currentCost;
    public int startCost = 1;

    public int foodPerSec;
    public int startFoodPerSec = 1;

    public int costPow = 2;
    public int upgradePow = 2;

    public bool isPurchased = false;

    // 구매, 구매시 비용, 기능 업그레이드, 업그레이드시 UI변경 , 저장, 불러오기는 데이터컨트롤러를 통해서, 불러오기시 UI에 적용 
    void Awake()
    {
        // 데이타 불러오기 , 데이타 저장은 딜리게이트로?? or 데이타 콘트롤러에서 전부 ?
        DataController.instance.LoadFarm(this);
        UpdateUI();
    }

    public void PurchaseFarm()
    {
        if (DataController.instance.Gold >= currentCost)
        {
            DataController.instance.Gold -= currentCost;
            level++;
            isPurchased = true;

            UpdateFarm();
            UpdateUI();

            // GameManager에서 total로 계산해 코루틴을 돌린다. 
            GameManager.instance.UpdateFoodPerSec();
            DataController.instance.SaveFarm(this);
            // 임시적으로 여기 
            UIManager.instance.UIUpgade();
        }
    }

    public void UpdateFarm()
    {
        // 3.14 Pow로 증가폭이 너무 작아서 goldPerSec을 더하는걸로 수정 
        foodPerSec = foodPerSec + startFoodPerSec * (int)Mathf.Pow(upgradePow, level);
        currentCost = startCost * (int)Mathf.Pow(costPow, level);
    }

    public void UpdateUI()
    {
        farmText.text = farmName + "\n\nLv: " + level + "\nFoodPerSec: " + foodPerSec + "\nUpgradeCost :" + currentCost;
    }

    // 게임종료 딜리게이트 이벤트 발생 시 자료 save 함수 호출 

    public void LoadDate()
    {
        // 데이타 불러오기 , 데이타 저장은 딜리게이트로?? or 데이타 콘트롤러에서 전부 ?
        DataController.instance.LoadFarm(this);
        UpdateUI();
    }
}
