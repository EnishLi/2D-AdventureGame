
public class SnailPatrolState : BaseState
{
    public override void onEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void Logicupdate()
    {
        
    }
    public override void PhysicsUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NpcState.Skill);
        }
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
    public override void OnExit()
    {
        
    }
}
