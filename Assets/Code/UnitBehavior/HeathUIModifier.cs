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
    }

    private void OnDisable()
    {
        ActionManager.OnStartLevel -= OnLevelStarted;
        ActionManager.OnLevelFinished -= OnLevelFinished;
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


}
