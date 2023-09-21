using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStateBar playerStateBar;

    [Header("ÊÂ¼þ¼àÌý")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO SceneLoadEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRised += OnHealthEvent;
        SceneLoadEvent.LoadREquestEvent += OnLoadEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRised -= OnHealthEvent;
        SceneLoadEvent.LoadREquestEvent -= OnLoadEvent;
    }

    private void OnLoadEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        if(arg0.SceneType == SceneType.Menu)
            playerStateBar.gameObject.SetActive(false);
        else
            playerStateBar.gameObject.SetActive(true);
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playerStateBar.OnHealthChange(persentage);
        playerStateBar.OnPowerChange(character);
    }
}
