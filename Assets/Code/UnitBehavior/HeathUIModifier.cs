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
    float modifier;

    bool callUpdateLoop;

    private void OnEnable()
    {
        ActionManager.OnStartFight += OnLevelStarted;
        ActionManager.OnLevelFinished += OnLevelFinished;
        ActionManager.OnCardDeployed += OnCardUsed;
        if (!isPlayer)
        {
            ActionManager.OnSpawnWord += ResetModifer;
            ActionManager.UpdateModifier += UpdateModifier;
        }
    }

    private void OnDisable()
    {
        ActionManager.OnStartFight -= OnLevelStarted;
        ActionManager.OnLevelFinished -= OnLevelFinished;
        ActionManager.OnCardDeployed -= OnCardUsed;
        if (!isPlayer)
        {
            ActionManager.OnSpawnWord -= ResetModifer;
            ActionManager.UpdateModifier -= UpdateModifier;
        }
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
            if (currentSlider.value > 0)
            {
                //Debug.LogError(((currentRate + (currentRate * modifier)) * Time.deltaTime) + " " + (modifier));
                currentSlider.value = currentSlider.value - ((currentRate + (currentRate * modifier)) * Time.deltaTime);
            }
            else
            {
                END_RESULT result = isPlayer ? END_RESULT.LOSE : END_RESULT.WIN;
                if (ActionManager.OnLevelFinished != null)
                    ActionManager.OnLevelFinished(result);

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

    void ResetModifer(string str)
    {
        modifier = 0f;
    }

    void UpdateModifier(float newModifier)
    {
        modifier = newModifier;
    }


}
