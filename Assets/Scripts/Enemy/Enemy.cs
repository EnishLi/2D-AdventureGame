using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(PhysicsCheck))]
public class Enemy : MonoBehaviour
{
    //����
    [HideInInspector]public Rigidbody2D rb;

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

    //λ��
    public Vector3 spawnPoint;

    [Header("���")]

    //��ⷶΧ
    public Vector2 centerOffset;

    //���ߴ�
    public Vector2 checkSize;

    //������
    public float checkDistance;
    [Header("��ʱ��")]
    //�Ⱥ�ʱ��
    public float waitTime;

    //�ȴ���ʱ��
    public float waitTimeCounter;

    //���ͼ��
    public LayerMask attackLayer;

    //��ʧʱ��
    public float lostTime;

    //��ʧ������
    public float lostTimeCounter;

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

    //����״̬
    protected BaseState skillState;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        //waitTimeCounter = waitTime;
        spawnPoint = transform.position;

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
        currentState.PhysicsUpdate();
        if (!isHurt && !isDead && !wait)
            Move();      
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
        //�Ż���ţ���ƶ�
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PreMove") && !animator.GetCurrentAnimatorStateInfo(0).IsName("SnailHideRecover"))
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
        if (!FoundPlayer()&& lostTimeCounter>0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        else if (FoundPlayer()) 
        {
            lostTimeCounter = lostTime;
        }
    }
    /// <summary>
    /// ���ֵ���
    /// </summary>
    /// <returns></returns>
    public virtual bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position+(Vector3)centerOffset,checkSize,0,faceDir,checkDistance,attackLayer);
    }
    /// <summary>
    /// �л�״̬
    /// </summary>
    /// <param name="state"></param>
    public void SwitchState(NpcState state)
    {
        var newState = state switch
        {
            NpcState.Patrol => patrolState,
            NpcState.Chase => chaseState,
            NpcState.Skill => skillState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.onEnter(this);
    }
    /// <summary>
    /// �����Լ�λ��
    /// </summary>
    /// <returns></returns>
    public virtual Vector3 GetNewPoint()
    { 
        return transform.position;
    }
    #region �¼�
    /// <summary>
    /// �ܵ�����������
    /// </summary>
    /// <param name="attackTrans"></param>
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
        rb.velocity = new Vector2(0,rb.velocity.y);

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
    #endregion
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance*-transform.localScale.x,0), 0.2f);
    }
}
