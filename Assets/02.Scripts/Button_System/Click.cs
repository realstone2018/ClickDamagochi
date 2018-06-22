using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    public Pet petScript;
    public GameObject monster;
    private Monster monsterScript;

    void Start () {
        petScript = FindObjectOfType<Pet>();
        monsterScript = monster.GetComponent<Monster>();
    }

    public void OnClick()
    {
        if (monster.activeSelf)
        {
            petScript.PetAttack(monsterScript);
        }
    }
}
