using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShowCardOnDeploy : MonoBehaviour
{
    [SerializeField]
    Text cardName;

    [SerializeField]
    Text cardDescription;

    [SerializeField]
    Image cardImage;

    private void OnEnable()
    {
        ActionManager.OnCardDeployed += CardDeployed;
    }

    private void OnDisable()
    {
        ActionManager.OnCardDeployed -= CardDeployed;
    }

    void CardDeployed(CardData data,bool isPlayer)
    {
        StartCoroutine(RunCardDeploy(data));
    }

    IEnumerator RunCardDeploy(CardData card)
    {
        cardName.text = card.cardName;
        cardDescription.text = card.description;
        cardImage.sprite = Resources.Load<Sprite>("CardSprites/" + card.cardImage);

        transform.DOScale(Vector3.one, 0.5f);
        transform.DOPunchRotation(new Vector3(0f, 270f, 0f), 1f);
        yield return new WaitForSeconds(2f);
        transform.DOScale(Vector3.zero, 0.5f);
        transform.DOPunchRotation(new Vector3(0f, 270f, 0f), 1f);
    }

}
