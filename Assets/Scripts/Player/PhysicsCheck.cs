using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    //胶囊变量
    private CapsuleCollider2D capsuleCollider;

    [Header("检测参数")]
    public bool manual;
    //检测范围变量
    public float checkRaduis;

    //检测对象
    public LayerMask groundLayer;
    public Vector2 bottomOffset;

    //左右检测偏移
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    [Header("状态")]
    //检测地面变量
    public bool isGround;

    //墙壁测量
    public bool touchLeftWall;
    public bool touchRightWall;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2((capsuleCollider.bounds.size.x+capsuleCollider.offset.x)/2,capsuleCollider.bounds.size.y/2);
            leftOffset = new Vector2(-rightOffset.x,rightOffset.y ); 
        }
    }


    private void Update()
    {
        check();
    }

    //经行地面检查函数
    private void check()
    {
        //对地面进行检测
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x*transform.localScale.x,bottomOffset.y), checkRaduis, groundLayer);

        //墙体的判断
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(leftOffset.x,leftOffset.y), checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2( rightOffset.x,rightOffset.y), checkRaduis, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRaduis); 
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);

    }
}
