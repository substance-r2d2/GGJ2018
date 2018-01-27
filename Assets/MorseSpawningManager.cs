using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorseSpawningManager : MonoBehaviour
{
    public static MorseSpawningManager Instance;

    [SerializeField]
    GameObject morsePrefab;

    public Transform morseParent;

    public Transform startpoint;

    public Transform endPoint;

    LevelData currentLevel;

    private void OnEnable()
    {
        ActionManager.OnStartLevel += OnLevelStart;
    }

    private void OnDisable()
    {
        ActionManager.OnStartLevel -= OnLevelStart;
    }

    private void Awake()
    {
        Instance = this;
    }

    void OnLevelStart(LevelData levelData)
    {
        currentLevel = levelData;
        SpawnMorseBlock();
        InvokeRepeating("SpawnMorseBlock", levelData.scrollDelayTime, levelData.scrollDelayTime);
    }

    void SpawnMorseBlock()
    {
        GameObject go = Instantiate(morsePrefab) as GameObject;
        MorceCodeBlock block = go.GetComponent<MorceCodeBlock>();
        block.Init(morseParent, startpoint, endPoint, currentLevel.scrollTime);
    }

}
