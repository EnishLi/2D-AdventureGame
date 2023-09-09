using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoManager : MonoBehaviour
{
    [Header("ʱ�����")]
    public PlayerAudioEventSO FxEvent;

    public PlayerAudioEventSO BGMEvent;

    [Header("���")]
    //BGM
    public AudioSource BGMSource;

    //��ҹ���
    public AudioSource FXSource;

    private void OnEnable()
    {
        FxEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
    }
    private void OnDisable()
    {
        FxEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
    }

    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }
}
