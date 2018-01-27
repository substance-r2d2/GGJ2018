using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{


    bool callUpdateLoop;

    public static ControlManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {

    }

}
