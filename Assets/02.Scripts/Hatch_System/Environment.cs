using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour {

    public float environment = 50f;
    public float environmentIncrement = 1f;

    public Image environmentLevel;

    void Start()
    {
        environmentLevel = gameObject.transform.GetChild(0).GetComponent<Image>();
        environmentLevel.fillAmount = environment / 100;

        StartCoroutine(ChangeEnvironment());
    }

    IEnumerator ChangeEnvironment()
    {
        while (true)
        {
            environment += environmentIncrement;

            environmentLevel.fillAmount = environment / 100;

            CheckEnvironment(environment);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void OnButtonDown()
    {
        environment = 50;
        environmentLevel.fillAmount = environment / 100;
    }

    virtual public void CheckEnvironment(float e)
    {

    }
}
