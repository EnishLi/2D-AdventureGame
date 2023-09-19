using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    [Header("ʱ�����")]
    public VoidEventSO afterSceneLoadedEvent;
    private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;

    public VoidEventSO camerShakeEvent;
    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>(); 
    }
    private void OnEnable()
    {
        camerShakeEvent.OnEventRaised += OnCamerShakeEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        camerShakeEvent.OnEventRaised -= OnCamerShakeEvent;
        afterSceneLoadedEvent.OnEventRaised = OnAfterSceneLoadedEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        GetNewCamerBounds();
    }

    private void OnCamerShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    /// <summary>
    /// �����л�֮�����
    /// </summary>
    /*private void Start()
    {
        GetNewCamerBounds();
    }*/
    private void GetNewCamerBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
            return;
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        confiner2D.InvalidateCache();
    }
}
