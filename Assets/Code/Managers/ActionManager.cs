using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionManager
{
    public static Action<LevelData> OnStartLevel;
    public static Action<LevelData> OnStartFight;

    public static Action<string> OnSpawnWord;
    public static Action OnSpawnWordFinished;

    public static Action<END_RESULT> OnLevelFinished;

    public static Action<INPUT> OnInput;

    public static Action<CardData,bool> OnCardDeployed;
    public static Action<CardData, bool> OnCardGained;

    public static Action OnCloseInventory;

    public static Action OnScore;
    public static Action<float> UpdateModifier;
}
