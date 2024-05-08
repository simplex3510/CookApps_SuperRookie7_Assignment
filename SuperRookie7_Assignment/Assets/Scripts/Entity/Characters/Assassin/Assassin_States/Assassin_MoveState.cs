using FSM.Base.State;

public class Assassin_MoveState : BaseState
{
    public Assassin_MoveState(Assassin entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Assassin>().AnimCntrllr.SetTrigger(GetEntity<Assassin>().AnimParam_Move);
        GetEntity<Assassin>().CheckNearestMonster();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        GetEntity<Assassin>().MoveToTarget();
    }
}
