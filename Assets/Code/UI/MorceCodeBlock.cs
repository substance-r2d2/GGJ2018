using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MorceCodeBlock : MonoBehaviour
{
    [SerializeField]
    Sprite dashSprite;

    [SerializeField]
    Sprite dotSprite;

    [SerializeField]
    Ease movementEaseType;

    [SerializeField]
    float scaleTime;

    [SerializeField]
    Ease scaleEaseType;

    bool callUpdateLoop;

    public bool shouldDetect;

    Tween movementTween;

    MorseData currentData;

    Rigidbody2D rigidBody;

    private void OnEnable()
    {
        ActionManager.OnInput += OnInput;
    }

    private void OnDisable()
    {
        ActionManager.OnInput -= OnInput;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Init(Transform parent, Transform startTransform, Transform endTransform,float scrollTime,MorseData data)
    {
        currentData = data;

        GetComponent<SpriteRenderer>().sprite = (data.inputType == INPUT.DASH) ? dashSprite : dotSprite;

        transform.SetParent(parent);
        transform.SetAsLastSibling();
        transform.localScale = Vector3.zero;

        transform.position = startTransform.position;

        transform.DOScale(Vector3.one, scaleTime).SetEase(scaleEaseType);

        movementTween = transform.DOMove(endTransform.position, scrollTime).OnComplete(Finished).SetEase(movementEaseType);
        callUpdateLoop = true;
    }

    private void Update()
    {
        if (callUpdateLoop)
        {
            if (movementTween != null)
            {
                if (movementTween.ElapsedPercentage() > 0.65f)
                {
                   // transform.DOScale(Vector3.zero, scaleTime).SetEase(scaleEaseType);
                    movementTween = null;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogError("enter " +collision.gameObject.tag);
        if (collision.gameObject.tag == Globals.DetectionTag)
        {
            //Debug.LogError("enter " +collision.gameObject.tag);
            shouldDetect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Globals.DetectionTag)
        {
            //Debug.LogError("exit " + collision.gameObject.tag);
            shouldDetect = false;
        }
    }

    void OnInput(INPUT currentInput)
    {
        if(shouldDetect)
        {
           // Debug.LogError(currentData.inputType + " " + currentInput);
            if (currentData.inputType == currentInput)
            {
                //Debug.LogError("HIT!");
                transform.DOShakeScale(1f);

                if (ActionManager.OnScore != null)
                    ActionManager.OnScore();
            }
            else
            {
                //Debug.LogError("MISS! "+currentInput);
            }
        }
    }

    void Finished()
    {
        Destroy(this.gameObject);
    }

}
