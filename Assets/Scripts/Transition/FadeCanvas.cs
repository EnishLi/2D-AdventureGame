using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;
    [Header("Ê±¼ä¼àÌý")]
    public FadeEventSO fadeEvent;

    private void OnFadeEvent(Color target,float duration)
    {
        fadeImage.DOBlendableColor(target,duration);
    }
    private void OnEnable()
    {
        fadeEvent.OnEventRaised += OnFadeEvent;
    }
    private void OnDisable()
    {
        fadeEvent.OnEventRaised -= OnFadeEvent;
    }

    private void OnFadeEvent(Color target, float duration, bool fadeIn)
    {
        fadeImage.DOBlendableColor(target,duration);
    }
}
