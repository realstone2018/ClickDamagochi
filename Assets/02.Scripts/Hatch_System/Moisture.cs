using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moisture : Environment
{
    public GrowingMushroom growingMushroom;


    override public void CheckEnvironment(float e)
    {
        if (e <= 60)
        {
            growingMushroom.growthRate = 0;
        }
        else 
            growingMushroom.growthRate = 1 + (e * 0.02f);    
    }

}
