using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BaseState
{
    //Ä¿±êÎ»ÖÃ
    private Vector3 target;
    private Vector3 moveDir;

    private Attack attack;

    private bool isAttack;
    //ÃÛ·ä¹¥»÷¼ÆÊýÆ÷
    private float attackRateCounter = 0;
    public override void onEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        attack = enemy.GetComponent<Attack>();
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.animator.SetBool("Chase",true);
        
    }
    public override void Logicupdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
            currentEnemy.SwitchState(NpcState.Patrol);
        target = new Vector3(currentEnemy.attcker.position.x,currentEnemy.attcker.position.y+1.5f,0);

        //¹¥»÷¾àÀëÅÐ¶Ï
        if (Mathf.Abs(target.x - currentEnemy.transform.position.x) <= attack.attackRange && Mathf.Abs(target.y - currentEnemy.transform.position.y) <= attack.attackRange)
        {
            //¹¥»÷
            isAttack = true;
            if(!currentEnemy.isHurt)
                currentEnemy.rb.velocity = Vector2.zero;

            //¼ÆÊ±Æ÷
            attackRateCounter -= Time.deltaTime;
            if (attackRateCounter <= 0)
            {
                currentEnemy.animator.SetTrigger("Attack");
                attackRateCounter = attack.attackRate;  
            }
        }
        else
        {
            isAttack = false;
        }
        moveDir = (target - currentEnemy.transform.position).normalized;
        if (moveDir.x > 0)
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
        if (moveDir.x < 0)
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);
    }
    public override void PhysicsUpdate()
    {
        if (!currentEnemy.isHurt && !currentEnemy.isDead && !isAttack)
        {
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }
        
    }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("Chase",false);
    }
}
