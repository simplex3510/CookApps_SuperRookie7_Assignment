using FSM.Base.State;

public class Goblin_MoveState : BaseState
{
    public Goblin_MoveState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        (entity as Goblin).AnimCntrllr.SetTrigger((entity as Goblin).AnimParam_Move);
    }

    public override void OnStateExit()
    {
        (entity as Goblin).CheckNearestEnemy();
    }

    public override void OnStateUpdate()
    {
        (entity as Goblin).MoveToTarget();
    }
}