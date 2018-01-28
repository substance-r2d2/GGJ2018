using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource source1;

    [SerializeField]
    AudioSource BGSource;

    [SerializeField]
    AudioSource gunBattleSource;

    [SerializeField]
    AudioClip dot;

    [SerializeField]
    AudioClip dash;

    [SerializeField]
    AudioClip typeWriter;

    [SerializeField]
    AudioClip DialogueOpen;

    [SerializeField]
    AudioClip MessageReceived;

    [SerializeField]
    AudioClip gunBattle;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayDot()
    {
        if (!source1.isPlaying)
        {
            source1.clip = dot;
            source1.Play();
        }
    }

    public void PlayDash()
    {
        if (!source1.isPlaying)
        {
            source1.clip = dash;
            source1.Play();
        }
    }

    public void PlayEndResult()
    {
        gunBattleSource = null;
        BGSource.Stop();
        source1.Stop();
        source1.clip = typeWriter;
        source1.Play();
    }

    public void PlayDialogueOpen()
    {
        source1.clip = DialogueOpen;
        source1.Play();
    }

    public void PlayMessageReceived()
    {
        source1.Stop();
        source1.clip = MessageReceived;
        source1.Play();
    }

    public void PlayGunBattle()
    {
        StartCoroutine("PlayGunBattleRoutine");
    }

    IEnumerator PlayGunBattleRoutine()
    {
        while(gunBattleSource != null)
        {
            gunBattleSource.clip = gunBattle;
            gunBattleSource.Play();

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

}
