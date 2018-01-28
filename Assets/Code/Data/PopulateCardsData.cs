using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopulateCardsData : MonoBehaviour
{
    [SerializeField]
    Transform cardsParent;

    [SerializeField]
    List<PlayerCard> playerCardSlots;

    [SerializeField]
    GameObject inventoryButton;

    bool isOpen;

    private void OnEnable()
    {
        ActionManager.OnCloseInventory += OnInventoryClicked;
        ActionManager.OnCardGained += OnCardGained;
    }

    private void OnDisable()
    {
        ActionManager.OnCloseInventory -= OnInventoryClicked;
        ActionManager.OnCardGained -= OnCardGained;
    }

    private void Awake()
    {
        cardsParent.transform.localScale = new Vector3(0f, 1f, 1f);
    }

    public void OnInventoryClicked()
    {

        if (!isOpen)
        {
            for (int i = 0; i < CardsManager.Instance.playerOwnedCards.Count; i++)
            {
                playerCardSlots[i].gameObject.SetActive(true);
                playerCardSlots[i].Init(CardsManager.Instance.playerOwnedCards[i]);
            }

            for (int i = CardsManager.Instance.playerOwnedCards.Count; i < playerCardSlots.Count; i++)
            {
                playerCardSlots[i].gameObject.SetActive(false);
            }

            cardsParent.DOScaleX(1f, 0.25f).OnComplete(ToggleOpenStatus);
        }
        else
        {
            cardsParent.DOScaleX(0f, 0.25f).OnComplete(ToggleOpenStatus);
        }
    }

    void ToggleOpenStatus()
    {
        isOpen = !isOpen;
    }

    public void OnCardGained(CardData data, bool isPlayer)
    {
        inventoryButton.transform.DOShakeRotation(0.5f);
    }

}
