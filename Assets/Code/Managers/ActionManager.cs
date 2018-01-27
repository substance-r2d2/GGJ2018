using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionManager
{
    public static Action<LevelData> OnStartLevel;

    public static Action OnSpawnWord;

    public static Action<END_RESULT> OnLevelFinished;

    public static Action<INPUT> OnInput;

    public static Action<CardData,bool> OnCardDeployed;
    public static Action<CardData, bool> OnCardGained;

    public static Action OnCloseInventory;

}
