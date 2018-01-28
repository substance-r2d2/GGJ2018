using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeCamera : MonoBehaviour
{
    private void OnEnable()
    {
        ActionManager.OnCardDeployed += CardDeployed;
    }

    private void OnDisable()
    {
        ActionManager.OnCardDeployed -= CardDeployed;
    }

    void CardDeployed(CardData data, bool isPlayer)
    {
        if(data.cardType == CARD_TYPE.ATTACK)
            Camera.main.DOShakePosition(0.75f);
    }

    [ContextMenu("test shake")]
    public void testshake()
    {
        Camera.main.DOShakePosition(0.5f);
    }

}
