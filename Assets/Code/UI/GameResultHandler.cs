using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultHandler : MonoBehaviour
{
    [SerializeField]
    Text textField;


    private void OnEnable()
    {
        ActionManager.OnLevelFinished += LevelFinished;
    }

    private void OnDisable()
    {
        ActionManager.OnLevelFinished -= LevelFinished;
    }

    void LevelFinished(END_RESULT result)
    {
        transform.localScale = Vector3.one;
        if (result == END_RESULT.LOSE)
        {
            StartCoroutine(StartResultAnim("MISSION FAILED!"));
        }
        else
        {
            StartCoroutine(StartResultAnim("YOU WIN!"));
        }
    }

    IEnumerator StartResultAnim(string message)
    {
        SoundManager.Instance.PlayEndResult();
        textField.text = "";
        for (int i = 0; i < message.Length; i++)
        {
            textField.text += message[i];
            for (int j = 0; j < 3; j++)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

}
