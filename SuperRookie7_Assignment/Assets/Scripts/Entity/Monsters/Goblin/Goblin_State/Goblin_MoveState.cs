using FSM.Base.State;

public class Goblin_MoveState : BaseState
{
    public Goblin_MoveState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Goblin>().AnimCntrllr.SetTrigger(GetEntity<Goblin>().AnimParam_Move);
    }

    public override void OnStateExit()
    {
        GetEntity<Goblin>().CheckNearestCharacter();
    }

    public override void OnStateUpdate()
    {
        GetEntity<Goblin>().MoveToTarget();
    }
}