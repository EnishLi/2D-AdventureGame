using Microsoft.CSharp.RuntimeBinder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //创建2D刚体变量
    public Rigidbody2D rb;
    //创建动画变量
    public Animator anim;
    private PhysicsCheck physicsCheck;
    // Start is called before the first frame update
    void Start()
    {
        //获取2D刚体组件
        rb=GetComponent<Rigidbody2D>();
        //获取动画组件
        anim=GetComponent<Animator>();
        //获取地面检测脚本组件
        physicsCheck=GetComponent<PhysicsCheck>();

    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }
    void SetAnimation()
    {
        anim.SetFloat("velocityX",Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY",rb.velocity.y);
        anim.SetBool("isGround",physicsCheck.isGround);
    }
}