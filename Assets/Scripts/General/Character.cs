
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("��������")]
    //�������ֵ
    public float maxHealth;

    //��ǰ����ֵ
    public float currentHealth;

    //�������ֵ
    public float maxPower;

    //��ǰ����ֵ
    public float currentPower;

    //�����ظ��ٶ�
    public float powerRecoverSpeed;

    [Header("״̬")]
    //�޵�ʱ��
    public float invulnerableDuration;

    //��ʱ��
    public float invulnerableCounter;

    //�޵�
    public bool invulnerable;

    //�˺��¼�
    public UnityEvent<Transform> OnTakedamage;

    //�����¼�
    public UnityEvent OnDie;

    //����UI�仯�¼�
    public UnityEvent<Character> OnHealthChange;

    private void Start()
    {
        currentHealth = maxHealth;
        currentPower = maxPower;
        OnHealthChange?.Invoke(this);
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
        if (currentPower < maxPower)
            currentPower += Time.deltaTime * powerRecoverSpeed;
    }
    /// <summary>
    /// �����޵�֡
    /// </summary>
    private void TriggerInvulnerable() 
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration ;
        }
    }
    /// <summary>
    /// �ܵ��˺�
    /// </summary>
    /// <param name="attacker"></param>
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
        OnHealthChange?.Invoke(this);

    }
    public void Onslide(int cost)
    {
        currentPower -= cost;
        OnHealthChange?.Invoke(this);
    }
}
