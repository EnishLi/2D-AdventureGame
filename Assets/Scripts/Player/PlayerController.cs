using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //获取胶囊碰撞体组件
    private CapsuleCollider2D coll;
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
    private Vector2 originalOffset;
    private Vector2 originalSize;

    //创建下蹲判断变量
    public bool isGrouch; 
    private void Awake() {

        runSpeed=speed;
        //获取刚体的组件
        rb=GetComponent<Rigidbody2D>();
        //获取物体检测的组件
        physicsCheck=GetComponent<PhysicsCheck>();
        //获取碰撞体组件
        coll=GetComponent<CapsuleCollider2D>();
        originalOffset=coll.offset;
        originalSize=coll.size;


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
   /* //测试
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }*/


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
        if(!isGrouch)
        {
        //人物移动
        rb.velocity=new Vector2(inputDirection.x*speed*Time.deltaTime,rb.velocity.y);
        }
    
        //人物翻转
        int faceDir=(int)transform.localScale.x;
        if(inputDirection.x>0)
            faceDir=1;
        if(inputDirection.x<0)
            faceDir=-1;
        transform.localScale=new Vector3(faceDir,1,1);
        //下蹲
        isGrouch=inputDirection.y<-0.01f && physicsCheck.isGround; 
        if(isGrouch)
        {
            //修改碰撞体大小和位移
            coll.offset=new Vector2(-0.05f,0.85f);
            coll.size=new Vector2(0.7f,1.7f);
        }
        else
        {
            //还原碰撞体大小和体积
            coll.size=originalSize;
            coll.offset=originalOffset;
        }

    }
}
