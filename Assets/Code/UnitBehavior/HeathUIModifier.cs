using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathUIModifier : MonoBehaviour
{
    [SerializeField]
    bool isPlayer; 

    Slider currentSlider;

    float currentRate;

    bool callUpdateLoop;

    private void OnEnable()
    {
        ActionManager.OnStartLevel += OnLevelStarted;
        ActionManager.OnLevelFinished += OnLevelFinished;
        ActionManager.OnCardDeployed += OnCardUsed;
    }

    private void OnDisable()
    {
        ActionManager.OnStartLevel -= OnLevelStarted;
        ActionManager.OnLevelFinished -= OnLevelFinished;
        ActionManager.OnCardDeployed -= OnCardUsed;
    }

    // Use this for initialization
    void Start ()
    {
        currentSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(callUpdateLoop)
        {
            if(currentSlider.value > 0)
                currentSlider.value = currentSlider.value - (currentRate * Time.deltaTime);
            else
            {
                END_RESULT result = isPlayer ? END_RESULT.LOSE : END_RESULT.WIN;
                if (ActionManager.OnLevelFinished != null)
                    ActionManager.OnLevelFinished(END_RESULT.LOSE);

                callUpdateLoop = false;
            }
        }
	}

    void OnLevelStarted(LevelData level)
    {
        currentSlider.minValue = 0f;
        currentSlider.maxValue = isPlayer ? level.playerHeath : level.enemyHealth;
        currentSlider.value = isPlayer ? level.playerHeath : level.enemyHealth;
        currentRate = isPlayer ? level.initalEnemyDamage : level.initalPlayerDamage;
        callUpdateLoop = true;
    }

    void OnLevelFinished(END_RESULT result)
    {
        callUpdateLoop = false;
    }

    void OnCardUsed(CardData cardData,bool playerOwned)
    {
        switch(cardData.cardType)
        {
            case CARD_TYPE.ATTACK:
                if(!isPlayer && playerOwned)
                {
                    float damage = (cardData.modifier * currentSlider.value) / 100f;
                    currentSlider.value -= damage; 
                }
                break;

            case CARD_TYPE.HP:
                if(isPlayer && playerOwned && cardData.modifier > 0f)
                {
                    float increased = (cardData.modifier * currentSlider.value) / 100f;
                    currentSlider.value += increased;
                }
                if(!isPlayer && playerOwned && cardData.modifier < 0f)
                {
                    float decreased = (cardData.modifier * currentSlider.value) / 100f;
                    currentSlider.value += decreased;
                }
                break;
        }
    }


}
