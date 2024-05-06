using FSM.Base.State;

public class Goblin_MoveState : BaseState
{
    public Goblin_MoveState(Goblin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Goblin>().AnimCntrllr.SetTrigger(GetEntity<Goblin>().AnimParam_Move);
        GetEntity<Goblin>().CheckNearestCharacter();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        GetEntity<Goblin>().MoveToTarget();
    }
}
