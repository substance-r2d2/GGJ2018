using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public float minScoreForCard;

    float maxScore;
    float currentScore;

    private void OnEnable()
    {
        ActionManager.OnSpawnWord += SpawnMorseBlock;
        ActionManager.OnScore += OnScore;
        ActionManager.OnSpawnWordFinished += OnSpawnWordFinished;
    }

    private void OnDisable()
    {
        ActionManager.OnSpawnWord -= SpawnMorseBlock;
        ActionManager.OnScore -= OnScore;
        ActionManager.OnSpawnWordFinished -= OnSpawnWordFinished;
    }

    void SpawnMorseBlock(string secretWord)
    {
        List<string> morse = new List<string>();
        for (int i = 0; i < secretWord.Length; i++)
        {
            morse.Add(MorseToCharacterMapper.charToMorse[secretWord[i]]);
        }

        for (int i = 0; i < morse.Count; i++)
        {
            for (int j = 0; j < morse[i].Length; j++)
            {
                ++maxScore;
            }
        }
    }

    void OnScore()
    {
        ++currentScore;
    }

    void OnSpawnWordFinished()
    {
        float score = ((currentScore * 100f) / maxScore);

        float modifier = 0f;
        if(score > 99.9f)
        {
            modifier = 0.5f;
        }
        else if(score < 99.9f && score > 85)
        {
            modifier = 0.4f;
        }
        else if(score < 85f && score > 65f)
        {
            modifier = 0.3f;
        }
        else if(score < 65f && score > 50f)
        {
            modifier = 0.2f;
        }

        Debug.LogError("score " + score + " " + modifier);
        if (ActionManager.UpdateModifier != null)
            ActionManager.UpdateModifier(modifier);

        if (score >= minScoreForCard)
        {
            CardsManager.Instance.GetRandomCard(true);
        }
    }


}
