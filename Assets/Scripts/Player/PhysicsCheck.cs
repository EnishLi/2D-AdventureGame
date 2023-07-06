using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    //检测范围变量
    public float checkRaduis;

    //检测对象
    public LayerMask groundLayer;
    public Vector2 bottomOffset;

    [Header("检测范围")]
    //检测地面变量
    public bool isGround;

    //�ŵ׵�λ�Ʋ�ֵ
    
    private void Update()
    {
        check();
    }

    private void check()
    {
        //������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, checkRaduis, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRaduis);
    }
}
