
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    //�������ֵ
    public float maxHealth;

    //��ǰ����ֵ
    public float currentHealth;

    [Header("�޵�ʱ��")]
    //�޵�ʱ��
    public float invulnerableDuration;

    //��ʱ��
    private float invulnerableCounter;

    //�޵�
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

    //�յ��˺�
    public void TakeDamage(Attack attacker) 
    {
        //Debug.Log(attacker.damage);
        if (invulnerable)
            return;
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //ִ������
            OnTakedamage?.Invoke(attacker.transform);
        }
        else 
        {
            currentHealth = 0;
            //��������
        }
        
    }

    //�����޵�֡
    private void TriggerInvulnerable() 
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration ;
        }
    }
}