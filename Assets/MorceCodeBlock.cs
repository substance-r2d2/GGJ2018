using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MorceCodeBlock : MonoBehaviour
{
    [SerializeField]
    Ease movementEaseType;

    [SerializeField]
    float scaleTime;

    [SerializeField]
    Ease scaleEaseType;

    bool callUpdateLoop;

    Tween movementTween;

    public void Init(Transform parent, Transform startTransform, Transform endTransform,float scrollTime)
    {
        transform.SetParent(parent);
        transform.SetAsLastSibling();
        transform.localScale = Vector3.zero;

        transform.position = startTransform.position;

        transform.DOScale(Vector3.one, scaleTime).SetEase(scaleEaseType);
        movementTween = transform.DOMove(endTransform.position, scrollTime).OnComplete(Finished).SetEase(movementEaseType);
        callUpdateLoop = true;
    }

    private void Update()
    {
        if (callUpdateLoop)
        {
            if (movementTween != null)
            {
                if (movementTween.ElapsedPercentage() > 0.65f)
                {
                    transform.DOScale(Vector3.zero, scaleTime).SetEase(scaleEaseType);
                    movementTween = null;
                }
            }
        }
    }

    void Finished()
    {
        Destroy(this.gameObject);
    }

}
