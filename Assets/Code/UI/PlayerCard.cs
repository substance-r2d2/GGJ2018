﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    CardData currentCardData;

    CardsOwned ownedData;

    [SerializeField]
    Image cardImage;

    [SerializeField]
    Text CardName;

    [SerializeField]
    Text count;

    public void Init(CardsOwned card)
    {
        ownedData = card;
        currentCardData = card.card;
        count.text = card.cardCount.ToString();
        CardName.text = card.card.cardName;
        cardImage.sprite = Resources.Load<Sprite>("CardSprites/" + card.card.cardImage);
    }

    public void OnCardClicked()
    {
        if(ownedData.cardCount > 0)
        {
            if(ActionManager.OnCardDeployed != null)
            {
                ActionManager.OnCardDeployed(currentCardData, true);

                if (ActionManager.OnCloseInventory != null)
                    ActionManager.OnCloseInventory();
            }
        }
    }

}
