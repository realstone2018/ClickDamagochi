using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchResult : MonoBehaviour {

    public static HatchResult instance = null;

    public Environment temperatureScript;
    public Environment moistureScript;
    public GrowingMushroom mushroomScript;

    public float temperature;
    public float moisture;
    public float mushroom;
     
    void Awake()
    {
        instance = this;
    }

	void Start ()
    {
        temperature = temperatureScript.environment;
        moisture = moistureScript.environment;
        mushroom = mushroomScript.mushroomCont;
	}

    public void HatchFalseByTemperature()
    {
        if (temperature >= 100)
        {
            // 파이어에그 Get
        }
        else
        {
            // 냉동달걀 Get
        }
    }

    public void HatchFalseByMushroom()
    {
        // 동충하초 Get
    }

    public void Hatch()
    {
        // 각 값에따라 속성 추가 하기
        // 씬넘기기 

    }
}
