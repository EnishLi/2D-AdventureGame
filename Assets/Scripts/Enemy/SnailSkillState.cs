
using UnityEngine;

public class SnailSkillState : BaseState
{
    public override void onEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.animator.SetBool("Walk", false);
        currentEnemy.animator.SetBool("Hide",true);
        currentEnemy.animator.SetTrigger("Skill");

        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.GetComponent<Character>().invulnerable = true;
        currentEnemy.GetComponent<Character>().invulnerableCounter = currentEnemy.lostTimeCounter;
    }
    public override void Logicupdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
            currentEnemy.SwitchState(NpcState.Patrol);
        currentEnemy.GetComponent<Character>().invulnerableCounter = currentEnemy.lostTimeCounter;
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("Hide", false);
        currentEnemy.GetComponent<Character>().invulnerable = true;
    }
}
