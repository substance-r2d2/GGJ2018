using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    List<DialogueData> initialData;

    [SerializeField]
    Transform DialogueParent;

    [SerializeField]
    Transform textBoxParent;

    [SerializeField]
    Text textBox;

    [SerializeField]
    Image characterImage;

    [SerializeField]
    float animTime;

    bool waitForNextDialogue;

    private void Start()
    {
        StartCoroutine(RunDialogue(initialData));
    }

    IEnumerator RunDialogue(List<DialogueData> dialogueData)
    {
        DialogueParent.localScale = Vector3.zero;
        textBoxParent.localScale = Vector3.zero;

        for (int i = 0; i < dialogueData.Count; i++)
        {
            characterImage.sprite = Resources.Load<Sprite>("CharacterImages/" + dialogueData[i].characterAssetName);
            characterImage.SetNativeSize();
            DialogueParent.DOScale(Vector3.one, animTime);
            yield return new WaitForSeconds(animTime);
            for (int j = 0; j < dialogueData[i].dialogues.Count; j++)
            {
                SoundManager.Instance.PlayDialogueOpen();
                textBoxParent.DOScale(Vector3.one, animTime);
                yield return new WaitForSeconds(animTime);

                textBox.text = dialogueData[i].dialogues[j];
                waitForNextDialogue = true;
                ActionManager.OnInput += WaitForNextDialogue;
                while (waitForNextDialogue)
                    yield return null;
                ActionManager.OnInput -= WaitForNextDialogue;
                textBox.text = "";

                textBoxParent.DOScale(Vector3.zero, animTime);
                yield return new WaitForSeconds(animTime);
            }
            textBoxParent.DOScale(Vector3.zero, animTime);
            yield return new WaitForSeconds(animTime);
            DialogueParent.DOScale(Vector3.zero, animTime);
            yield return new WaitForSeconds(animTime);
        }

        LevelManager.Instance.StartFighting();
    }

    void WaitForNextDialogue(INPUT input)
    {
        waitForNextDialogue = false;
    }
}


[System.Serializable]
public class DialogueData
{
    public string characterAssetName;
    public List<string> dialogues;
}
