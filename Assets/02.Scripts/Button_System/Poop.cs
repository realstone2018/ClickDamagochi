using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour {
    public Pet pet;

    void Start()
    {
        pet = FindObjectOfType<Pet>();
        StartCoroutine(Barfy());
    }

    IEnumerator Barfy()
    {
        while (true)
        {
            yield return new WaitForSeconds(60.0f);
            pet.SubMood();
        }
    }

    // 클릭되면 제거 
    public void OnClick()
    {
        pet.Refresh();


        Destroy(gameObject);
    }
}
