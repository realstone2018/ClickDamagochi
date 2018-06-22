using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Pet pet;

    public Text goldAndFoodText;
    // 경험치 UI 관련 변수 
    public Text expText;
    public Image expBar;
    // 포만감 UI 관련 변수
    public Text satietyText;
    public Image satietyBar;

    public float mRatio;

    public Text scaleText;

    public GameObject petStatePanel;
    public GameObject facilitesPanel;
    public GameObject farmList;
    public GameObject hunterList;

    public GameObject huntingControl;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;

        Screen.SetResolution(1440, 2960, true);
    }

    private void Start()
    {
        mRatio = pet.ExpPercent();
        expText.text = "EXP : " + mRatio + "%";
        expBar.transform.localScale = new Vector3(mRatio / 100f, 1, 1);
        // 어디선가 게임 호출할때 중복 호출, 찾아내서 중복제거 
        UIUpgade();

        ////////////////////////
        // 일단 임시로 여기에 //
        ////////////////////////
        UpdateScale();
    }
    public void UIUpgade()
    {
        goldAndFoodText.text = "Gold : " + DataController.instance.Gold + "\nFood : " + DataController.instance.Food;
        // expText.text =  현재레벨 경험치 / 현재 레벨까지 필요 경험치 
        //              =  총획득경험치 - 현제 레벨까지 누적 필요경험치의 / 다음레벨까지의 누적 경험치 - 현제레벨까지의 누적 필요경험치 
        mRatio = pet.ExpPercent();
        expText.text = "EXP : " + mRatio + "%";

        expBar.transform.localScale = new Vector3(mRatio / 100f, 1, 1);

        //Debug.Log("총획득경험치 : " + GameManager.instance.exp);
        //Debug.Log("현제레벨까지의 누적 필요경험치 : " + GameManager.instance.getSum());
        //Debug.Log("다음레벨까지의 누적 경험치 : " + GameManager.instance.cumulativeNeedExp());
        //Debug.Log("Gold : " + DataController.instance.Gold + "      Food : " + DataController.instance.Food);

    }

    ////////////////////////////////////////
    // 하나의 함수로 합쳐서 중복 제거하기 //
    ////////////////////////////////////////   switch문으로 ?  or 배열에 버튼들 저장하고 foreach문 돌렸을 때 true, false에 따라 true문 버튼 실행 ? 
    // 왜 활성화 / 비활성화로 처리안하고 이리했더라 
    public void OnHuntButton()
    {
        huntingControl.SetActive(true);
    }

    public void MovePanelToCenter(GameObject panel)
    {
        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.position = new Vector2(400, 260);
    }

    public void MovePanelToOutside(GameObject panel)
    {
        RectTransform rect = panel.GetComponent<RectTransform>();
        rect.position = new Vector2(0, 1000);
    }

    public void OnFarmButton()
    {
        farmList.SetActive(true);
        hunterList.SetActive(false);

    }

    public void OnHunterButton()
    {
        hunterList.SetActive(true);
        farmList.SetActive(false);

    }

    public void SatietyUIUpdate(int satiety)
    {
        satietyBar.fillAmount = satiety / 100;
        satietyText.text = "Satiety : " + satiety + "%";

    }

    public void OnFeedButton()
    {
        pet.EatMeat();
    }
 
    public void UpdateScale()
    {
        scaleText.text = DataController.instance.Scale.ToString();
    }
}
