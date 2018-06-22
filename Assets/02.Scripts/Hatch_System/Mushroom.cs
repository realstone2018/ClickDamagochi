using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {

	//public enum State { bloom, idle, pulled};
    //public State state = State.bloom;

    void Update()
    {
        
    }

    void OnEnable()
    {
        // 자라는 애니메이션 실행 -> 자동응로 idle 애니메이션으로 넘겨서 반복 
    }

    public void MushroomPull()
    {
        // 뽑히는 애니메이션 실행 
    }

    // 뽑히는 애니메이션 마지막에 이벤트 호출 
    public void ActvieFalseObject()
    {
        gameObject.SetActive(false);
    }

}
