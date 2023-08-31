using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //创建胶囊碰撞体组件变量
    private CapsuleCollider2D coll;

    //创建物体检测的变量
    private PhysicsCheck physicsCheck;

    //创建一个控制器变量。控制游戏输入
    public PlayerInputControl inputControl;

    //创建一个2D刚体组件变量
    private Rigidbody2D rb;

    //创建动画组件变量
    private PlayerAnimation playerAnimation;

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
   
    //人物范围
    private Vector2 originalOffset;
    private Vector2 originalSize;

    //伤害作用力的变量
    public float hurtForce;

    [Header("材质")]
    public PhysicsMaterial2D normal;

    public PhysicsMaterial2D wall;

    [Header("状态")]
    //创建下蹲状态
    public bool isGrouch;

    //玩家受伤状态
    public bool isHurt;

    //玩家状态
    public bool isDead;

    //攻击状态
    public bool isAttack;

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

        //获取角色动画组件
        playerAnimation=GetComponent<PlayerAnimation>();

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

        // 攻击
        inputControl.Gameplay.Attack.started += PlayerAttack;



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
        CheckState();
    }
    private void FixedUpdate() {
        if(!isHurt && !isAttack)
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
    //攻击的方法
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayAttack();
        isAttack = true;
    }
    //材质切换
    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
        
        gameObject.layer = isDead ? LayerMask.NameToLayer("Enemy") : LayerMask.NameToLayer("Player");

    }
    #region unity事件
    public void GetHurt(Transform attacker)
    { 
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x),0).normalized;
        rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);
    }
    public void PlayerDead()
    { 
        isDead = true;
        inputControl.Gameplay.Disable();
    }
    
    #endregion
}
