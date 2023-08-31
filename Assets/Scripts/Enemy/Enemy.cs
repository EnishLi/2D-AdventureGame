using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //����
    protected Rigidbody2D rb;

    //����
    protected Animator animator;

    [Header("��������")]
    //�ٶ�
    public float normalSpeed;

    //����ٶ�
    public float chaseSpeed;

    //��ǰ�ٶ�
    public float currentSpeed;

    //����
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
