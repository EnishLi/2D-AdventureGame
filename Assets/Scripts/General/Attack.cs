using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //¹¥»÷Á¦
    public int damage;

    //¹¥»÷·¶Î§
    public float attackRange;

    //¹¥»÷ÆµÂÊ
    public float attackRate;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Character>()?.TakeDamage(this);
    }
}
