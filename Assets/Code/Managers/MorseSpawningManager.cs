using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MorseSpawningManager : MonoBehaviour
{
    public static MorseSpawningManager Instance;

    [SerializeField]
    GameObject morsePrefab;

    [SerializeField]
    GameObject mainMorseGameObject;

    public Transform morseParent;

    public Transform startpoint;

    public Transform endPoint;

    LevelData currentLevel;
    List<string> morse = new List<string>();

    int currentWord = 0;
    bool isCodeRunning = false;

    [SerializeField]
    GameObject ButtonObj;

    private void OnEnable()
    {
        ActionManager.OnStartLevel += OnLevelStart;
        ActionManager.OnSpawnWord += SpawnMorseBlock;
    }

    private void OnDisable()
    {
        ActionManager.OnStartLevel -= OnLevelStart;
        ActionManager.OnSpawnWord -= SpawnMorseBlock;
    }

    private void Awake()
    {
        Instance = this;
        mainMorseGameObject.transform.localScale = Vector3.zero;
    }

    void OnLevelStart(LevelData levelData)
    {
        currentWord = 0;
        currentLevel = levelData;
        //SpawnMorseBlock();
    }

    void SpawnMorseBlock(string secretWord)
    {
        for (int i = 0; i < secretWord.Length; i++)
        {
            morse.Add(MorseToCharacterMapper.charToMorse[secretWord[i]]);
        }

        ButtonObj.transform.DOPunchScale(Vector3.one, 1f);
        SoundManager.Instance.PlayMessageReceived();
    }

    public void OnMessageClicked()
    {
        if(morse.Count > 0 && !isCodeRunning)
            StartCoroutine(RunTheMorseCode());
    }

    IEnumerator RunTheMorseCode()
    {
        isCodeRunning = true;
        mainMorseGameObject.transform.DOScale(Vector3.one, 0.3f);
        yield return new WaitForSeconds(0.35f);

        for (int i = 0; i < morse.Count; i++)
        {
            for (int j = 0; j < morse[i].Length; j++)
            {
                GameObject go = Instantiate(morsePrefab) as GameObject;
                MorceCodeBlock block = go.GetComponent<MorceCodeBlock>();
                INPUT input = morse[i][j] == '.' ? INPUT.DOT : INPUT.DASH;
                //float scrollTime = currentLevel.scrollTime - (currentLevel.scrollTime * currentLevel.)
                block.Init(morseParent, startpoint, endPoint, currentLevel.scrollTime, new MorseData(input));

                if (input == INPUT.DASH)
                    SoundManager.Instance.PlayDash();
                else
                    SoundManager.Instance.PlayDot();

                yield return new WaitForSeconds(currentLevel.scrollDelayTime);
            }
        }

        Invoke("TriggerEndWordEvent", currentLevel.scrollTime);

        morse.Clear();
        isCodeRunning = false;
    }

    void TriggerEndWordEvent()
    {
        mainMorseGameObject.transform.DOScale(Vector3.zero, 0.3f);

        if (ActionManager.OnSpawnWordFinished != null)
            ActionManager.OnSpawnWordFinished();
    }

}
