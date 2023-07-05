using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    //��ⷶΧ
    public float checkRaduis;

    //���㼶
    public LayerMask groundLayer;

    //�����ж�
    public bool isGround;

    //�ŵ׵�λ�Ʋ�ֵ
    public Vector2 bottomOffset;
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
