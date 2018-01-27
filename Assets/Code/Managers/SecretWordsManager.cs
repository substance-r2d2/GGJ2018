using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWordsManager : MonoBehaviour
{
    public List<string> secretWords;

    public static SecretWordsManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public string GetRandomSecretWord()
    {
        return secretWords[Random.Range(0, secretWords.Count)];
    }
}
