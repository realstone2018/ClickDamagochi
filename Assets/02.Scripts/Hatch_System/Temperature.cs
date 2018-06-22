using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temperature : Environment {

    public enum State {Cold, Worm, Hot};
    public State state = State.Worm;

    override public void CheckEnvironment(float e)
    {
        if (environment <= 0 || environment >= 100)
        {
            HatchResult.instance.HatchFalseByTemperature();
        }
        else if (state == State.Worm && e <= 30f)
        {
            state = State.Cold;
            ColdState();
        }
        else if(state == State.Worm && e >= 75f)
        {
            state = State.Hot;
            HotState();
        }
        else if (state != State.Worm && e > 30 && e < 75)
        {
            state = State.Worm;
            WormState();
        }
    }

    public void ColdState()
    {
        Debug.Log("Cold");
        environmentLevel.color = new Color(0, 0, 1, 0.8f);
    }

    public void WormState()
    {
        Debug.Log("Worm");
        environmentLevel.color = new Color(1, 1, 1, 0.8f);
    }

    public void HotState()
    {
        Debug.Log("Hot");
        environmentLevel.color = new Color(1, 0, 0, 0.8f);
    }
}