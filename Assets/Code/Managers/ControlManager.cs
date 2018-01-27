using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField]
    float dotPressTime;

    [SerializeField]
    float invalidateTime;

    bool callUpdateLoop;

    public static ControlManager Instance;

    float keyDownTime = -1f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            keyDownTime = Time.time;
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (keyDownTime > 0f)
            {
                float totalTime = Time.time - keyDownTime;

                INPUT currentInput = INPUT.WRONG;
                if(totalTime <= dotPressTime)
                {
                    currentInput = INPUT.DOT;
                }
                else if(totalTime > dotPressTime && totalTime <= invalidateTime)
                {
                    currentInput = INPUT.DASH;
                }

                //Debug.LogError("trigger " + currentInput + " " + totalTime);
                if (ActionManager.OnInput != null)
                    ActionManager.OnInput(currentInput);

                keyDownTime = -1f;
            }
        }
    }

}
