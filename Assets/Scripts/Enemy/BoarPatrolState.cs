using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{

    public override void onEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        
    }
    public override void Logicupdate()
    {
        
        //TODO:���ֵ��ˣ�player���л���chase(���)
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.animator.SetBool("Walk", false);
        }
        else
        {
            currentEnemy.animator.SetBool("Walk", true);
        }
    }
   
    public override void PhysicsUpdate()
    {
       
    }
    public override void OnExit()
    {
        currentEnemy.animator.SetBool("Walk",false);
    }
}
