using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 몬스터 오브젝트는 죽을때마다 Destroy 새로운 몬스터를 instance하는게 아닌 죽을때마다 HP회복, Ui이미지만 바꿔준다.
public class Monster : MonoBehaviour {

    public InventoryController inventoryController;

    public Image hpBar;

    public int monsterLv = 1;

    public float initHp = 100f;
    public float fullHp = 100f;
    public float hp;
    public float initDamage = 1;
    public float damage = 1;

    public Sprite[] monsterImages = null;
    public int monsterType = 0;

    public Transform monsterTr;
    public Vector3 initPosition;

    //Status 상태 확인은 어디서 ? 할필요가없나? 필요할때만 확인 ?
    // Status가 어디서 필요한거지 
    public enum Status {
        idle,
        Attack,
        Hit,
        Die
    }
    public Status monsterStatus = Status.idle;

    public delegate void Down();
    public static event Down MonsterDowwn;

    void Awake()
    {
        // Level 데이터 Load후 hp, damage 계산을 위해 MonsterLvelUp()함수를 호출, 이때 monsterLv--를 통해 level 유지  
        DataController.instance.LoadMonsterLevel(this);
        monsterLv--;
        MonsterLevelUp();
    }

    void Start()
    {
        monsterTr = gameObject.GetComponent<Transform>();
    }

    public void MonsterHit(Pet pet)
    {
        hp -= pet.damage;
        hpBar.fillAmount = hp / fullHp;

        StartCoroutine(ShakeMonster());

        if (hp <= 0)
        {
            pet.Cure();
            MonsterDie(pet);
        }
    }

    //Hp가 0이되면 Die 
    private void MonsterDie(Pet pet)
    {
        // Die()함수로 나눠야하나 ?
        // 골드, 푸드 증가, 경험치 증가,    골드와 경험치는 몬스터 레벨에 따라 증가량변수설정하기, level * 증가량
        DataController.instance.Gold += 5;
        DataController.instance.Food += 1;
        pet.AddExp(20);

        // 아이템 드랍 
        DropItem();

        // AddExp()에서 monsterlv가 올라가므로  여기서 hp = fullHp 해도 이상 무 
        MonsterLevelUp();

        // 변경된 골드, 푸드, 경험치데이타 UI적용
        UIManager.instance.UIUpgade();

        // 딜리게이트 이벤트 발생 (HuntingControl 스크립트에서 이벤트 받음)
        MonsterDowwn();

        gameObject.SetActive(false);
    }

    // GameManager에서 펫 레벨업시 호출 
    public void MonsterLevelUp()
    {
        monsterLv++;

        damage = initDamage + monsterLv * 0.1f;
        fullHp = initHp + monsterLv * 0.2f;

        hp = fullHp;
        hpBar.fillAmount = hp / fullHp;

        // 랜덤 몬스터 이미지 변경
        monsterType = Random.Range(0, 2);
        gameObject.GetComponent<Image>().sprite = monsterImages[monsterType];

        DataController.instance.SaveMonsterLevel(this);
    }


    public void DropItem()
    {
        int ran = Random.Range(0, 6);
        Debug.Log("DropItem Random : " + ran);

        // if(ran = 2) {        일단 테스트를 위해 확률 적용 x 
        switch (monsterType)
        {
            case 0 :
                inventoryController.GetItem("Egg", 1);
                break;
            case 1:
                inventoryController.GetItem("Meat", 1);
                break;
        }        
    }

    IEnumerator ShakeMonster()
    {
        initPosition = monsterTr.position;

        for (int i = 0; i < 3; i++)
        {
            
            Vector2 randomPos = Random.insideUnitCircle * 0.3f;
            monsterTr.position = initPosition + new Vector3(randomPos.x * 80.0f, randomPos.y * 50.0f, 0);
            yield return new WaitForSeconds(0.03f);
        }

        monsterTr.position = initPosition;

    }
}

