using Entity.Base;
using FSM.Base.State;

public class Goblin_IdleState : BaseState
{
    public Goblin_IdleState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        (entity as Goblin).AnimCntrllr.SetTrigger((entity as Goblin).AnimParam_Idle);
    }

    public override void OnStateExit()
    {
        (entity as Goblin).CheckNearestEnemy();
    }

    public override void OnStateUpdate()
    {

    }
}