using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //物体检测的变量
    private PhysicsCheck physicsCheck;
    //创建一个控制器变量。控制游戏输入
    public PlayerInputControl inputControl;
    //创建一个2D刚体组件
    private Rigidbody2D rb;
    //创建一个二维方向变量
    public Vector2 inputDirection;
    [Header("基础变量")]
    //创建一个奔跑速度
    private float runSpeed;
    //创建一个走路的速度
    private float walkSpeed => speed/2.5f;
    //创建速度变量
    public float speed;
    //创建一个力的变量
    public float jumpForce;
    private void Awake() {
        runSpeed=speed;
        //获取刚体的组件
        rb=GetComponent<Rigidbody2D>();
        //创建玩家输入的类
        inputControl =new PlayerInputControl();
        //挂载一个跳跃函数
        inputControl.Gameplay.Jump.started += Jump;
        #region 强制走路
        inputControl.Gameplay.Walk.performed += ctx => 
        {
            if(physicsCheck.isGround)
                speed=walkSpeed;
        };
        inputControl.Gameplay.Walk.canceled+=ctx=>
        {
            if(physicsCheck.isGround)
                speed=runSpeed;
        };
        #endregion
        //获取物体检测的组件
        physicsCheck=GetComponent<PhysicsCheck>();

        
        
    }

    //跳跃函数
    private void Jump(InputAction.CallbackContext context)
    {
        if(physicsCheck.isGround)
        {
            //向上方施加一个瞬时力
            rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
        }
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }


    private void Update()
    {
        //获取移动的值
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate() {
        move(); 
    }
    //人物移动函数
    private void move()
    {
        //人物移动
        rb.velocity=new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        //人物翻转
        int faceDir=(int)transform.localScale.x;
        if(inputDirection.x>0)
            faceDir=1;
        if(inputDirection.x<0)
            faceDir=-1;
        transform.localScale=new Vector3(faceDir,1,1);
    }
}
