using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //����
    protected Rigidbody2D rb;

    //����
    [HideInInspector]public Animator animator;

    //��ײ���
    [HideInInspector]public PhysicsCheck physicsCheck;

    [Header("��������")]
    //�ٶ�
    public float normalSpeed;

    //����ٶ�
    public float chaseSpeed;

    //��ǰ�ٶ�
    [HideInInspector] public float currentSpeed;

    //����
    public Vector3 faceDir;

    //������
    public float hurtForce;

    //������
    public Transform attcker;
    [Header("��ʱ��")]
    //�Ⱥ�ʱ��
    public float waitTime;

    //�ȴ���ʱ��
    public float waitTimeCounter;

    [Header("״̬")]

    //�ȴ�״̬
    public bool wait;

    //����״̬
    public bool isHurt;

    //����״̬
    public bool isDead;

    //��ǰ״̬
    private BaseState currentState;

    //Ѳ��״̬
    protected BaseState patrolState;

    //���״̬
    protected BaseState chaseState;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;

    }
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.onEnter(this);
    }

    private void Update()
    {
        
        faceDir = new Vector3(-transform.localScale.x, 0, 0);      
        currentState.Logicupdate();
        TimeCounter();
    }
    private void FixedUpdate()
    {
        

        if(!isHurt && !isDead && !wait)
            Move();
        
        currentState.PhysicsUpdate();
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    public virtual void  Move()
    {

        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

   /// <summary>
   /// ��ʱ��
   /// </summary>
    public void TimeCounter()
    {
        
        if (wait)
        { 
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            { 
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }
    public void OnTakeDamage(Transform attackTrans)
    {
        attcker = attackTrans;

        //ת��
        if (attackTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1,1,1);
        if(attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);

        //���˱�����
        isHurt = true;
        animator.SetTrigger("Hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x,0).normalized;
        rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);

        //����Э��
        StartCoroutine(OnHurt(dir));
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void OnDie()
    {
        gameObject.layer = 2;
        animator.SetBool("Dead",true);
        isDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }
}
