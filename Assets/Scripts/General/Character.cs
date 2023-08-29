
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")]
    //最大生命值
    public float maxHealth;

    //当前生命值
    public float currentHealth;

    [Header("无敌时间")]
    //无敌时间
    public float invulnerableDuration;

    //计时器
    private float invulnerableCounter;

    //无敌
    public bool invulnerable;

    public UnityEvent<Transform> OnTakedamage;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (invulnerable) 
        {
            invulnerableCounter -=Time.deltaTime;
            if (invulnerableCounter <= 0) 
            {
                invulnerable = false;
            }
        }
    }

    //收到伤害
    public void TakeDamage(Attack attacker) 
    {
        //Debug.Log(attacker.damage);
        if (invulnerable)
            return;
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //执行受伤
            OnTakedamage?.Invoke(attacker.transform);
        }
        else 
        {
            currentHealth = 0;
            //触发死亡
        }
        
    }

    //触发无敌帧
    private void TriggerInvulnerable() 
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration ;
        }
    }
}
