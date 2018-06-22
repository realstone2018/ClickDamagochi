using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowingMushroom : MonoBehaviour {

    public GameObject mushroomPrefab;

    public GameObject[] mushrooms = null;

    public float mushroomCont = 0f;
    public float growthRate = 1f;

    public Image mushroomLevel;

    void Start()
    {
        mushroomLevel = gameObject.transform.GetChild(0).GetComponent<Image>();
        mushroomLevel.fillAmount = mushroomCont / 100;

        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        while (true)
        {
            mushroomCont += growthRate;

            mushroomLevel.fillAmount = mushroomCont / 100;

            if (mushroomCont >= 100)
            {
                HatchResult.instance.HatchFalseByMushroom();
            }

            // load 했을때도 적용되게 버섯을 생성해야함         
            BloomMushroom();

            yield return new WaitForSeconds(1.0f);
        }
    }

    public void OnButtonDown()
    {
        mushroomCont = 0;
        mushroomLevel.fillAmount = mushroomCont / 100;

        StartCoroutine(MushroomPull());
    }

    IEnumerator MushroomPull()
    {
        foreach(GameObject mushroom in mushrooms)
        {
            if (mushroom.activeSelf)
                mushroom.GetComponent<Mushroom>().MushroomPull();

            yield return new WaitForSeconds(3.0f);
        }
    }

    public void BloomMushroom()
    {
        for(int i = 0; i < (int)mushroomCont/20; i++)
        {
            if (!mushrooms[i].activeSelf)
                mushrooms[i].SetActive(true);
        }
    }
}
