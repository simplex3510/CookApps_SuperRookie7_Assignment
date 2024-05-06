using FSM.Base.State;

public class Knight_MoveState : BaseState
{
    public Knight_MoveState(Knight entity) : base(entity) { }

    public override void OnStateEnter()
    {
        GetEntity<Knight>().AnimCntrllr.SetTrigger(GetEntity<Knight>().AnimParam_Move);
        GetEntity<Knight>().CheckNearestMonster();
    }

    public override void OnStateExit()
    {
        GetEntity<Knight>().AnimCntrllr.ResetTrigger(GetEntity<Knight>().AnimParam_Move);
    }

    public override void OnStateUpdate()
    {
        GetEntity<Knight>().MoveToTarget();
    }
}
