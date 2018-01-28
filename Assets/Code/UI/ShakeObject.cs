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
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(Vector3.one * 0.85f, 0.1f));
        seq.Append(transform.DOScale(Vector3.one, 0.1f));
    }

}
