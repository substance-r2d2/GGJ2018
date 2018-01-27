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
        currentLevel = levelData;
        //SpawnMorseBlock();
    }

    void SpawnMorseBlock()
    {
        mainMorseGameObject.transform.DOScale(Vector3.one, 0.5f);

        string secretWord = SecretWordsManager.Instance.GetRandomSecretWord();

        for (int i = 0; i < secretWord.Length; i++)
        {
            morse.Add(MorseToCharacterMapper.charToMorse[secretWord[i]]);
        }

        StartCoroutine(RunTheMorseCode());
    }

    IEnumerator RunTheMorseCode()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < morse.Count; i++)
        {
            for (int j = 0; j < morse[i].Length; j++)
            {
                GameObject go = Instantiate(morsePrefab) as GameObject;
                MorceCodeBlock block = go.GetComponent<MorceCodeBlock>();
                INPUT input = morse[i][j] == '.' ? INPUT.DOT : INPUT.DASH;
                block.Init(morseParent, startpoint, endPoint, currentLevel.scrollTime, new MorseData(input));
                yield return new WaitForSeconds(currentLevel.scrollDelayTime);
            }
        }
    }

}
