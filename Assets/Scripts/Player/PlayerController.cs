using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //����������
    private Rigidbody2D rb;
    //���������������
    public PlayInputControl inputControl;

    //������ײ�����
    public PhysicsCheck physicsCheck;
    //��������ķ���
    public Vector2 inputDirection;
    [Header("��������")]
    //���������ƶ����ٶ�
    public float speed;

    //��Ծ��
    public float jumpForce;



    private void Awake()
    {
        //���������ʵ����
        inputControl = new PlayInputControl();
        //��ȡ�������
        rb = GetComponent<Rigidbody2D>();

        //��ȡ������ײ���
        physicsCheck = GetComponent<PhysicsCheck>();

        //����������Jump����������Jump����
        inputControl.GamePlay.Jump.started += Jump;
    }

    private void OnEnable()
    {
        //����ʵ����Ŀ�ʼ
        inputControl.Enable();
    }
    private void OnDisable()
    {
        //����ʵ����Ľ���
        inputControl.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //��ȡ���������뷽��
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        //�����ƶ�
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //���﷭ת
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            faceDir = 1;
        if (inputDirection.x < 0)
            faceDir = -1;
        transform.localScale = new Vector3(faceDir, 1, 1);

    }
    //��Ծ�ķ���
    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        //ʩ��һ����Ծ����
        //����Ծ��������
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

    }
}
