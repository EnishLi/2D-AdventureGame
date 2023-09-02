

public abstract class BaseState
{
    protected Enemy currentEnemy;
    public abstract void onEnter (Enemy enemy);
    public abstract void Logicupdate ();

    public abstract void PhysicsUpdate();

    public abstract void OnExit ();
}
