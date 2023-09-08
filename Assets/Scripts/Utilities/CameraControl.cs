using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
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
    }
    private void OnDisable()
    {
        camerShakeEvent.OnEventRaised -= OnCamerShakeEvent;
    }

    private void OnCamerShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    /// <summary>
    /// 场景切换之后更改
    /// </summary>
    private void Start()
    {
        GetNewCamerBounds();
    }
    private void GetNewCamerBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null)
            return;
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        confiner2D.InvalidateCache();
    }
}
