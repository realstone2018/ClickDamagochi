using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pet : MonoBehaviour {

    public int petLv = 1;
    public int exp = 0;
    public int levelBalance = 40;

    public int initDamage = 10;
    public int damage = 10;
    public float initHp = 100;
    public float fullHp = 100;
    public float hp;
    public Image hpBar;
    public Text statusText;

    public GameObject wantMeet;
    public GameObject poopPrefab;
    public Transform poopPoint;

    // 포만감
    public int satiety;
    // 기분 
    public int mood = 100;

    public Transform petTr;
    public Vector3 initPosition;

    void Awake()
    {
        petTr = gameObject.GetComponent<Transform>();
        // Monster와 다르게 Pet은 경험치 조건이 만족해야 StatusUpgrade()함수에 들어가기 때문에 hp 초기화 필요 
        hp = fullHp;

        // exp Load 후 Level계산, level에 따른 hp, damage계산을 위해 AddExp()함수 호출  
        DataController.instance.LoadPetStatus(this);
        AddExp(0);

        // 데이타 불러오기 , 데이타 저장은 딜리게이트로?? or 데이타 콘트롤러에서 전부 ?
        UpdateStatusUI();
    }

    void Start()
    {
        StartCoroutine(Digest());
        StartCoroutine(Hungry());
        // Pet Hp는 직접 해결,  포만감은 왜 UI에서 해결? 이것도 합칠까 ?
        UIManager.instance.SatietyUIUpdate(satiety);
        //Debug.Log("Satiety : " + satiety);
    }

    // 일정 시간마다 포만감 감소, 30감소 마다 poop 생성
    IEnumerator Digest()
    {
        while (true)
        {
            // 30번 반복 후 (30분 후)
            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForSeconds(2f);
                satiety--;
                UIManager.instance.SatietyUIUpdate(satiety);
            }

            ////////////////////////////////////////////////////////
            // 나중에 반드시 수정하기   10초마다 저장 노노노   /////
            ////////////////////////////////////////////////////////
            DataController.instance.SavePetStatus(this);

            // 응가 UI생성를 랜덤  반지름 0.3 원형 안에 생성  
            Vector2 randomPos = Random.insideUnitCircle * 0.3f;
            GameObject poop = Instantiate(poopPrefab, poopPoint.position + new Vector3(randomPos.x * 80.0f, randomPos.y * 150.0f, 0), Quaternion.identity);
            poop.transform.parent = gameObject.transform;
        }
    }

    IEnumerator Hungry()
    {
        while (true)
        {
            if (satiety <= 0)
            {
                // 다운 혹은 다이

            }
            else if (satiety <= 20)
            {
                // 배고픔 UI 생성
                wantMeet.SetActive(true);
            }
            // 포만감이 21 이상이면 배고픔 UI 비활성화 
            else
            {
                wantMeet.SetActive(false);
            }
            // 3분 마다 체크 , 테스트를위해서 10초마다 
            yield return new WaitForSeconds(10f);
        }
    }

    // Click 스크립트에서 화면이 클릭되었을때 호출 
    public void PetAttack(Monster monster)
    {
        hp -= monster.damage;
        hpBar.fillAmount =  hp / fullHp;
        // 나주에 프로펄티로 고칠거라 일단 다른 클래스 변수에 직접접근 
        monster.MonsterHit(this);

        StartCoroutine(ShakeMonster());

        if (hp <= 0)
        {
            PetDie();
        }
    }

    public void PetDie()
    {

    }

    // UIManager스크립트에서 버튼이 눌렀을 때 호출 
    public void EatMeat()
    {
        // 섭취량을 구한후, Food와 비교
        int intake = 100 - satiety;

        if (DataController.instance.Food >= intake)
        {
            satiety += intake;
            DataController.instance.Food -= intake;
            UIManager.instance.SatietyUIUpdate(satiety);
        }
        else
        {
            satiety += DataController.instance.Food;
            DataController.instance.Food = 0;
        }
    }

    public void SubMood()
    {
        mood -= 3;
    }

    public void Refresh()
    {
        StartCoroutine(Refeshing());
    }

    // Scene에 Poop 컴포넌트를 가진 객체가 없다면 Mood를 1분마다 증가, 100 초과시 100으로 
    // 코루틴 true로 돌릴필요없이 청소시에 Mood가 100이 될때까지만 실행 
    IEnumerator Refeshing()
    {
        while (mood <= 100)
        {
            yield return new WaitForSeconds(60.0f);

            if (FindObjectOfType<Poop>() == null)
            {
                mood += 3;
            }
        }

        mood = 100; 
    }

    public int GetExp()
    {
        return exp;
    }

    // EXP를 더하고 만약 경험치%가 100이 되면 lv 증가 
    public void AddExp(int e)
    {
        exp += e;

        if (ExpPercent() >= 100f)
        {
            petLv = ExpToLevel();
            // 레벨증가에따른 Pet 데이터 변환 함수 호출하기 
            StatusUpgrade();
        }

        DataController.instance.SavePetStatus(this);
    }

    public float ExpPercent()
    {
        // 현제 레벨에서의 경험치 / 현제 레벨에서 레벨업에 필요한 경험치 * 100
        float ratio = (float)(exp - GetSum()) / (float)(CumulativeNeedExp() - GetSum()) * 100f;
        return ratio;
    }

    // 현제 레벨까지의 누적필요경험치 반환, lv가 3일때 120 반환 
    public int GetSum()
    {
        int sum = 0;
        for (int i = 1; i < petLv; ++i)
        {
            sum += i;
        }
        return sum * levelBalance;
    }

    public int CumulativeNeedExp()
    {
        int sum = 0;
        for (int i = 1; i <= petLv; ++i)
        {
            sum += i;
        }
        return sum * levelBalance;
    }

    // 경험치를 레벨로 변환  
    // 왜 왜 레벨업시 ++단순히로 않하는가 =>  ExpPerCent()가 100%가 됬을 때 레벨업 시 한번에 2~3업 시 오류 발생 (Add Exp()함수에 예외설정을 하지 않는한)
    //                                     => 데이터  Save, Load 시 EXP만하면 Ok, Level은 불러온뒤 EXP에 따라 계산하면 된다.   
    // 왜 능력치도 단순히 ++로 안하고 레벨값으로 계산하는가 =>  레벨이 높을수록 능력치 증가값 증가  ++로 해결될게 x 
    public int ExpToLevel()
    {
        int sum = 0;
        // sum 증가량 i 
        // sum은 1, 3, 6, 10 ... 으로 증가 ㅂ
        //    40,  120,  240,  400 .. 
        //  40   80   120  160
        // 1업당 다음 레벨업을 위한 필요 경험치는 40씩 늘어난다. 
        int i = 1;

        // xp보다  sum * 40 이 클때까지 반복
        while (true)
        {
            sum += i;

            if (sum * levelBalance > exp)
            {
                return i;
            }
            i++;
        }
    }

    public void StatusUpgrade()
    {
        // 몬스터 체력증가량과 조율하기 
        damage = initDamage + petLv * 2;
        // 몬스터 공격력과 조율하기 
        fullHp = initHp + petLv * 10;

        Cure();
        UpdateStatusUI();
    }

    // Monster스크립트에서 몬스터가 죽었을때 호출 
    public void Cure()
    {
        hp = fullHp;
        hpBar.fillAmount = hp / fullHp;
    }


    public void UpdateStatusUI()
    {
        statusText.text = "LV : " + petLv + "\nHP : " + fullHp + "\nPOWER : " + damage;
    }

    public void AddStatus(string stat, int increment)
    {            
        switch (stat)
        {
            case "Damage":
                damage += increment;
                break;
            case "HP":
                fullHp += increment;
                break;
        }

        UpdateStatusUI();
    }

    IEnumerator ShakeMonster()
    {
        initPosition = petTr.position;

        for (int i = 0; i < 2; i++)
        {

            Vector2 randomPos = Random.insideUnitCircle * 0.3f;
            petTr.position = initPosition + new Vector3(randomPos.x * 50.0f, randomPos.y * 25.0f, 0);
            yield return new WaitForSeconds(0.03f);
        }

        petTr.position = initPosition;
    }
}
