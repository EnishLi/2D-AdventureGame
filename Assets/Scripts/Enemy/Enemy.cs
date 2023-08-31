using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //刚体
    protected Rigidbody2D rb;

    //动画
    protected Animator animator;

    [Header("基本参数")]
    //速度
    public float normalSpeed;

    //冲刺速度
    public float chaseSpeed;

    //当前速度
    public float currentSpeed;

    //方向
    public Vector3 faceDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        Debug.Log("aaa");
    }
    private void FixedUpdate()
    {
        Move();
    }
    public virtual void  Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }
}
