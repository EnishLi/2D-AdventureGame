using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //创建刚体类
    private Rigidbody2D rb;
    //创建按键输入的类
    public PlayInputControl inputControl;

    //物体碰撞检测类
    public PhysicsCheck physicsCheck;
    //创建输入的方向
    public Vector2 inputDirection;
    [Header("基本参数")]
    //创建人物移动的速度
    public float speed;

    //跳跃力
    public float jumpForce;



    private void Awake()
    {
        //创建输入的实体类
        inputControl = new PlayInputControl();
        //获取刚体组件
        rb = GetComponent<Rigidbody2D>();

        //获取物理碰撞组件
        physicsCheck = GetComponent<PhysicsCheck>();

        //关联函数将Jump按键关联到Jump函数
        inputControl.GamePlay.Jump.started += Jump;
    }

    private void OnEnable()
    {
        //输入实体类的开始
        inputControl.Enable();
    }
    private void OnDisable()
    {
        //输入实体类的结束
        inputControl.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //获取按键的输入方向
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        //人物移动
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //人物翻转
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir, 1, 1);

    }
    //跳跃的方法
    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        //施加一个跳跃的力
        //对跳跃进行限制
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

    }
}
