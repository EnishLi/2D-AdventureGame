using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    //ºÏ≤‚∑∂Œß
    public float checkRaduis;

    //ºÏ≤‚≤„º∂
    public LayerMask groundLayer;

    //µÿ√Ê≈–∂œ
    public bool isGround;

    //Ω≈µ◊µƒŒª“∆≤Ó÷µ
    public Vector2 bottomOffset;
    private void Update()
    {
        check();
    }

    private void check()
    {
        //ºÏ≤‚µÿ√Ê
        isGround = Physics2D.OverlapCircle((Vector2)transform.position+bottomOffset, checkRaduis, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRaduis);
    }
}
