using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("ÒÆ¶¯·¶Î§")]

    //Ñ²Âß·¶Î§
    public float patrolRadius;

    protected override void Awake()
    {
        base.Awake();
        patrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
    }

    public override bool FoundPlayer()
    {
       var obj = Physics2D.OverlapCircle(transform.position,checkDistance,attackLayer);
        if (obj)
        {
            attcker = obj.transform;
        }
        return obj;
    }
    public override void OnDrawGizmosSelected()
    {
        //¼ì²â·¶Î§
        Gizmos.DrawWireSphere(transform.position,checkDistance);

        //ÒÆ¶¯·¶Î§
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,patrolRadius);

    }
    public override Vector3 GetNewPoint()
    {
        var targetX = Random.Range(-patrolRadius,patrolRadius);
        var targetY = Random.Range(-patrolRadius, patrolRadius);
        return spawnPoint + new Vector3(targetX, targetY);
    }
    public override void Move()
    {
        
    }
}
