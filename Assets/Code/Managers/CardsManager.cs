using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField]
    List<CardData> cards;

    public static CardsManager Instance;

    public List<CardsOwned> playerOwnedCards;
    public List<CardsOwned> enemyOwnedCards;

    private void OnEnable()
    {
        ActionManager.OnCardGained += OnCardGained;
        ActionManager.OnCardDeployed += OnCardDeployed;
    }

    private void OnDisable()
    {
        ActionManager.OnCardGained -= OnCardGained;
        ActionManager.OnCardDeployed -= OnCardDeployed;
    }

    private void Awake()
    {
        Instance = this;
        playerOwnedCards = new List<CardsOwned>();

    }

    void OnCardGained(CardData card, bool isPlayer)
    {
        if(isPlayer)
        {
            if (card.isAutoTrigger)
            {
                if(ActionManager.OnCardDeployed != null)
                {
                    ActionManager.OnCardDeployed(card, isPlayer);
                }
            }
            else
            {
                CardsOwned cardOwned = playerOwnedCards.Find(x => x.card == card);
                if (cardOwned == null)
                {
                    playerOwnedCards.Add(new CardsOwned(card, 1));
                }
                else
                    cardOwned.UpdateCardCount(1);
            }
        }
    }

    void OnCardDeployed(CardData card, bool isPlayer)
    {
        if(isPlayer)
        {
            CardsOwned cardOwned = playerOwnedCards.Find(x => x.card == card);
            if(cardOwned != null)
                cardOwned.UpdateCardCount(-1);
        }
    }

    public void GetRandomCard(bool isPlayer)
    {
        if(ActionManager.OnCardGained != null)
            ActionManager.OnCardGained(cards[Random.Range(0, cards.Count)], isPlayer);
    }
}

[System.Serializable]
public class CardData
{
    public string cardName;
    public string cardImage;
    public CARD_TYPE cardType;
    public float modifier;
    public bool isAutoTrigger;
    public string description;
}

public class CardsOwned
{
    public CardData card { get; set; }
    public int cardCount { get; set; }

    public CardsOwned(CardData newCard)
    {
        card = newCard;
        cardCount = 0;
    }

    public CardsOwned(CardData newCard, int initalCount)
    {
        card = newCard;
        cardCount = initalCount;
    }

    public void UpdateCardCount(int val)
    {
        cardCount += val;
    }
}

public enum CARD_TYPE
{
    ATTACK,
    HP
}
