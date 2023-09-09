using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator),typeof(PhysicsCheck))]
public class Enemy : MonoBehaviour
{
    //刚体
    [HideInInspector]public Rigidbody2D rb;

    //动画
    [HideInInspector]public Animator animator;

    //碰撞检测
    [HideInInspector]public PhysicsCheck physicsCheck;

    [Header("基本参数")]
    //速度
    public float normalSpeed;

    //冲刺速度
    public float chaseSpeed;

    //当前速度
    [HideInInspector] public float currentSpeed;

    //方向
    public Vector3 faceDir;

    //方向力
    public float hurtForce;

    //攻击者
    public Transform attcker;

    //位置
    public Vector3 spawnPoint;

    [Header("检测")]

    //检测范围
    public Vector2 centerOffset;

    //检测尺寸
    public Vector2 checkSize;

    //检测距离
    public float checkDistance;
    [Header("计时器")]
    //等候时间
    public float waitTime;

    //等待计时器
    public float waitTimeCounter;

    //检测图层
    public LayerMask attackLayer;

    //丢失时间
    public float lostTime;

    //丢失计数器
    public float lostTimeCounter;

    [Header("状态")]

    //等待状态
    public bool wait;

    //受伤状态
    public bool isHurt;

    //死亡状态
    public bool isDead;

    //当前状态
    private BaseState currentState;

    //巡逻状态
    protected BaseState patrolState;

    //冲锋状态
    protected BaseState chaseState;

    //技能状态
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
    /// 移动
    /// </summary>
    public virtual void  Move()
    {
        //优化蜗牛的移动
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PreMove") && !animator.GetCurrentAnimatorStateInfo(0).IsName("SnailHideRecover"))
            rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

   /// <summary>
   /// 计时器
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
    /// 发现敌人
    /// </summary>
    /// <returns></returns>
    public virtual bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position+(Vector3)centerOffset,checkSize,0,faceDir,checkDistance,attackLayer);
    }
    /// <summary>
    /// 切换状态
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
    /// 更新自己位置
    /// </summary>
    /// <returns></returns>
    public virtual Vector3 GetNewPoint()
    { 
        return transform.position;
    }
    #region 事件
    /// <summary>
    /// 受到攻击被击退
    /// </summary>
    /// <param name="attackTrans"></param>
    public void OnTakeDamage(Transform attackTrans)
    {
        attcker = attackTrans;

        //转身
        if (attackTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1,1,1);
        if(attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);

        //受伤被击退
        isHurt = true;
        animator.SetTrigger("Hurt");
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x,0).normalized;
        rb.AddForce(dir*hurtForce,ForceMode2D.Impulse);
        rb.velocity = new Vector2(0,rb.velocity.y);

        //启动协程
        StartCoroutine(OnHurt(dir));
    }

    /// <summary>
    /// 怪物击退
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
    /// 怪物死亡
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
