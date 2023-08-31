
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    //�������ֵ
    public float maxHealth;

    //��ǰ����ֵ
    public float currentHealth;

    [Header("�����޵�")]
    //�޵�ʱ��
    public float invulnerableDuration;

    //��ʱ��
    private float invulnerableCounter;

    //�޵�
    public bool invulnerable;

    //�˺��¼�
    public UnityEvent<Transform> OnTakedamage;

    //�����¼�
    public UnityEvent OnDie;

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
    //�����޵�֡
    private void TriggerInvulnerable() 
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration ;
        }
    }
    //�ܵ��˺�
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
            OnDie?.Invoke();
        }

    }
}
