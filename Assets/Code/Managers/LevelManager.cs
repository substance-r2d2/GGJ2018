using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelData> levels;

    int curentLevel = 0;

    public static LevelManager Instance;

    private void OnEnable()
    {
        ActionManager.OnLevelFinished += LevelDone;
        ActionManager.OnSpawnWordFinished += SpawnNextWord;
    }

    private void OnDisable()
    {
        ActionManager.OnLevelFinished -= LevelDone;
        ActionManager.OnSpawnWordFinished -= SpawnNextWord;
    }

    private void Awake()
    {
        Instance = this;
    }

    IEnumerator Start()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return null;
        }

        if(levels.Count > 0)
        {
            if (ActionManager.OnStartLevel != null)
                ActionManager.OnStartLevel(levels[0]);
        }
    }

    public void StartFighting()
    {
        SoundManager.Instance.PlayGunBattle();

        if (ActionManager.OnStartFight != null)
            ActionManager.OnStartFight(levels[curentLevel]);

        Invoke("TriggerWord", Random.Range(levels[curentLevel].minMessageDelay, levels[curentLevel].maxMessageDelay));
    }

    void TriggerWord()
    {
        string word = SecretWordsManager.Instance.GetRandomSecretWord();

        if (ActionManager.OnSpawnWord != null)
            ActionManager.OnSpawnWord(word);

        //Invoke("TriggerWord", Random.Range(levels[curentLevel].minMessageDelay, levels[curentLevel].maxMessageDelay));
    }

    void SpawnNextWord()
    {
        //Debug.LogError("next word Spawned");
        Invoke("TriggerWord", Random.Range(levels[curentLevel].minMessageDelay, levels[curentLevel].maxMessageDelay));
    }

    void LevelDone(END_RESULT result)
    {
        if(result == END_RESULT.WIN)
        {
            ++curentLevel;
        }
    }

}


[System.Serializable]
public class LevelData
{
    public string levelName;

    public float enemyHealth;
    public float playerHeath;

    public float initalEnemyDamage;
    public float initalPlayerDamage;

    public float scrollTime;
    public float scrollDelayTime;
    public float perWordMultiplier;

    public float maxMessageDelay;
    public float minMessageDelay;
}
