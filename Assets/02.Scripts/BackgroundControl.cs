using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour {

    public Animator[] mBackgrounds;

    private void Start()
    {
        FlowControl(0);
    }

    public void FlowControl(float speed)
    {
        foreach(Animator bg in mBackgrounds)
        {
            bg.speed = speed;
        }
    }
}
