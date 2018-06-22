using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingControl : MonoBehaviour {


    // 배경 애니메이션 전부 정지 
    // 몬스터 등장 
    // Pet 탐색상태에서  사냥 상태로 변경 (이때 Pet 스크립트 내에서 애니메이션 변경 )

    // 몬스터 사망시 몬스터함수에서 델리게이트 발생,  배경 다시 움직이기, Pet 상태를 다시 탐색 상태로  

    //  캐릭터를 달리는 상태로,    backgrounds.FlowControl(speed);        foregroundsFlowControl(speed);    재생하기 
    public BackgroundControl backgrounds;
    public BackgroundControl foregrounds;

    public GameObject monsterObject;

    private void Awake()
    {
        // 델리게이트 등록 
        Monster.MonsterDowwn += StartHunting;
    }

    private void Start()
    {
        // Pet을 탐색 상태로 변경 
        backgrounds.FlowControl(1.0f);
        foregrounds.FlowControl(1.0f);

        StartCoroutine(Hunting());
    }

    IEnumerator Hunting()
    {
        yield return new WaitForSeconds(5.0f);

        // Pet을 교전상태로 변경 
        // 배경이동 정지
        backgrounds.FlowControl(0.0f);
        foregrounds.FlowControl(0.0f);

        // 몬스터 활성화 
        monsterObject.SetActive(true);

    }

    // 몬스터 죽었을 시 델리게이트 발생 ,  배경 다시움직이기, Pet 탐색 상태로 변경, 몬스터 그림 변경 
    public void StartHunting()
    {
        backgrounds.FlowControl(1.0f);
        foregrounds.FlowControl(1.0f);

        StartCoroutine(Hunting());
    }

}
