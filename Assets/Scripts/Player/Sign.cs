using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

public class Sign : MonoBehaviour
{
    PlayerInputControl playerInputControl;

    Animator animator;

    public GameObject signSprite;

    //Sign组件启动判断
    private bool canPress;

    public Transform playerTrans;

    private Iinteractable targetItem;

    private void Awake()
    {
        //子物体一开始为false 所以无法直接GET
        //animator = GetComponentInChildren<Animator>();
        animator = signSprite.GetComponent<Animator>();
        playerInputControl = new PlayerInputControl();
        playerInputControl.Enable();
        
    }
    private void Update()
    {     
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        signSprite.transform.localScale = playerTrans.localScale;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = collision.GetComponent<Iinteractable>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }
    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
        playerInputControl.Gameplay.Confirm.started += OnConfirm;
    }
    private void OnDisable()
    {
        canPress = false;
    }
    /// <summary>
    /// 交互的实现
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnConfirm(InputAction.CallbackContext context)
    {
        if (canPress)
        {
            targetItem.TriggerAction();
            GetComponent<AudioDefination>()?.PlayAudioClip();
        }
    }

    /// <summary>
    /// 不同设备转换不同互动标识
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="change"></param>
    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionStarted)
        {
            var d = ((InputAction)obj).activeControl.device;
            switch (d.device) 
            {
                case Keyboard:
                    animator.Play("KeyBoard");
                    break;
                case DualShockGamepad:
                    animator.Play("Ps");
                    break;
                case XInputController:
                    animator.Play("Xbox");
                    break;
            }
        }
    }
}
