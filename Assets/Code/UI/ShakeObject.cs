using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeObject : MonoBehaviour
{

    private void OnEnable()
    {
        ActionManager.OnScore += ShakeIt;
    }

    private void OnDisable()
    {
        ActionManager.OnScore -= ShakeIt;
    }

    void ShakeIt()
    {
        transform.DOShakeScale(0.25f, 0.25f, 5);
    }

}
