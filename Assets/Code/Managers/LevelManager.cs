﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelData> levels;

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

}


[System.Serializable]
public class LevelData
{
    public string levelName;

    public int enemyHealth;
    public int playerHeath;

    public int initalEnemyDamage;
    public int initalPlayerDamage;
}